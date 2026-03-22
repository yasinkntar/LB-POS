using LB_POS.Core.Base;
using MediatR;

namespace LB_POS.Core.Features.Authentication.Commands.Models
{
    public class SignInWithCookieCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
