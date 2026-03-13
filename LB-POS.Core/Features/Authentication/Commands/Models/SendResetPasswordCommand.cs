using LB_POS.Core.Base;
using MediatR;

namespace LB_POS.Core.Features.Authentication.Commands.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}