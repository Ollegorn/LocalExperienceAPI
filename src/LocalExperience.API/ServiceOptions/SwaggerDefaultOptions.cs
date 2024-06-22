namespace Ollegorn.LocalExperience.API.ServiceOptions;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

public class SwaggerDefaultOptions : IConfigureOptions<SwaggerGenOptions>, IConfigureOptions<SwaggerOptions>, IConfigureOptions<SwaggerUIOptions>
{
  public void Configure(SwaggerGenOptions options)
  {
    ArgumentNullException.ThrowIfNull(options);

    options.EnableAnnotations(true, true);
    options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.InferSecuritySchemes();
    options.DescribeAllParametersInCamelCase();

    options.UseAllOfForInheritance();
    options.UseOneOfForPolymorphism();
  }

  public void Configure(SwaggerOptions options)
  {
    ArgumentNullException.ThrowIfNull(options);

    options.PreSerializeFilters.Add((openApi, request) => openApi.Servers =
      [new OpenApiServer { Url = $"{request.Scheme}://{request.Host.Value}", }]);
  }

  public void Configure(SwaggerUIOptions options)
  {
    ArgumentNullException.ThrowIfNull(options);

    options.DocExpansion(DocExpansion.None);

    options.DisplayOperationId();
    options.DisplayRequestDuration();

    options.EnableDeepLinking();
    options.EnableFilter();
    options.EnablePersistAuthorization();
    options.EnableTryItOutByDefault();
    options.EnableValidator();
    options.ShowExtensions();
  }
}
