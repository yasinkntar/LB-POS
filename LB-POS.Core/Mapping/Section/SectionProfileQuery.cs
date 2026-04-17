using LB_POS.Core.Features.Section.Query.Result;
using LB_POS.Data.Commons;
using LB_POS.Data.DTOs.SectionDtos;

namespace LB_POS.Core.Mapping.Section
{
    public partial class SectionProfile
    {
        public void GetSectionMapping()
        {
            CreateMap<SectionSummaryDto, GetSectionListResponse>()
                //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Name, src.NameEn)))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Localize(src.BranchName, src.BranchNameEn)))
                .ForMember(dest => dest.CountOfTables, opt => opt.MapFrom(src => src.TablesCount))
                .ForMember(dest => dest.CountOfWaiters, opt => opt.MapFrom(src => src.WaitersCount));
        }
    }
}
