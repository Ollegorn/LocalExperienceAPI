namespace Ollegorn.LocalExperience.Persistence.Models;

using System.ComponentModel.DataAnnotations;

using Ollegorn.LocalExperience.Persistence.Base;

public sealed class Category : LocalExperienceEntity<long>
{
  public string Name { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public IReadOnlyCollection<Activity> Activities { get; set; } = [];
}
