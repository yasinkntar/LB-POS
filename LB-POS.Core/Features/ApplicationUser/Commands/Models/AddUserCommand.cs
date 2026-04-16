using LB_POS.Core.Base;
using LB_POS.Core.Behavior;
using LB_POS.Data.Helpers;
using MediatR;

namespace LB_POS.Core.Features.ApplicationUser.Commands.Models
{
    [HasPermission(Permission.UserManger.CreateUser)]
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

    }
}
