using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.Persistence;

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
using var scope = app.Services.CreateScope();
var ctx = scope.ServiceProvider.GetRequiredService<LocalExperienceDbContext>();
await ctx.Activities.ToListAsync();
//app.Run();
