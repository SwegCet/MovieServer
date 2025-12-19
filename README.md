# Movie Search – Backend (ASP.NET Core)

This is the **backend API** for the Movie Search & Favorites application. It is built using **ASP.NET Core**, **Entity Framework Core**, and is Dockerized for easy deployment.

---

## Tech Stack

* **ASP.NET Core Web API**
* **Entity Framework Core** (ORM)
* **Supabase / PostgreSQL** (database)
* **JWT Authentication**
* **Docker**
* **Swagger / OpenAPI**

---

## Project Structure

```
MovieApi/
├── Controllers/        # API endpoints (Movies, Favorites, Auth)
├── Data/               # DbContext & EF configuration
├── Models/             # Entity models
├── DTOs/               # Request/response DTOs
├── Services/           # Reusable backend services (Auth, Tokens, TMDB API)
├── Program.cs          # App startup
└── appsettings.json    # Configuration
```

---

## Architecture Overview

* **Controllers** handle HTTP requests
* **Entity Framework Core** maps models to the database
* **JWT middleware** protects authenticated routes
* **Swagger** provides interactive API documentation

---

## Authentication

* JWT tokens are expected in the `Authorization` header

```
Authorization: Bearer <token>
```

* Tokens are validated using Supabase or a configured authority

---

## Favorites Endpoints

| Method | Endpoint                  | Description                |
| ------ | ------------------------- | -------------------------- |
| GET    | `/api/favorites`          | Get user's favorite movies |
| POST   | `/api/favorites`          | Add a favorite movie       |
| DELETE | `/api/favorites/{tmdbId}` | Remove a favorite          |

The backend stores:

* TMDB Movie ID
* Movie Title
* Poster Path

---

## Environment Variables

Recommended `.env` (or Render environment variables):

```
SUPABASE_URL=...
SUPABASE_ANON_KEY=...
JWT_AUTHORITY=https://<project>.supabase.co/auth/v1
DB_CONNECTION_STRING=...
```

> Never commit secrets to GitHub

---

## Running Locally (No Docker)

```bash
dotnet restore
dotnet run
```

Runs at:

```
http://localhost:7248
```

---

## Running with Docker

### Build

```bash
docker build -t movieapi .
```

### Run

```bash
docker run -p 8080:8080 movieapi
```

API available at:

```
http://localhost:8080
```

---

## Swagger

Swagger UI:

```
http://localhost:8080/swagger
```

Use this to test endpoints and verify authentication.

---

## Common Issues

* **404 errors** → Check route attributes
* **CORS issues** → Configure allowed origins
* **JWT failures** → Verify authority & token format
* **Docker port mismatch** → Ensure `ASPNETCORE_URLS` matches exposed port

---
