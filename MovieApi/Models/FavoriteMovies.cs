using System;

namespace MovieApi.Models
{
    public class FavoriteMovies
    {
        public long Id { get; set; } // bigserial
        public Guid UserId { get; set; } // uuid
        public required string MovieApiId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Profile User { get; set; } = null!;
    }
}
