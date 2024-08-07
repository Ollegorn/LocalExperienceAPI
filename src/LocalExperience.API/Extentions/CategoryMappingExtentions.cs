namespace Ollegorn.LocalExperience.API.Extentions;

using Ollegorn.LocalExperience.Persistence.Models;
using Ollegorn.LocalExperience.Web.Models;

public static class CategoryMappingExtentions
{
  public static RetrieveCategoryDto ToRetrieveCategoryDto(this Category category)
  {
    ArgumentNullException.ThrowIfNull(category);

    var dto = new RetrieveCategoryDto(category.Id, category.Name, category.Description);

    return dto;
  }

  public static ICollection<RetrieveCategoryDto> ToRetrieveCategoryDtoList(this ICollection<Category> categories)
  {
    ArgumentNullException.ThrowIfNull(categories);

    var dtos = categories.Select(c => c.ToRetrieveCategoryDto()).ToList();

    return dtos;
  }
}
