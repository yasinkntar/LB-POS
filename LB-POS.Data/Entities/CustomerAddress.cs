namespace LB_POS.Data.Entities
{
    public class CustomerAddress
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Title { get; set; } // Home / Work

        public string Address { get; set; }

        public int? DeliveryZoneId { get; set; }
        public DeliveryZone DeliveryZone { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool IsDefault { get; set; }
    }
}
