using LB_POS.Core.Base;
using LB_POS.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LB_POS.Core.Behavior
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ResponseHandler _responseHandler;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthorizationBehavior(
            IHttpContextAccessor httpContextAccessor,
            ResponseHandler responseHandler,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _responseHandler = responseHandler;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var allowAnonymous = request.GetType()
    .GetCustomAttributes(typeof(AllowAnonymousRequestAttribute), true)
    .Any();

            if (allowAnonymous)
                return await next();
            if (httpContext?.User == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return HandleUnauthorized<TResponse>("User Not Authenticated");
            }

            var user = httpContext.User;

            // ✅ SuperAdmin bypass
            if (user.IsInRole("SuperAdmin"))
                return await next();

            var attributes = request.GetType()
                .GetCustomAttributes(typeof(HasPermissionAttribute), true)
                .Cast<HasPermissionAttribute>()
                .ToList();

            if (!attributes.Any())
                return await next();

            // ✅ جمع User Claims
            var userClaims = user.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            // ✅ جلب Role Claims (مهم جدًا)
            var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    userClaims.AddRange(roleClaims
                        .Where(c => c.Type == "Permission")
                        .Select(c => c.Value));
                }
            }

            // إزالة التكرار
            userClaims = userClaims.Distinct().ToList();

            // ✅ التحقق من الصلاحيات
            foreach (var attr in attributes)
            {
                if (!userClaims.Contains(attr.Permission))
                {
                    return HandleUnauthorized<TResponse>($"Permission Required: {attr.Permission}");
                }
            }

            return await next();
        }

        private TResponse HandleUnauthorized<TResponse>(string message)
        {
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Base.Response<>))
            {
                var dataType = responseType.GenericTypeArguments[0];
                var method = typeof(ResponseHandler)
                    .GetMethod("Unauthorized")
                    .MakeGenericMethod(dataType);

                return (TResponse)method.Invoke(_responseHandler, new object[] { message });
            }

            throw new UnauthorizedAccessException(message);
        }
    }
}