#pragma warning disable SA1402 // File may only contain a single type
namespace Ollegorn.LocalExperience.Web.Models;

public record CreateActivityDto(
  string Name,
  string Description,
  decimal PricePerPerson,
  int TotalMinutes,
  int MaxPeople,
  bool IsSponsored,
  long CategoryId);

public record UpdateActivityDto(
  string Name,
  string Description,
  decimal PricePerPerson,
  int TotalMinutes,
  int MaxPeople,
  bool IsSponsored,
  long CategoryId);

public record RetrieveActivityDto(
  long Id,
  string Name,
  string Description,
  decimal PricePerPerson,
  int TotalMinutes,
  int MaxPeople,
  bool IsSponsored,
  long CategoryId);
