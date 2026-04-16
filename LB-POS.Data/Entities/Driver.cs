namespace LB_POS.Data.Entities
{
    using LB_POS.Data.Entities.Identity;
    using LB_POS.Data.Enums;

    public class Driver
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Code { get; set; }

        public string PhoneNumber { get; set; }

        public string VehicleNumber { get; set; }

        public DriverStatus Status { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
