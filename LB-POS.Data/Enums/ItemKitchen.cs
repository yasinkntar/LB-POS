using LB_POS.Data.Entities;

namespace LB_POS.Data.Enums
{
    public class ItemKitchen
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
