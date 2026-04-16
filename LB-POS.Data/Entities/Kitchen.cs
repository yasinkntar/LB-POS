using LB_POS.Data.Enums;

namespace LB_POS.Data.Entities
{
    public class Kitchen
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public string PrinterName { get; set; }
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public ICollection<ItemKitchen> ItemKitchens { get; set; }
    }
}
