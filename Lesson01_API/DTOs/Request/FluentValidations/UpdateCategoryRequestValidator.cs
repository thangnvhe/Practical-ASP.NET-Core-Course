using FluentValidation;

namespace Lesson01_API.DTOs.Request.FluentValidations
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("CategoryName is required.")
                .MaximumLength(100).WithMessage("CategoryName cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x => x.ParentCategoryID)
                .GreaterThan(0).When(x => x.ParentCategoryID.HasValue).WithMessage("ParentCategoryID must be greater than 0 if provided.");
        }
    }
}
