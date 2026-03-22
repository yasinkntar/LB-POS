using FluentValidation;
using LB_POS.Core.Features.Authentication.Commands.Models;
using LB_POS.Core.Resources;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.Authentication.Commands.Validatiors
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(200);
        }
    }

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("كلمات المرور يجب أن تتطابق");
        }
    }
}
