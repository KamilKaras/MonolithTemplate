# MonolithTemplate

Starter template for building fullstack applications using **.NET + React + Docker**.

The goal of this project is to provide a ready-to-use foundation with:

- backend API
- frontend app
- Docker setup
- CI pipeline

---

## 🚀 Tech Stack

- **Backend:** .NET (ASP.NET Core)
- **Frontend:** React (Vite)
- **Database:** PostgreSQL
- **Containerization:** Docker & Docker Compose
- **CI:** GitHub Actions

---

## 📁 Project Structure

```
/apps
  /api        -> .NET backend
  /web        -> React frontend
/infra
  docker-compose.dev.yml
  docker-compose.staging.yml
/.github
  /workflows  -> CI/CD pipelines
```

---

## 🛠️ Running locally

### Requirements

- Docker
- Docker Compose
- .NET SDK 10
- Node.js 22

### Start the app

```bash
cd infra
docker compose -f docker-compose.dev.yml up --build
```

The development compose file now provides safe local defaults for PostgreSQL, JWT, and the frontend API URL.
You can still override them with environment variables or an `infra/.env` file when needed.

### Services

- Frontend: http://localhost:3000
- API: http://localhost:8080
- Database: localhost:5432

### Local configuration

Backend configuration follows this rule:

- `appsettings.json` contains non-secret placeholders only
- `appsettings.Development.json` contains safe local development defaults
- environment variables or user secrets should be used for any real credentials

Important backend settings that should come from environment variables or user secrets outside local development:

- `ConnectionStrings__Default`
- `Jwt__Key`
- `SmtpSettings__Server`
- `SmtpSettings__UserName`
- `SmtpSettings__Password`

Frontend configuration:

- development API URL is in [apps/web/.env.development](apps/web/.env.development)
- production build API URL is in [apps/web/.env.production](apps/web/.env.production)

Build-time frontend API URL can also be overridden with `VITE_API_URL`.

### Local app without Docker

Backend:

```bash
dotnet build MonolithTemplate.sln
dotnet run --project apps/api/MonolithTemplate.Api/MonolithTemplate.Api.csproj
```

Frontend:

```bash
cd apps/web
npm install
npm run dev
```

---

## 🐳 Docker

### Backend

- Built using multi-stage Dockerfile
- Runs on ASP.NET runtime

### Frontend

- Dev mode: `Dockerfile.dev`
- Production: built and served via nginx

The development compose flow uses `Dockerfile.dev`.
The production image is built from `Dockerfile`.

---

## 🔄 CI Pipeline

On every Pull Request to `develop`:

- Backend is built (.NET)
- Frontend is built (React)
- Docker images are built

Merge is blocked until all checks pass ✅

---

## Staging notes

The staging compose file expects the following environment values:

- `GHCR_OWNER`
- `IMAGE_TAG` (optional, defaults to `develop`)
- `POSTGRES_DB`
- `POSTGRES_USER`
- `POSTGRES_PASSWORD`
- `Frontend__BaseUrl`
- `Jwt__Key`
- optional SMTP overrides (`SmtpSettings__*`)

Do not rely on placeholder values from tracked config files for staging or production.

---

## 📌 Purpose

This template is designed to:

- skip repetitive setup
- enforce good practices (CI/CD, Docker)
- speed up development of new projects

---

## 👨‍💻 Author

Kamil
