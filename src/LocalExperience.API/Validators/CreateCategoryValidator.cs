namespace Ollegorn.LocalExperience.API.Validators;

using FluentValidation;

using Ollegorn.LocalExperience.Web.Models;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
  public CreateCategoryValidator()
  {
    RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    RuleFor(x => x.Description).NotEmpty();
  }
}
