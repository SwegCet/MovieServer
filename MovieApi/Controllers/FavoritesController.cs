using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/favorites")]
[Authorize]
public class FavoritesController(AppDbContext db) : ControllerBase
{
    private Guid UserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(id!);
    }

    [HttpGet]
    public async Task<ActionResult<List<FavoriteMovie>>> GetMine()
    {
        var uid = UserId();
        var favs = await db.FavoriteMovies
            .Where(f => f.ProfileId == uid)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();

        return Ok(favs);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FavoriteCreateRequest req)
    {
        var uid = UserId();

        var exists = await db.FavoriteMovies.AnyAsync(f => f.ProfileId == uid && f.TmdbMovieId == req.TmdbMovieId);
        if (exists) return NoContent();

        db.FavoriteMovies.Add(new FavoriteMovie
        {
            ProfileId = uid,
            TmdbMovieId = req.TmdbMovieId,
            Title = req.Title,
            PosterPath = req.PosterPath
        });

        await db.SaveChangesAsync();
        return Created("", null);
    }

    [HttpDelete("{tmdbMovieId:int}")]
    public async Task<IActionResult> Remove(int tmdbMovieId)
    {
        var uid = UserId();
        var fav = await db.FavoriteMovies.SingleOrDefaultAsync(f => f.ProfileId == uid && f.TmdbMovieId == tmdbMovieId);
        if (fav is null) return NoContent();

        db.FavoriteMovies.Remove(fav);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
