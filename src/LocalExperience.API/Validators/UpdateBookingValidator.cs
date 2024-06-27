namespace Ollegorn.LocalExperience.API.Validators;

using FluentValidation;
using Ollegorn.LocalExperience.Web.Models;

public class UpdateBookingValidator : AbstractValidator<UpdateBookingDto>
{
  public UpdateBookingValidator()
  {
    RuleFor(x => x.ReservedFor)
        .GreaterThan(DateTime.Now);

    RuleFor(x => x.CustomersName)
        .NotEmpty()
        .MinimumLength(3);

    RuleFor(x => x.NumberOfPeople)
        .GreaterThan(0);

    RuleFor(x => x.TotalPrice)
        .GreaterThan(0);

    RuleFor(x => x.CustomersNumber)
        .NotEmpty();

    RuleFor(x => x.CustomersEmail)
        .NotEmpty()
        .EmailAddress();

    RuleFor(x => x.ActivityId)
        .GreaterThan(0);
  }
}
