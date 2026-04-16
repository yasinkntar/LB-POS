namespace LB_POS.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
