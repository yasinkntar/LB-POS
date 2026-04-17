namespace LB_POS.Data.DTOs.SectionDtos
{
    public class SectionSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string BranchName { get; set; }
        public string BranchNameEn { get; set; }// الخاصية الجديدة
        public int TablesCount { get; set; }
        public int WaitersCount { get; set; }
    }
}
