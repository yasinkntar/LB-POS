namespace LB_POS.Data.DTOs
{
    public class UpdateRoleClaimsRequest
    {
        public int RoleId { get; set; }
        public List<RoleClaims> RoleClaims { get; set; } = new();
    }

    public class RoleClaims
    {
        public string GroupName { get; set; } // القسم (مثل: إدارة المستخدمين)
        public string DisplayName { get; set; } // الاسم الصديق (عرض المستخدمين)
        public string Type { get; set; } // القيمة البرمجية (Users.View)
        public bool Value { get; set; } // الحالة (True/False)
    }
}
