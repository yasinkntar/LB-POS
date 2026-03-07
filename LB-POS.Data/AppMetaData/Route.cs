namespace LB_POS.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = $"{root}/{version}/";
        public static class BranchRouteing
        {

            public const string Perfix = $"{Rule}Branch/";
            public const string GetListBranch = $"{Perfix}BranchList";
            public const string GetByID = $"{Perfix}" + "{ID}";
            public const string AddBranch = $"{Perfix}Create";
            public const string EditeBranch = $"{Perfix}Edite";
            public const string DeleteBranch = $"{Perfix}DeleteBranch";
            public const string PaginatedBranch = $"{Perfix}Paginated";


        }

    }
}
