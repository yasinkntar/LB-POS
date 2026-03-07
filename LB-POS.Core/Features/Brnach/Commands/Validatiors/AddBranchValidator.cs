using FluentValidation;
using LB_POS.Core.Features.Brnach.Commands.Models;
using LB_POS.Core.Resources;
using LB_POS.Service.IService;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.Brnach.Commands.Validatiors
{
    public class AddBranchValidator : AbstractValidator<AddBranchCommand>
    {
        private readonly IBranchService _service;
        private readonly IStringLocalizer<SharedResources> _Localizer;
        public AddBranchValidator(IBranchService service, IStringLocalizer<SharedResources> Localizer)
        {
            _service = service;
            _Localizer = Localizer;
            ApplyNewValidationsRules();
            CustomeValidationsRules();
        }

        public void ApplyNewValidationsRules()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage(_Localizer[SharedResourcesKeys.MaximumLength, 50])
                .Matches(@"^\d+$").WithMessage(_Localizer[SharedResourcesKeys.NumbersOnly]);

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(200).WithMessage(_Localizer[SharedResourcesKeys.MaximumLength, 200]);

            RuleFor(x => x.SyndicateLicenseNumber)
                .MaximumLength(30).WithMessage(_Localizer[SharedResourcesKeys.MaximumLength, 30])
                .Matches(@"^\d*$").WithMessage(_Localizer[SharedResourcesKeys.NumbersOnly])
                .When(x => !string.IsNullOrEmpty(x.SyndicateLicenseNumber));

            RuleFor(x => x.ActivityCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(10).WithMessage(_Localizer[SharedResourcesKeys.MaximumLength, 10])
                .Matches(@"^\d+$").WithMessage(_Localizer[SharedResourcesKeys.NumbersOnly]);

            RuleFor(x => x.Country)
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .Length(2).WithMessage(_Localizer[SharedResourcesKeys.MaximumLength, 2]);

            RuleFor(x => x.Governate)
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(100);

            RuleFor(x => x.RegionCity)
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(100);

            RuleFor(x => x.Street)
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(200);

            RuleFor(x => x.BuildingNumber)
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                  .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .Matches(@"^\d+$").WithMessage(_Localizer[SharedResourcesKeys.NumbersOnly]);

            RuleFor(x => x.PostalCode)
                .Matches(@"^\d*$").WithMessage(_Localizer[SharedResourcesKeys.NumbersOnly])
                .When(x => !string.IsNullOrEmpty(x.PostalCode));
        }
        public void CustomeValidationsRules()
        {
            RuleFor(x => x.Code).MustAsync(async (command, code, cancellation) =>
                                await _service.IsUniqueAsync(b => b.Code == code, null, cancellation))
                               .WithMessage(_Localizer[SharedResourcesKeys.IsExist]);
            RuleFor(x => x.Name).MustAsync(async (command, Name, cancellation) =>
                    await _service.IsUniqueAsync(b => b.Name == Name, null, cancellation))
                   .WithMessage(_Localizer[SharedResourcesKeys.IsExist]);
        }




    }
}