namespace Ollegorn.LocalExperience.API.DependencyInjectionExtentions;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

public static class SwaggerVersioningExtentions
{
  public static IServiceCollection AddSwaggerVersioning(this IServiceCollection services, OpenApiInfo apiInfo)
  {
    ArgumentNullException.ThrowIfNull(services);
    ArgumentNullException.ThrowIfNull(apiInfo);

    services.TryAddSingleton(apiInfo);

    services.TryAddSingleton<SwaggerGenApiVersioningOptions>();
    services.TryAddSingleton<IConfigureOptions<SwaggerGenOptions>>(sp => sp.GetRequiredService<SwaggerGenApiVersioningOptions>());
    services.TryAddSingleton<IConfigureOptions<SwaggerUIOptions>>(sp => sp.GetRequiredService<SwaggerGenApiVersioningOptions>());

    return services;
  }
}
