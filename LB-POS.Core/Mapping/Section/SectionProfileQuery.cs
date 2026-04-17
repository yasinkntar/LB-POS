using LB_POS.Core.Features.Section.Query.Result;
using LB_POS.Data.Commons;

namespace LB_POS.Core.Mapping.Section
{
    public partial class SectionProfile
    {
        public void GetSectionMapping()
        {
            CreateMap<LB_POS.Data.Entities.Section, GetSectionListResponse>()
                //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Name, src.NameEn)))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Localize(src.Branch.Name, src.Branch.NameEn)))
                .ForMember(dest => dest.CountOfTables, opt => opt.MapFrom(src => src.Tables.Count()))
                .ForMember(dest => dest.CountOfWaiters, opt => opt.MapFrom(src => src.Waiters.Count()));
        }
    }
}
