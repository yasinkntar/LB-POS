using LB_POS.Core.Base;
using LB_POS.Core.Behavior;
using MediatR;

namespace LB_POS.Core.Features.Authentication.Commands.Models
{
    [AllowAnonymousRequest]
    public class SignInWithCookieCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
