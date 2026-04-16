using AutoMapper;
using LB_POS.Core.Base;
using LB_POS.Core.Features.ApplicationUser.Commands.Models;
using LB_POS.Core.Resources;
using LB_POS.Data.Entities.Identity;
using LB_POS.Service.IService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
                IRequestHandler<AddUserCommand, Response<string>>,
                IRequestHandler<EditUserCommand, Response<string>>,
                IRequestHandler<DeleteUserCommand, Response<string>>,
                IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly IApplicationUserService _applicationUserService;
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                         IMapper mapper,
                                         UserManager<User> userManager,
                                         IHttpContextAccessor httpContextAccessor,
                                         IEmailsService emailsService,
                                         IApplicationUserService applicationUserService
            ) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _applicationUserService = applicationUserService;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);
            //Create
            var createResult = await _applicationUserService.AddUserAsync(identityUser, request.Password);
            switch (createResult)
            {
                case "EmailIsExist": return BadRequest<string>(_sharedResources[SharedResourcesKeys.EmailIsExist]);
                case "UserNameIsExist": return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserNameIsExist]);
                case "ErrorInCreateUser": return BadRequest<string>(_sharedResources[SharedResourcesKeys.FaildToAddUser]);
                case "Failed": return BadRequest<string>(_sharedResources[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(createResult);
            }
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();
            //Delete the User
            var result = await _userManager.DeleteAsync(user);
            //in case of Failure
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeletedFailed]);
            return Success((string)_sharedResources[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {     //get user
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();

            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //var user1=await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, request.NewPassword);

            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_sharedResources[SharedResourcesKeys.Success]);
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            // 1. التأكد من وجود المستخدم
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null) return NotFound<string>();

            // 2. التحقق من أن اسم المستخدم الجديد غير محجوز لمستخدم آخر
            var userByUserName = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Id != oldUser.Id);

            if (userByUserName != null)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserNameIsExist]);

            // 3. عمل Mapping للبيانات الأساسية (FullName, Email, PhoneNumber, etc.)
            // ملاحظة: تأكد أن المابنج لا يقوم بنسخ حقل كلمة المرور بشكل تلقائي للـ PasswordHash
            _mapper.Map(request, oldUser);

            // 4. تحديث البيانات الأساسية للمستخدم
            var result = await _userManager.UpdateAsync(oldUser);
            if (!result.Succeeded)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.UpdateFailed]);

            // 5. منطق تغيير كلمة المرور (فقط إذا تم إرسال كلمة مرور جديدة)
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                // إزالة كلمة المرور القديمة (بدون الحاجة لمعرفة القديمة، بما أنك Admin)
                await _userManager.RemovePasswordAsync(oldUser);

                // إضافة كلمة المرور الجديدة
                var addPasswordResult = await _userManager.AddPasswordAsync(oldUser, request.Password);

                if (!addPasswordResult.Succeeded)
                {
                    // في حال فشلت (مثلاً بسبب شروط تعقيد كلمة المرور)
                    var error = string.Join(", ", addPasswordResult.Errors.Select(e => e.Description));
                    return BadRequest<string>(error);
                }
            }

            // 6. رسالة النجاح
            return Success((string)_sharedResources[SharedResourcesKeys.Updated]);
        }

    }
}
