using FluentValidation;
using LB_POS.Core.Features.Brnach.Commands.Models;
using LB_POS.Service.IService;

namespace LB_POS.Core.Features.Brnach.Commands.Validatiors
{
    public class EditBranchValidator : AbstractValidator<EditBranchCommand>
    {
        private readonly IBranchService _service;
        public EditBranchValidator(IBranchService service)
        {
            _service = service;
            ApplyNewValidationsRules();
            CustomeValidationsRules();
        }

        public void ApplyNewValidationsRules()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Code مطلوب")
                .NotNull().WithMessage("Code مطلوب")
                .MaximumLength(50).WithMessage("Code يجب ألا يتجاوز 50 حرف")
                .Matches(@"^\d+$").WithMessage("Code يجب أن يحتوي على أرقام فقط");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("الاسم مطلوب")
                .NotNull().WithMessage("Code مطلوب")
                .MaximumLength(200).WithMessage("الاسم يجب ألا يتجاوز 200 حرف");

            RuleFor(x => x.SyndicateLicenseNumber)
                .MaximumLength(30).WithMessage("رقم الرخصة يجب ألا يتجاوز 30 حرف")
                .Matches(@"^\d*$").WithMessage("رقم الرخصة يجب أن يحتوي على أرقام فقط")
                .When(x => !string.IsNullOrEmpty(x.SyndicateLicenseNumber));

            RuleFor(x => x.ActivityCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("ActivityCode مطلوب")
                .MaximumLength(10).WithMessage("ActivityCode يجب ألا يتجاوز 10 حرف")
                .Matches(@"^\d+$").WithMessage("ActivityCode يجب أن يحتوي على أرقام فقط");

            RuleFor(x => x.Country)
                .NotNull().WithMessage("Code مطلوب")
                .NotEmpty().WithMessage("الدولة مطلوبة")
                .Length(2).WithMessage("Country يجب أن يحتوي على حرفين فقط");

            RuleFor(x => x.Governate)
                .NotNull().WithMessage("Code مطلوب")
                .NotEmpty().WithMessage("المحافظة مطلوبة")
                .MaximumLength(100);

            RuleFor(x => x.RegionCity)
                .NotNull().WithMessage("Code مطلوب")
                .NotEmpty().WithMessage("المدينة مطلوبة")
                .MaximumLength(100);

            RuleFor(x => x.Street)
                .NotNull().WithMessage("Code مطلوب")
                .NotEmpty().WithMessage("الشارع مطلوب")
                .MaximumLength(200);

            RuleFor(x => x.BuildingNumber)
                .NotNull().WithMessage("Code مطلوب")
                .NotEmpty().WithMessage("رقم المبنى مطلوب")
                .Matches(@"^\d+$").WithMessage("رقم المبنى يجب أن يحتوي على أرقام فقط");

            RuleFor(x => x.PostalCode)
                .Matches(@"^\d*$").WithMessage("PostalCode يجب أن يحتوي على أرقام فقط")
                .When(x => !string.IsNullOrEmpty(x.PostalCode));
        }
        public void CustomeValidationsRules()
        {
            RuleFor(x => x.Code).MustAsync(async (command, code, cancellation) =>
                                await _service.IsUniqueAsync(b => b.Code == code, command.Id, cancellation))
                               .WithMessage("Code موجود مسبقاً");
            RuleFor(x => x.Name).MustAsync(async (command, Name, cancellation) =>
                    await _service.IsUniqueAsync(b => b.Name == Name, command.Id, cancellation))
                   .WithMessage("Name موجود مسبقاً");
        }




    }
}