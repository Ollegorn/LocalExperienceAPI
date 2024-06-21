namespace Ollegorn.LocalExperience.API.HostedServices;

using System.Threading;
using System.Threading.Tasks;

public class FirstBackgroundService : BackgroundService
{
  private CancellationTokenSource? cts;

  private readonly ILogger logger;

  public FirstBackgroundService(ILogger<FirstBackgroundService> logger)
  {
    this.logger = logger;
  }

  public void Trigger()
  {
    cts?.Cancel();
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    logger.LogWarning("The service is starting");

    while (true)
    {
      cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
      try
      {
        await Task.Delay(Timeout.Infinite, cts.Token);
      }
      catch (TaskCanceledException)
      {
        logger.LogWarning("Service is running");
      }

      cts.Dispose();
    }
  }
}
