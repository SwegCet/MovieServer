using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Services;

public class AuthService(AppDbContext db, JwtTokenService jwt)
{
    public async Task<AuthResponse> Signup(SignupRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Email) ||
            string.IsNullOrWhiteSpace(req.Username) ||
            string.IsNullOrWhiteSpace(req.Password))
            throw new Exception("Email, username, and password are required.");

        var exists = await db.Profiles.AnyAsync(p => p.Username == req.Username || p.Email == req.Email);
        if (exists) throw new Exception("Username or email already taken.");

        var user = new Profile
        {
            Email = req.Email.Trim(),
            Username = req.Username.Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            Role = "User",
        };

        db.Profiles.Add(user);
        await db.SaveChangesAsync();

        return new AuthResponse(jwt.Create(user), user.Username);
    }

    public async Task<AuthResponse> Login(LoginRequest req)
    {
        var user = await db.Profiles.SingleOrDefaultAsync(p => p.Username == req.Username);
        if (user is null) throw new Exception("Invalid credentials.");

        var ok = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash);
        if (!ok) throw new Exception("Invalid credentials.");

        return new AuthResponse(jwt.Create(user), user.Username);
    }
}
