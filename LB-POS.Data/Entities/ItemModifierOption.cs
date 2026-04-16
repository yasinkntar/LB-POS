namespace LB_POS.Data.Entities
{
    public class ItemModifierOption
    {
        public int Id { get; set; }

        public int ModifierGroupId { get; set; }
        public ItemModifierGroup ModifierGroup { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public decimal Price { get; set; } // إضافة سعر

        public bool IsDefault { get; set; }
    }
}
