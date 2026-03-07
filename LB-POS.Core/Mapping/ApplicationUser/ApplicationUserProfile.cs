using AutoMapper;

namespace LB_POS.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            GetUserPaginationMapping();
            GetUserByIdMapping();
            UpdateUserMapping();
        }
    }
}
