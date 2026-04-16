namespace LB_POS.Data.Entities
{
    public class Section
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public int DisplayOrder { get; set; }

        public ICollection<Table> Tables { get; set; }
        public ICollection<Waiter> Waiters { get; set; }
    }
}
