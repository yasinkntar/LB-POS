using AutoMapper;

namespace LB_POS.Core.Mapping.Brnach
{
    public partial class BrnachProfile : Profile
    {
        public BrnachProfile()
        {
            // Query
            GetBranchListMapping();
            GetBranchByIDMapping();
            /// Command
            InsertBranchMapping();
            EditeBranchMapping();
        }
    }
}
