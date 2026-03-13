using LB_POS.Core.Base;
using LB_POS.Data.Results;
using MediatR;

namespace LB_POS.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
