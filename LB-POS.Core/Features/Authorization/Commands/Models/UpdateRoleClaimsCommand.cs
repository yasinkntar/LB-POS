using LB_POS.Core.Base;
using LB_POS.Data.DTOs;
using MediatR;

namespace LB_POS.Core.Features.Authorization.Commands.Models
{
    public class UpdateRoleClaimsCommand : UpdateRoleClaimsRequest, IRequest<Response<string>>
    {
        // يرث من الـ Request الخاص بك ليحمل الـ RoleId وقائمة الـ Claims
    }
}
