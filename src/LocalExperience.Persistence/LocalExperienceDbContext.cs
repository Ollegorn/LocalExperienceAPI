namespace Ollegorn.LocalExperience.Persistence;

using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.Persistence.Models;

public class LocalExperienceDbContext : DbContext
{
  public LocalExperienceDbContext(DbContextOptions<LocalExperienceDbContext> options)
    : base(options)
  {
  }

  public DbSet<Category> Categories { get; set; }

  public DbSet<Activity> Activities { get; set; }
}
