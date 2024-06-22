using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Ollegorn.LocalExperience.API.DependencyInjectionExtentions;
using Ollegorn.LocalExperience.API.HostedServices;
using Ollegorn.LocalExperience.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddHostedService<FirstBackgroundService>();
builder.Services.AddDbContext<LocalExperienceDbContext>(options => options
  .UseNpgsql(builder.Configuration.GetConnectionString("LocalExperienceDb"), npgsql => npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddSwaggerDefaults();
builder.Services.AddSwaggerGen(o =>
{
  var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

  o.IncludeXmlComments(xmlPath);
});

builder.Services.AddApiVersioning(o =>
{
  o.ReportApiVersions = true;
  o.AssumeDefaultVersionWhenUnspecified = true;
})
  .AddApiExplorer(o =>
  {
    o.SubstituteApiVersionInUrl = true;
    o.GroupNameFormat = "'v'VVV";
  })
  .EnableApiVersionBinding();

builder.Services.AddSwaggerVersioning(new OpenApiInfo { Title = "LocalExperienceAPI", Description = "This is a short description" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
  app.UseSwaggerUI(o =>
  {
    var descriptions = app.DescribeApiVersions();
    foreach (var desc in descriptions)
    {
      o.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
    }
  });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.Run();
