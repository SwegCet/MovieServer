namespace MovieApi.DTOs;
public record FavoriteCreateRequest(int TmdbMovieId, string Title, string? PosterPath);
