using BookShop2025.Web.ViewModels.Category;
using FluentValidation;

namespace BookShop2025.Web.Validators
{
    public class CategoryValidator : AbstractValidator<CatetoryEditVm>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Required")
                .MinimumLength(3).WithMessage("Must have al least {MinLength} characters")
                .MaximumLength(50).WithMessage("No more than {MaxLength} characters");
        }
    }
}
