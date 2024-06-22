using Microsoft.EntityFrameworkCore;

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
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.Run();
