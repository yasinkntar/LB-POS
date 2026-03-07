using FluentValidation;
using LB_POS.Core.Features.ApplicationUser.Commands.Models;
using LB_POS.Core.Resources;
using LB_POS.Service.IService;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.ApplicationUser.Commands.Validatiors
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IBranchService _service;
        private readonly IStringLocalizer<SharedResources> _Localizer;
        public AddUserValidator(IBranchService service, IStringLocalizer<SharedResources> Localizer)
        {
            _service = service;
            _Localizer = Localizer;
            ApplyNewValidationsRules();
            CustomeValidationsRules();
        }

        private void CustomeValidationsRules()
        {
            //   throw new NotImplementedException();
        }

        private void ApplyNewValidationsRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResourcesKeys.MaximumLength, 100]);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResourcesKeys.MaximumLength, 100]);

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_Localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_Localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.ConfirmPassword)
                 .Equal(x => x.Password).WithMessage(_Localizer[SharedResourcesKeys.PasswordNotEqualConfirmPass]);

        }
    }
}
