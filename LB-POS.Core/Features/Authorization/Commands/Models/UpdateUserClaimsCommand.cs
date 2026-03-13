using LB_POS.Core.Base;
using LB_POS.Data.DTOs;
using MediatR;

namespace LB_POS.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
    {
    }
}
