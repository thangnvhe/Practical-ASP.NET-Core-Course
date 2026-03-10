using FluentValidation;

namespace Lesson01_API.DTOs.Request.FluentValidations
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.ParentCategoryID)
                .GreaterThan(0).When(x => x.ParentCategoryID.HasValue)
                .WithMessage("Parent category ID must be greater than 0.");
        }
    }
}
