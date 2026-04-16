namespace LB_POS.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<CustomerAddress> Addresses { get; set; }
        //public ICollection<Order> Orders { get; set; }
    }
}
