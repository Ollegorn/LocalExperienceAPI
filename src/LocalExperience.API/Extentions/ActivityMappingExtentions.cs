namespace Ollegorn.LocalExperience.API.Extensions
{
  using Ollegorn.LocalExperience.Persistence.Models;
  using Ollegorn.LocalExperience.Web.Models;

  public static class ActivityMappingExtensions
  {
    public static RetrieveActivityDto ToRetrieveActivityDto(this Activity activity)
    {
      ArgumentNullException.ThrowIfNull(activity);

      var dto = new RetrieveActivityDto(
          activity.Id,
          activity.Name,
          activity.Description,
          activity.AvailableDays,
          activity.PricePerPerson,
          activity.TotalMinutes,
          activity.MaxPeople,
          activity.IsSponsored,
          activity.Category.Id);

      return dto;
    }

    public static ICollection<RetrieveActivityDto> ToRetrieveActivityDtoList(this ICollection<Activity> activities)
    {
      ArgumentNullException.ThrowIfNull(activities);

      var dtos = activities.Select(a => a.ToRetrieveActivityDto()).ToList();

      return dtos;
    }
  }
}
