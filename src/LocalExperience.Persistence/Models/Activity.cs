namespace Ollegorn.LocalExperience.Persistence.Models;

using Ollegorn.LocalExperience.Persistence.Base;

public sealed class Activity : LocalExperienceEntity<long>
{
  public string Name { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public decimal PricePerPerson { get; set; }

  public int TotalMinutes { get; set; }

  public int MaxPeople { get; set; }

  public bool IsSponsored { get; set; }

  public Category Category { get; set; } = default!;
}

// TODO: bitwise operators, logging, cancelation token.
