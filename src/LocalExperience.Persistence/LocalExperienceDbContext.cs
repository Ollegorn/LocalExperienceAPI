namespace Ollegorn.LocalExperience.Persistence;

using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.Persistence.Models;

public class LocalExperienceDbContext(DbContextOptions<LocalExperienceDbContext> options)
  : DbContext(options)
{
  public DbSet<Category> Categories { get; set; }

  public DbSet<Activity> Activities { get; set; }

  public DbSet<Booking> Bookings { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    ArgumentNullException.ThrowIfNull(modelBuilder);

    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      foreach (var foreignKey in entityType.GetForeignKeys())
      {
        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
      }
    }
  }
}
