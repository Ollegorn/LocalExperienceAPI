using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.Persistence;
using Ollegorn.LocalExperience.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<LocalExperienceDbContext>(options => options
  .UseNpgsql(builder.Configuration.GetConnectionString("LocalExperienceDb"), npgsql => npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

var a = new CreateActivityDto("dsad", "dsad", 10, 60, 3, true, 1);
var b = a with { Name = "kaitoula" };
