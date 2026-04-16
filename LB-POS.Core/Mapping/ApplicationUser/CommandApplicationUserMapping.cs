using LB_POS.Core.Features.ApplicationUser.Commands.Models;
using LB_POS.Data.Entities.Identity;

namespace LB_POS.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
        public void UpdateUserMapping()
        {
            CreateMap<EditUserCommand, User>() // أو اسم كلاس المستخدم لديك
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // تجاهل الـ ID لأنه لا يتغير
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
