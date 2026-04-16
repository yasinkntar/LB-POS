using LB_POS.Data.Enums;

namespace LB_POS.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsAvailable { get; set; }

        public bool HasVariants { get; set; }

        public ICollection<ItemModifierGroup> ModifierGroups { get; set; }
        public ICollection<ItemKitchen> ItemKitchens { get; set; }
    }
}
