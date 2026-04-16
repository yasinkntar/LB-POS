using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_POS.Core.Features.Section.Query.Result
{
    public class GetSectionListResponse
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string BranchName { get; set; }
        public int CountOfTables { get; set; }
        public int DisplayOrder { get; set; }
    }
}
