using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Profile> Profiles => Set<Profile>(); // Profiles table
    public DbSet<FavoriteMovie> FavoriteMovies => Set<FavoriteMovie>(); // Favorite Movies Table

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Maps C# classes to database tables
        modelBuilder.Entity<Profile>().ToTable("profiles", "public");
        modelBuilder.Entity<FavoriteMovie>().ToTable("favorite_movies", "public");
        //Unique Constraints to ensure no Username nor Passwords are same
        modelBuilder.Entity<Profile>().HasIndex(p => p.Username).IsUnique();
        modelBuilder.Entity<Profile>().HasIndex(p => p.Email).IsUnique();
        //Ensures no duplicate favorite movies for same profile
        modelBuilder.Entity<FavoriteMovie>()
            .HasIndex(f => new { f.ProfileId, f.TmdbMovieId })
            .IsUnique();
        //Configures one-to-many rleationship between Profile and FavoriteMovie
        modelBuilder.Entity<Profile>()
            .HasMany(p => p.FavoriteMovies)
            .WithOne(f => f.Profile!)
            .HasForeignKey(f => f.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
