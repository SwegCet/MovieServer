using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<FavoriteMovie> FavoriteMovies => Set<FavoriteMovie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>().ToTable("profiles", "public");
        modelBuilder.Entity<FavoriteMovie>().ToTable("favorite_movies", "public");

        modelBuilder.Entity<Profile>().HasIndex(p => p.Username).IsUnique();
        modelBuilder.Entity<Profile>().HasIndex(p => p.Email).IsUnique();

        modelBuilder.Entity<FavoriteMovie>()
            .HasIndex(f => new { f.ProfileId, f.TmdbMovieId })
            .IsUnique();

        modelBuilder.Entity<Profile>()
            .HasMany(p => p.FavoriteMovies)
            .WithOne(f => f.Profile!)
            .HasForeignKey(f => f.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
