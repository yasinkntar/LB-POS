namespace LB_POS.Data.DTOs
{
    public class ManageRoleClaimsResult
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleClaims> RoleClaims { get; set; } = new();
    }
}
