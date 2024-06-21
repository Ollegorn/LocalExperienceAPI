namespace Ollegorn.LocalExperience.Persistence.Models;

using Ollegorn.LocalExperience.Persistence.Base;

public sealed class Booking : LocalExperienceEntity<Guid>
{
  public DateTime ReservedFor { get; set; }

  public string CustomersName { get; set; } = string.Empty;

  public int NumberOfPeople { get; set; }

  public decimal TotalPrice { get; set; }

  public string CustomersNumber { get; set; } = string.Empty;

  public string CustomersEmail { get; set; } = string.Empty;

  public string Coupon { get; set; } = string.Empty;

  public Activity Activity { get; set; } = default!;
}
