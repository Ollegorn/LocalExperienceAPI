#pragma warning disable SA1402 // File may only contain a single type
namespace Ollegorn.LocalExperience.Web.Models;

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

public record CreateActivityDto(
  string Name,
  string Description,
  Days AvailableDays,
  decimal PricePerPerson,
  int TotalMinutes,
  int MaxPeople,
  bool IsSponsored,
  long CategoryId);

public record UpdateActivityDto(
  string Name,
  string Description,
  Days AvailableDays,
  decimal PricePerPerson,
  int TotalMinutes,
  int MaxPeople,
  bool IsSponsored,
  long CategoryId);

public record RetrieveActivityDto(
  long Id,
  string Name,
  string Description,
  Days AvailableDays,
  decimal PricePerPerson,
  int TotalMinutes,
  int MaxPeople,
  bool IsSponsored,
  long CategoryId);
