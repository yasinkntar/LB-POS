using LB_POS.Data.Enums;

namespace LB_POS.Data.Entities
{
    public class Table
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public int? SectionId { get; set; }
        public Section Section { get; set; }

        public string TableNumber { get; set; }
        public string Name { get; set; }

        public int Capacity { get; set; }

        public TableStatus Status { get; set; }

        public double? PosX { get; set; }
        public double? PosY { get; set; }

        public bool IsActive { get; set; }
    }
}
