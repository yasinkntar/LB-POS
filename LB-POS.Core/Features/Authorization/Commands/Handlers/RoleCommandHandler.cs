using LB_POS.Core.Base;
using LB_POS.Core.Features.Authorization.Commands.Models;
using LB_POS.Core.Resources;
using LB_POS.Service.IService;
using MediatR;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
           IRequestHandler<AddRoleCommand, Response<string>>,
           IRequestHandler<EditRoleCommand, Response<string>>,
           IRequestHandler<DeleteRoleCommand, Response<string>>,
           IRequestHandler<UpdateUserRolesCommand, Response<string>>,
    IRequestHandler<UpdateRoleClaimsCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public RoleCommandHandler(
            IStringLocalizer<SharedResources> localizer,
            IAuthorizationService authorizationService) : base(localizer)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Command Handlers
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);

            // استخدام مقارنة غير حساسة لحالة الأحرف لتجنب أخطاء الـ Magic Strings
            if (result.Equals("Success", StringComparison.OrdinalIgnoreCase))
                return Success((string)_localizer[SharedResourcesKeys.Success]);

            return BadRequest<string>(_localizer[SharedResourcesKeys.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);

            // استخدام Switch Expression (C# 8+) لكود أنظف وأسهل في القراءة
            return result switch
            {
                "notFound" or "NotFound" => NotFound<string>(),
                "Success" => Success((string)_localizer[SharedResourcesKeys.Updated]),
                _ => BadRequest<string>(result)
            };
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);

            return result switch
            {
                "NotFound" or "notFound" => NotFound<string>(),
                "Used" => BadRequest<string>(_localizer[SharedResourcesKeys.RoleIsUsed]),
                "Success" => Success((string)_localizer[SharedResourcesKeys.Deleted]),
                _ => BadRequest<string>(result)
            };
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);

            return result switch
            {
                "UserIsNull" => NotFound<string>(_localizer[SharedResourcesKeys.UserIsNotFound]),
                "FailedToRemoveOldRoles" => BadRequest<string>(_localizer[SharedResourcesKeys.FailedToRemoveOldRoles]),
                "FailedToAddNewRoles" => BadRequest<string>(_localizer[SharedResourcesKeys.FailedToAddNewRoles]),
                "FailedToUpdateUserRoles" => BadRequest<string>(_localizer[SharedResourcesKeys.FailedToUpdateUserRoles]),
                "Success" => Success<string>(_localizer[SharedResourcesKeys.Success]),
                _ => Success<string>(_localizer[SharedResourcesKeys.Success]) // Default fallback
            };
        }
        #endregion
        public async Task<Response<string>> Handle(UpdateRoleClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateRoleClaims(request);

            return result switch
            {
                "Success" => Success((string)_localizer[SharedResourcesKeys.Updated]),
                "RoleNotFound" => NotFound<string>(_localizer[SharedResourcesKeys.NotFound]),
                "FailedToUpdateRoleClaims" => BadRequest<string>(_localizer[SharedResourcesKeys.NotFound]),
                _ => BadRequest<string>(result)
            };
        }
    }
}