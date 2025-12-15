using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models;

[Table("favorite_movies", Schema = "public")]
public class FavoriteMovie
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("profile_id")]
    public Guid ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public Profile? Profile { get; set; }

    [Required]
    [Column("tmdb_movie_id")]
    public int TmdbMovieId { get; set; }

    [Required, MaxLength(300)]
    [Column("title")]
    public string Title { get; set; } = "";

    [Column("poster_path")]
    public string? PosterPath { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
