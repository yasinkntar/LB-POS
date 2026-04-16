namespace LB_POS.Data.Entities
{
    public class DeliveryZone
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal? MinOrderAmount { get; set; }

        public int? EstimatedTime { get; set; }

        public bool IsActive { get; set; }
    }
}
