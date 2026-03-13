using LB_POS.Core.Base;
using MediatR;

namespace LB_POS.Core.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
