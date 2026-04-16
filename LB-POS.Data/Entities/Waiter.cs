namespace LB_POS.Data.Entities
{
    using LB_POS.Data.Entities.Identity;

    public class Waiter
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Code { get; set; }

        public string PhoneNumber { get; set; }

        public int? SectionId { get; set; }
        public Section Section { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
