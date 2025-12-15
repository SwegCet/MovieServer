using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models;

[Table("profiles", Schema = "public")]
public class Profile
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(32)]
    [Column("username")]
    public string Username { get; set; } = "";

    [Required, MaxLength(255)]
    [Column("email")]
    public string Email { get; set; } = "";

    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = "";

    [Required]
    [Column("role")]
    public string Role { get; set; } = "User";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<FavoriteMovie> FavoriteMovies { get; set; } = new();
}
