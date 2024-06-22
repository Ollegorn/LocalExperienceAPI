namespace Ollegorn.LocalExperience.API.Endpoints;

using Asp.Versioning;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.API.Extentions;
using Ollegorn.LocalExperience.Persistence;
using Ollegorn.LocalExperience.Web.Models;

public class CategoryEndpoints : IEndpoint
{
  public void MapEndpoint(IEndpointRouteBuilder app)
  {
    ArgumentNullException.ThrowIfNull(app);

    var api = app.NewApiVersionSet()
      .HasApiVersion(new ApiVersion(1))
      .Build();

    var group = app
      .MapGroup("api/v{apiVersion}/category")
      .WithApiVersionSet(api)
      .HasApiVersion(1)
      .WithTags("VersionCategory")
      .WithOpenApi();

    group.MapGet("{id:long}", GetCategory);
  }

  public async Task<Results<Ok<RetrieveCategoryDto>, NotFound>> GetCategory(long id, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(dbContext);

    var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    if (category == null)
    {
      return TypedResults.NotFound();
    }

    var categoryDto = category.ToRetrieveCategoryDto();
    return TypedResults.Ok(categoryDto);
  }
}
