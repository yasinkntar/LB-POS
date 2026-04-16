using LB_POS.Core.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LB_POS.Core.Features.ApplicationUser.Commands.Models
{

    public class EditUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; } // علامة الاستفهام تجعلها Nullable (اختيارية)

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
