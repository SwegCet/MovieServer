using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System;

namespace MovieApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Profile> Profiles { get; set; } 
        public DbSet<FavoriteMovies> FavoriteMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profiletest");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Username).HasColumnName("username");
                entity.Property(p => p.CreatedAt).HasColumnName("created_at");
            });

            modelBuilder.Entity<FavoriteMovies>(entity =>
            {
                entity.ToTable("favoritetest");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.UserId).HasColumnName("id");
                entity.Property(f => f.Id).HasColumnName("user_id");
                entity.Property(f => f.MovieApiId).HasColumnName("movie_api_id");
                entity.Property(f => f.CreatedAt).HasColumnName("created_at");

                entity.HasOne(f => f.User)
                    .WithMany().
                    HasForeignKey(f => f.User.Id);
            });
        }

    }
}
