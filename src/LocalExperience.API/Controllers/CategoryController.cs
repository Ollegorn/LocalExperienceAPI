namespace Ollegorn.LocalExperience.API.Controllers;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Ollegorn.LocalExperience.API.Extentions;
using Ollegorn.LocalExperience.API.HostedServices;
using Ollegorn.LocalExperience.Persistence.Models;

[Route("api/v1/category2")]
[ApiController]
public class CategoryController : ControllerBase
{
  private readonly ILogger logger;
  private readonly FirstBackgroundService? firstBackgroundService;

  public CategoryController(ILogger<CategoryController> logger, IEnumerable<IHostedService> services)
  {
    this.logger = logger;
    this.firstBackgroundService = services.OfType<FirstBackgroundService>().FirstOrDefault();
  }

  [HttpGet("{id:long}")]
  public async Task<Results<Ok<Category>, NotFound>> GetCategory(long id, CancellationToken cancellationToken)
  {
    var category = new Category();
    if (category == null)
    {
      logger.LogEntityNotFound(nameof(Category), id);
      return TypedResults.NotFound();
    }

    await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
    logger.LogCritical("returning category");
    return TypedResults.Ok(category);
  }

  [HttpGet("trigger")]
  public async Task<Ok> Trigger()
  {
    firstBackgroundService?.Trigger();
    return TypedResults.Ok();
  }
}
