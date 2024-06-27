namespace Ollegorn.LocalExperience.API.Validators;

using FluentValidation;
using Ollegorn.LocalExperience.Web.Models;

public class UpdateActivityValidator : AbstractValidator<UpdateActivityDto>
{
  public UpdateActivityValidator()
  {
    RuleFor(x => x.Name)
        .NotEmpty()
        .MinimumLength(3);

    RuleFor(x => x.Description)
        .NotEmpty();

    RuleFor(x => x.AvailableDays)
        .NotEqual(Days.None)
        .WithMessage("Available days must be specified.");

    RuleFor(x => x.PricePerPerson)
        .GreaterThan(0);

    RuleFor(x => x.TotalMinutes)
        .GreaterThan(0);

    RuleFor(x => x.MaxPeople)
        .GreaterThan(0);

    RuleFor(x => x.CategoryId)
        .GreaterThan(0);
  }
}
