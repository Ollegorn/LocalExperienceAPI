namespace Ollegorn.LocalExperience.API.Endpoints;

using System.ComponentModel.DataAnnotations;

using Asp.Versioning;

using FluentValidation;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.API.Extensions;
using Ollegorn.LocalExperience.Persistence;
using Ollegorn.LocalExperience.Persistence.Models;
using Ollegorn.LocalExperience.Web.Models;

public class ActivityEndpoints(ILogger<ActivityEndpoints> logger) : IEndpoint
{
  private readonly ILogger logger = logger;

  public void MapEndpoint(IEndpointRouteBuilder app)
  {
    ArgumentNullException.ThrowIfNull(app);

    var api = app.NewApiVersionSet()
      .HasApiVersion(new ApiVersion(1))
      .Build();

    var group = app
      .MapGroup("api/v1/activity") // {apiVersion}
      .WithApiVersionSet(api)
      .HasApiVersion(1)
      .WithTags("Activity")
      .WithOpenApi();

    group.MapGet("{id:long}", GetActivity).WithName(nameof(GetActivity));
    group.MapGet(string.Empty, GetAllActivities);
    group.MapPost(string.Empty, CreateActivity);
    group.MapDelete("{id:long}", DeleteActivity);
    group.MapPut("{id:long}", UpdateActivity);
  }

  public async Task<Results<Ok<RetrieveActivityDto>, NotFound>> GetActivity(long id, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var activity = await dbContext.Activities.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    if (activity == null)
    {
      return TypedResults.NotFound();
    }

    var activityDto = activity.ToRetrieveActivityDto();
    return TypedResults.Ok(activityDto);
  }

  public async Task<Ok<ICollection<RetrieveActivityDto>>> GetAllActivities(LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var activities = await dbContext.Activities.Include(a => a.Category).ToListAsync(cancellationToken);
    var dtos = activities.ToRetrieveActivityDtoList();

    return TypedResults.Ok(dtos);
  }

  public async Task<Results<CreatedAtRoute<RetrieveActivityDto>, BadRequest, ValidationProblem>> CreateActivity(CreateActivityDto createActivityDto, LocalExperienceDbContext dbContext, IValidator<CreateActivityDto> validator, CancellationToken cancellationToken)
  {
    var validationResult = await validator.ValidateAsync(createActivityDto, cancellationToken);
    var category = await dbContext.Categories.FindAsync(createActivityDto.CategoryId);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var activity = new Activity
    {
      Name = createActivityDto.Name,
      Description = createActivityDto.Description,
      AvailableDays = createActivityDto.AvailableDays,
      PricePerPerson = createActivityDto.PricePerPerson,
      TotalMinutes = createActivityDto.TotalMinutes,
      MaxPeople = createActivityDto.MaxPeople,
      IsSponsored = createActivityDto.IsSponsored,
      Category = category
    };

    dbContext.Activities.Add(activity);
    await dbContext.SaveChangesAsync(cancellationToken);

    var activityDto = activity.ToRetrieveActivityDto();

    return TypedResults.CreatedAtRoute(activityDto, nameof(GetActivity), new { id = activity.Id });
  }

  public async Task<Results<NoContent, NotFound>> DeleteActivity(long id, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var activity = await dbContext.Activities.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    if (activity is null)
    {
      return TypedResults.NotFound();
    }

    dbContext.Activities.Remove(activity);
    await dbContext.SaveChangesAsync(cancellationToken);

    return TypedResults.NoContent();
  }

  public async Task<Results<Ok<RetrieveActivityDto>, NotFound>> UpdateActivity(long id, UpdateActivityDto updateActivityDto, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var activity = await dbContext.Activities.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    var category = await dbContext.Categories.FindAsync(updateActivityDto.CategoryId);
    if (activity is null)
    {
      return TypedResults.NotFound();
    }

    activity.Name = updateActivityDto.Name;
    activity.Description = updateActivityDto.Description;
    activity.AvailableDays = updateActivityDto.AvailableDays;
    activity.PricePerPerson = updateActivityDto.PricePerPerson;
    activity.TotalMinutes = updateActivityDto.TotalMinutes;
    activity.MaxPeople = updateActivityDto.MaxPeople;
    activity.IsSponsored = updateActivityDto.IsSponsored;
    activity.Category = category;

    await dbContext.SaveChangesAsync(cancellationToken);

    var activityDto = activity.ToRetrieveActivityDto();
    return TypedResults.Ok(activityDto);
  }
}
