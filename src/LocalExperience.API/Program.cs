using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.API.HostedServices;
using Ollegorn.LocalExperience.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddHostedService<FirstBackgroundService>();
builder.Services.AddDbContext<LocalExperienceDbContext>(options => options
  .UseNpgsql(builder.Configuration.GetConnectionString("LocalExperienceDb"), npgsql => npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddSwaggerGen(options =>
{
  options.CustomSchemaIds(x => x.FullName?.Replace("+", ".", StringComparison.Ordinal));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.Run();
