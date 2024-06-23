namespace Ollegorn.LocalExperience.API.Validators;

using FluentValidation;

using Ollegorn.LocalExperience.Web.Models;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
  public UpdateCategoryValidator()
  {
    RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    RuleFor(x => x.Description).NotEmpty();
  }
}
