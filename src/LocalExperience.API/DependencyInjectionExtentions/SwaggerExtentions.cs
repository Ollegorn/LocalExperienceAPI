namespace Ollegorn.LocalExperience.API.DependencyInjectionExtentions;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Ollegorn.LocalExperience.API.ServiceOptions;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

public static class SwaggerExtentions
{
  public static IServiceCollection AddSwaggerDefaults(this IServiceCollection services)
  {
    ArgumentNullException.ThrowIfNull(services);

    services.TryAddSingleton<SwaggerDefaultOptions>();
    services.TryAddSingleton<IConfigureOptions<SwaggerOptions>>(s => s.GetRequiredService<SwaggerDefaultOptions>());
    services.TryAddSingleton<IConfigureOptions<SwaggerUIOptions>>(s => s.GetRequiredService<SwaggerDefaultOptions>());
    services.TryAddSingleton<IConfigureOptions<SwaggerGenOptions>>(s => s.GetRequiredService<SwaggerDefaultOptions>());

    return services;
  }
}
