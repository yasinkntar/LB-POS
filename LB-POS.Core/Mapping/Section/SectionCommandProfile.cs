namespace LB_POS.Core.Mapping.Section
{
    public partial class SectionProfile
    {
        public void InsertSectionMapping()
        {
            CreateMap<Features.Section.Command.Models.AddSectionCommand, Data.Entities.Section>();
        }
    }
}
