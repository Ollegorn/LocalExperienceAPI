using System.Reflection;

using Microsoft.Extensions.DependencyInjection.Extensions;

public interface IEndpoint
{
  /// <summary>
  /// Maps the endpoint to the provided <see cref="IEndpointRouteBuilder"/>.
  /// </summary>
  /// <param name="app">The app with the new endpoint mappings.</param>
  void MapEndpoint(IEndpointRouteBuilder app);
}

public static class EndpointExtentions
{
  public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
  {
    ArgumentNullException.ThrowIfNull(services);
    ArgumentNullException.ThrowIfNull(assembly);

    var serviceDescriptors = assembly
      .DefinedTypes
      .Where(t => t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(typeof(IEndpoint)))
      .Select(t => ServiceDescriptor.Transient(typeof(IEndpoint), t))
      .ToArray();

    services.TryAddEnumerable(serviceDescriptors);
    return services;
  }

  public static WebApplication MapEndpoints(this WebApplication webApplication)
  {
    ArgumentNullException.ThrowIfNull(webApplication);

    var endpoints = webApplication.Services.GetService<IEnumerable<IEndpoint>>() ?? [];

    foreach (var endpoint in endpoints)
    {
      endpoint.MapEndpoint(webApplication);
    }

    return webApplication;
  }
}
