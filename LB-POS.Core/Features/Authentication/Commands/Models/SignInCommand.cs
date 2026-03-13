using LB_POS.Core.Base;
using LB_POS.Data.Results;
using MediatR;

namespace LB_POS.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
