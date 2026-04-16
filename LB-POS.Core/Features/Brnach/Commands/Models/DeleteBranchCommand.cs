using LB_POS.Core.Base;
using MediatR;

namespace LB_POS.Core.Features.Brnach.Commands.Models
{
    public class DeleteBranchCommand : IRequest<Response<string>>
    {
        public int ID { get; set; }
    }
}
