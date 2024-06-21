namespace Ollegorn.LocalExperience.API.Extentions;

public static partial class LoggerExtentions
{
  private const string LogEntityNotFoundMessage = "Entity {EntityType} with id {Id} was not found.";

  [LoggerMessage(Level = LogLevel.Warning, Message = LogEntityNotFoundMessage)]
  public static partial void LogEntityNotFound(this ILogger logger, string entityType, object id);
}
