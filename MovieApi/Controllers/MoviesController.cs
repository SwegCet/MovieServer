using Microsoft.AspNetCore.Mvc;
using MovieApi.Services;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController(TmdbService tmdb) : ControllerBase
{
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
        => Content(await tmdb.SearchMovies(q), "application/json");

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => Content(await tmdb.GetMovie(id), "application/json");
}
