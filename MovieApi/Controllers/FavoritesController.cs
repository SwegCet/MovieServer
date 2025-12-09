using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using System.Threading.Tasks;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public FavoritesController(AppDbContext db)
        {
            _db = db;
        }

        // GET /api/favorites/{userId}
        [HttpGet("{userId:guid}")]
        public async Task<ActionResult> GetFavorites(Guid userId)
        {
            var favorites = await _db.FavoriteMovies
                    .Where(f => f.UserId == userId)
                    .ToListAsync();

            return Ok(favorites);
        }

        public class AddFavoriteRequest
        {
            public Guid UserId { get; set; }
            public string MovieApiId { get; set; } = string.Empty;
        }

        //POST /api/favorites
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest req)
        {
            var fav = new FavoriteMovies
            {
                UserId = req.UserId,
                MovieApiId = req.MovieApiId,
                CreatedAt = DateTime.UtcNow
            };

            _db.FavoriteMovies.Add(fav);
            await _db.SaveChangesAsync();

            return Ok(fav);
        }
    }
}
