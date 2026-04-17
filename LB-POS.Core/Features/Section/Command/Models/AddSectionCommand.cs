using LB_POS.Core.Base;
using MediatR;

namespace LB_POS.Core.Features.Section.Command.Models
{
    public class AddSectionCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public int BranchId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
