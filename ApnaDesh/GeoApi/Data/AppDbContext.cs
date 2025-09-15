using GeoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<State> States => Set<State>();
    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<State>()
            .HasIndex(s => new { s.CountryId, s.Name })
            .IsUnique();

        modelBuilder.Entity<City>()
            .HasIndex(c => new { c.StateId, c.Name })
            .IsUnique();

        modelBuilder.Entity<State>()
            .HasOne(s => s.Country)
            .WithMany(c => c.States)
            .HasForeignKey(s => s.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<City>()
            .HasOne(c => c.State)
            .WithMany(s => s.Cities)
            .HasForeignKey(c => c.StateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
