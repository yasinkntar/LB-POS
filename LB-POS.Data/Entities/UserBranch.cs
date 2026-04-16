using LB_POS.Data.Entities.Identity;

namespace LB_POS.Data.Entities
{
    public class UserBranch
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsDefault { get; set; } // الفرع الافتراضي
        public DateTime CreatedAt { get; set; }


        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
