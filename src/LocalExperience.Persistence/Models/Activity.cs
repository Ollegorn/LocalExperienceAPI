#pragma warning disable SA1402 // File may only contain a single type
namespace Ollegorn.LocalExperience.Persistence.Models;

using Ollegorn.LocalExperience.Persistence.Base;

[Flags]
public enum Days
{
  None = 0,
  Monday = 1,
  Tuesday = Monday << 1,
  Wednesday = Tuesday << 1,
  Thursday = Wednesday << 1,
  Friday = Thursday << 1,
  Saturday = Friday << 1,
  Sunday = Saturday << 1,

  Weekend = Saturday | Sunday,
}

public sealed class Activity : LocalExperienceEntity<long>
{
  // public Activity()
  // {
  //  Days d = Days.Monday | Days.Thursday;
  //  bool isTuesdaySet = (d & Days.Tuesday) == Days.Tuesday;
  //  var isMondaySet = d.HasFlag(Days.Monday);
  // }
  public string Name { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public Days AvailableDays { get; set; }

  public decimal PricePerPerson { get; set; }

  public int TotalMinutes { get; set; }

  public int MaxPeople { get; set; }

  public bool IsSponsored { get; set; }

  public Category Category { get; set; } = default!;

  public IReadOnlyCollection<Booking> Bookings { get; set; } = [];
}
