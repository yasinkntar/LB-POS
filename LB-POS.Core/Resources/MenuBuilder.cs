using LB_POS.Data.Helpers;

namespace LB_POS.Core.Resources
{
    public static class MenuBuilder
    {
        public static List<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                // 1. لوحة التحكم والإدارة (Dashboard & Setup)
                new MenuItem
                {
                    Title = "الإدارة العامة",
                    Icon = "fas fa-shield-alt",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "إحصائيات الفروع", Url = "/Dashboard/Index", Icon = "fas fa-chart-line" },
                        new MenuItem { Title = "الفروع", Url = "/Branches", Icon = "fas fa-store-alt" },
                        new MenuItem { Title = "إدارة الاقسام", Url = "/Sections", Icon = "fas fa-fire-alt" },
                        new MenuItem { Title = "إدارة المطابخ", Url = "/Setup/Kitchens", Icon = "fas fa-fire-alt" },
                        new MenuItem { Title = "تخطيط الطاولات", Url = "/Setup/Tables", Icon = "fas fa-chair" }
                    }
                },

                // 2. إدارة المخازن والمشتريات (Inventory)
                new MenuItem
                {
                    Title = "المخازن والمواد الخام",
                    Icon = "fas fa-boxes",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "تعريف المخازن", Url = "/Inventory/Warehouses", Icon = "fas fa-warehouse" },
                        new MenuItem { Title = "الأصناف الخام", Url = "/Inventory/RawItems", Icon = "fas fa-apple-alt" },
                        new MenuItem { Title = "أرصدة المخازن", Url = "/Inventory/StockLevel", Icon = "fas fa-list-ol" },
                        new MenuItem { Title = "تحويلات بين الفروع", Url = "/Inventory/Transfers", Icon = "fas fa-exchange-alt" },
                        new MenuItem { Title = "فواتير المشتريات", Url = "/Inventory/Purchases", Icon = "fas fa-file-invoice" },
                        new MenuItem { Title = "هالك المخزون", Url = "/Inventory/Waste", Icon = "fas fa-trash-alt" }
                    }
                },

                // 3. المنيو والمنتجات (Menu Engineering)
                new MenuItem
                {
                    Title = "قائمة الطعام",
                    Icon = "fas fa-utensils",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "تصنيفات المنيو", Url = "/Menu/Categories", Icon = "fas fa-tags" },
                        new MenuItem { Title = "الوجبات والمنتجات", Url = "/Menu/Items", Icon = "fas fa-hamburger" },
                        new MenuItem { Title = "المكونات (Recipes)", Url = "/Menu/Recipes", Icon = "fas fa-blender" },
                        new MenuItem { Title = "الإضافات (Modifiers)", Url = "/Menu/Extras", Icon = "fas fa-plus-circle" }
                    }
                },

                // 4. الكول سنتر والعملاء (Call Center & CRM)
                new MenuItem
                {
                    Title = "مركز الاتصال",
                    Icon = "fas fa-headset",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "شاشة استقبال الطلبات", Url = "/CallCenter/TakeOrder", Icon = "fas fa-phone-volume" },
                        new MenuItem { Title = "سجل العملاء", Url = "/CallCenter/Customers", Icon = "fas fa-address-book" },
                        new MenuItem { Title = "توزيع الطلبات", Url = "/CallCenter/Dispatch", Icon = "fas fa-map-marker-alt" }
                    }
                },

                // 5. التشغيل والمبيعات (Operations)
                new MenuItem
                {
                    Title = "نقاط البيع والتشغيل",
                    Icon = "fas fa-desktop",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "شاشة الكاشير (POS)", Url = "/Sales/POS", Icon = "fas fa-cash-register" },
                        new MenuItem { Title = "شاشة المطبخ (KDS)", Url = "/Kitchen/Display", Icon = "fas fa-concierge-bell" },
                        new MenuItem { Title = "الطلبات النشطة", Url = "/Sales/ActiveOrders", Icon = "fas fa-stream" },
                        new MenuItem { Title = "إغلاق الوردية", Url = "/Sales/ShiftClose", Icon = "fas fa-lock" }
                    }
                },

                // 6. إدارة السائقين (Delivery Management)
                new MenuItem
                {
                    Title = "إدارة التوصيل",
                    Icon = "fas fa-motorcycle",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "بيانات السائقين", Url = "/Drivers/Index", Icon = "fas fa-id-card" },
                        new MenuItem { Title = "توزيع السائقين", Url = "/Drivers/Assignment", Icon = "fas fa-user-tag" },
                        new MenuItem { Title = "تسوية حسابات السائقين", Url = "/Drivers/Settlements", Icon = "fas fa-wallet" }
                    }
                },

                // 7. شؤون الموظفين (HR & Attendance)
                new MenuItem
                {
                    Title = "الموظفين",
                    Icon = "fas fa-user-clock",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "سجل الموظفين", Url = "/HR/Employees", Icon = "fas fa-user-friends" },
                        new MenuItem { Title = "الحضور والانصراف", Url = "/HR/Attendance", Icon = "fas fa-fingerprint" }
                    }
                },

                // 8. التقارير المالية (Reporting)
                new MenuItem
                {
                    Title = "التقارير",
                    Icon = "fas fa-file-invoice-dollar",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "مبيعات الفروع الموحدة", Url = "/Reports/BranchSales", Icon = "fas fa-chart-bar" },
                        new MenuItem { Title = "تقرير الضريبة (14%)", Url = "/Reports/TaxReport", Icon = "fas fa-percent" },
                        new MenuItem { Title = "تحليل تكلفة الوجبات", Url = "/Reports/FoodCost", Icon = "fas fa-funnel-dollar" },
                        new MenuItem { Title = "تقرير النواقص", Url = "/Reports/Shortage", Icon = "fas fa-exclamation-triangle" }
                    }
                },

                // 9. إدارة المستخدمين والصلاحيات
                new MenuItem
                {
                    Title = SharedResourcesKeys.UsersManagement,
                    Icon = "fas fa-user-shield",
                    Permission = Permission.UserManger.ViewUsers,
                    Children = new List<MenuItem>
                    {
                        new MenuItem
                        {
                             Title = SharedResourcesKeys.RoleIsUsed,
                             Url = "/Roles",
                             Icon = "fas fa-user-tag",
                             Permission = Permission.UserManger.ViewRoles,
                        },
                        new MenuItem
                        {
                             Title = SharedResourcesKeys.UsersManagement,
                             Url = "/Users",
                             Icon = "fas fa-users",
                             Permission = Permission.UserManger.ViewUsers,
                        }
                    }
                }
            };
        }
    }
}