namespace Ollegorn.LocalExperience.API.Extentions;

using System.Globalization;

using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions.Refit.Destructurers;

public static class LoggingExtentions
{
  public static LoggerConfiguration ConfigureStartupLogger(this LoggerConfiguration config)
    => (config ?? throw new ArgumentNullException(nameof(config)))
      .MinimumLevel.ControlledBy(Program.levelSwitch)
      .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
      .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
      .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
      .MinimumLevel.Override("System", LogEventLevel.Error)
      .Enrich.WithMachineName()
      .Enrich.WithEnvironmentUserName()
      .Enrich.WithProcessId()
      .Enrich.WithProcessName()
      .Enrich.WithThreadId()
      .Enrich.WithThreadName()
      .Enrich.FromLogContext()
      .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
        .WithDefaultDestructurers()
        .WithDestructurers([
          new ApiExceptionDestructurer(false),
          new DbUpdateExceptionDestructurer()
          ]))
      .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, formatProvider: CultureInfo.InvariantCulture);

  public static LoggerConfiguration ConfigureRuntimeLogger(this LoggerConfiguration config, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
  {
    config.ConfigureStartupLogger()
      .Enrich.WithProperty("Application", webHostEnvironment.ApplicationName)
      .Enrich.WithProperty("Environment", webHostEnvironment.EnvironmentName)
      .WriteTo.Logger(log => log
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Error)
        .WriteTo.File(
          configuration.GetValue("AzureDeployment", false)
          ? $@"d:\home\logfiles\application\{webHostEnvironment.ApplicationName}.txt"
          : Path.Combine(Directory.GetCurrentDirectory(), "Logs", $"{webHostEnvironment.ApplicationName}-.log"),
          fileSizeLimitBytes: 31_457_280,
          rollingInterval: RollingInterval.Day,
          rollOnFileSizeLimit: true,
          retainedFileCountLimit: 10,
          shared: true,
          flushToDiskInterval: TimeSpan.FromSeconds(5),
          formatProvider: CultureInfo.InvariantCulture)
        .WriteTo.Seq(configuration["Serilog:Seq:Uri"], apiKey: configuration["Serilog:Seq:ApiKey"], controlLevelSwitch: Program.levelSwitch));

    return config;
  }
}
