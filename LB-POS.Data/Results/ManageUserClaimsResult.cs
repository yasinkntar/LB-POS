namespace LB_POS.Data.Results
{
    public class ManageUserClaimsResult
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public List<UserClaims> userClaims { get; set; }
    }
    public class UserClaims
    {
        public string GroupName { get; set; } // القسم (مثل: إدارة المستخدمين)
        public string DisplayName { get; set; } // الاسم الصديق (عرض المستخدمين)
        public string Type { get; set; } // القيمة البرمجية (Users.View)
        public bool Value { get; set; } // الحالة (True/False)
        public bool IsInheritedFromRole { get; set; } // خاصية جديدة
    }
}
