namespace LB_POS.Data.Entities
{
    public class ItemModifierGroup
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public bool IsRequired { get; set; }   // لازم يختار
        public bool AllowMultiple { get; set; } // اختيار متعدد
        public int MinSelect { get; set; }
        public int MaxSelect { get; set; }

        public ICollection<ItemModifierOption> Options { get; set; }
    }
}
