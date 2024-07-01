namespace Ollegorn.LocalExperience.API.Endpoints;

using System.ComponentModel.DataAnnotations;

using Asp.Versioning;

using FluentValidation;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.API.Extentions;
using Ollegorn.LocalExperience.Persistence;
using Ollegorn.LocalExperience.Persistence.Models;
using Ollegorn.LocalExperience.Web.Models;

public class CategoryEndpoints(ILogger<CategoryEndpoints> logger) : IEndpoint
{
  private readonly ILogger logger = logger;

  public void MapEndpoint(IEndpointRouteBuilder app)
  {
    ArgumentNullException.ThrowIfNull(app);

    var api = app.NewApiVersionSet()
      .HasApiVersion(new ApiVersion(1))
      .Build();

    var group = app
      .MapGroup("api/v1/category") // {apiVersion}
      .WithApiVersionSet(api)
      .HasApiVersion(1)
      .WithTags("VersionCategory")
      .WithOpenApi();

    group.MapGet("{id:long}", GetCategory).WithName(nameof(GetCategory));
    group.MapGet(string.Empty, GetAllCategories);
    group.MapPost(string.Empty, CreateCategory);
    group.MapDelete(string.Empty, DeleteCategory);
    group.MapPut(string.Empty, UpdateCategory);
  }

  public async Task<Results<Ok<RetrieveCategoryDto>, NotFound>> GetCategory(long id, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    if (category == null)
    {
      logger.LogEntityNotFound(nameof(Category), id);
      return TypedResults.NotFound();
    }

    var categoryDto = category.ToRetrieveCategoryDto();
    return TypedResults.Ok(categoryDto);
  }

  public async Task<Ok<ICollection<RetrieveCategoryDto>>> GetAllCategories(LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var categories = await dbContext.Categories.ToListAsync(cancellationToken);
    var dtos = categories.ToRetrieveCategoryDtoList();

    return TypedResults.Ok(dtos);
  }

  public async Task<Results<CreatedAtRoute<RetrieveCategoryDto>, BadRequest, ValidationProblem>> CreateCategory(CreateCategoryDto createCategoryDto, LocalExperienceDbContext dbContext, IValidator<CreateCategoryDto> validator, CancellationToken cancellationToken)
  {
    var validationResult = await validator.ValidateAsync(createCategoryDto, cancellationToken);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var category = new Category
    {
      Name = createCategoryDto.Name,
      Description = createCategoryDto.Description,
    };

    dbContext.Categories.Add(category);
    await dbContext.SaveChangesAsync(cancellationToken);

    var categoryDto = category.ToRetrieveCategoryDto();

    return TypedResults.CreatedAtRoute(categoryDto, nameof(GetCategory), new { id = category.Id });
  }

  public async Task<Results<NoContent, NotFound, Conflict<string>>> DeleteCategory(long id, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    if (category is null)
    {
      return TypedResults.NotFound();
    }

    dbContext.Categories.Remove(category);

    var activities = await dbContext.Activities.Where(a => a.Category.Id == id).ToListAsync(cancellationToken);

    if (activities.Count != 0)
    {
      return TypedResults.Conflict("There are Activities registered in this Category.");
    }

    await dbContext.SaveChangesAsync(cancellationToken);
    return TypedResults.NoContent();
  }

  public async Task<Results<Ok<RetrieveCategoryDto>, NotFound>> UpdateCategory(long id, UpdateCategoryDto updateCategoryDto, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    if (category is null)
    {
      return TypedResults.NotFound();
    }

    category.Name = updateCategoryDto.Name;
    category.Description = updateCategoryDto.Description;

    await dbContext.SaveChangesAsync(cancellationToken);

    var categoryDto = category.ToRetrieveCategoryDto();
    return TypedResults.Ok(categoryDto);
  }
}
