using System;

namespace MovieApi.Models
{
    public class Profile
    {
        public Guid Id { get; set; } // uuid
        public required string Username { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<FavoriteMovies> FavoriteMovies { get; set; } = new List<FavoriteMovies>();
    }
}
