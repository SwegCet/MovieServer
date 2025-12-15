using System.Net.Http.Headers;

namespace MovieApi.Services;

public class TmdbService(IConfiguration config, HttpClient http)
{
    private string Token => config["Tmdb:BearerToken"]!;

    private void EnsureAuth()
    {
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        http.DefaultRequestHeaders.Accept.Clear();
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> SearchMovies(string query)
    {
        EnsureAuth();
        var url = $"https://api.themoviedb.org/3/search/movie?query={Uri.EscapeDataString(query)}";
        return await http.GetStringAsync(url);
    }

    public async Task<string> GetMovie(int id)
    {
        EnsureAuth();
        var url = $"https://api.themoviedb.org/3/movie/{id}";
        return await http.GetStringAsync(url);
    }
}
