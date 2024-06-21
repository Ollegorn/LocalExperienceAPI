#pragma warning disable SA1402 // File may only contain a single type
namespace Ollegorn.LocalExperience.Web.Models;

public record CreateCategoryDto(
  string Name,
  string Description);

public record UpdateCategoryDto(
  string Name,
  string Description);

public record RetrieveCategoryDto(
  long Id,
  string Name,
  string Description);
