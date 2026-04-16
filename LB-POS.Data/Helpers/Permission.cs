namespace LB_POS.Data.Helpers
{
    public static class Permission
    {
        // أسماء الأقسام للسهولة
        public static class Modules
        {
            public const string UserManagement = "إدارة المستخدمين";
            public const string RoleManagement = "إدارة الأدوار";
            public const string Inventory = "المخازن";
            public const string Branch = "الفروع";
        }

        public static class UserManger
        {
            // نستخدم صيغة مجمعة: "القسم.الصلاحية"
            public const string ViewUsers = "Users.View";
            public const string CreateUser = "Users.Create";
            public const string DeleteUser = "Users.Delete";
            public const string EditUser = "Users.Edit";
            public const string EditRoleUser = "Users.EditRoleUser";
            public const string EditPermissionUser = "Users.EditPermissionUser";

            public const string ViewRoles = "Roles.View";
            public const string CreateRole = "Roles.Create";
            public const string DeleteRole = "Roles.Delete";
            public const string EditRole = "Roles.Edit";
        }
        public static class Branch
        {
            // نستخدم صيغة مجمعة: "القسم.الصلاحية"
            public const string ViewBranch = "Branch.View";
            public const string CreateBranch = "Branch.Create";
            public const string DeletBranch = "Branch.Delete";
            public const string EditBranch = "Branch.Edit";


        }

        // مصفوفة مساعدة لربط كل صلاحية بقسمها (لأغراض العرض)
        public static List<(string Group, string Name, string Value)> GetAllPermissions()
        {
            return new List<(string, string, string)>
            {
                (Modules.UserManagement, "عرض المستخدمين", UserManger.ViewUsers),
                (Modules.UserManagement, "إضافة مستخدم", UserManger.CreateUser),
                (Modules.UserManagement, "حذف مستخدم", UserManger.DeleteUser),
                (Modules.UserManagement, "تعديل مستخدم", UserManger.EditUser),
                (Modules.UserManagement, "تعديل دور المستخدم", UserManger.EditRoleUser),
                (Modules.UserManagement, "تعديل صلاحيات المستخدم", UserManger.EditPermissionUser),

                (Modules.RoleManagement, "عرض الأدوار", UserManger.ViewRoles),
                (Modules.RoleManagement, "إضافة دور", UserManger.CreateRole),
                (Modules.RoleManagement, "حذف دور", UserManger.DeleteRole),
                (Modules.RoleManagement, "تعديل دور", UserManger.EditRole),


                (Modules.Branch, "عرض الفروع", Branch.ViewBranch),
                (Modules.Branch, "إضافة فرع", Branch.CreateBranch),
                (Modules.Branch, "حذف فرع", Branch.DeletBranch),
                (Modules.Branch, "تعديل فرع", Branch.EditBranch),
            };
        }
    }
}