using LB_POS.Core.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LB_POS.Core.Behavior
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ResponseHandler _responseHandler;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, ResponseHandler responseHandler)
        {
            _httpContextAccessor = httpContextAccessor;
            _responseHandler = responseHandler;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType()
                .GetCustomAttributes(typeof(LB_POS.Core.Behavior.AuthorizeClaimAttribute), true)
                .Cast<LB_POS.Core.Behavior.AuthorizeClaimAttribute>();

            foreach (var attr in authorizeAttributes)
            {
                var hasClaim = _httpContextAccessor.HttpContext.User.Claims
                    .Any(c => c.Type == attr.ClaimType && c.Value == attr.ClaimValue);

                if (!hasClaim)
                {
                    // ارجع Unauthorized ResponseHandler بدل Result
                    var responseType = typeof(TResponse);
                    if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Base.Response<>))
                    {
                        var dataType = responseType.GenericTypeArguments[0];
                        var method = typeof(ResponseHandler).GetMethod("Unauthorized").MakeGenericMethod(dataType);
                        return (TResponse)method.Invoke(_responseHandler, new object[] { null });
                    }

                    throw new UnauthorizedAccessException("لا توجد صلاحيات كافية");
                }
            }

            return await next();
        }
    }
}