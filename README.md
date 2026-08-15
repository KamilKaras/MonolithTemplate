# MonolithTemplate

Starter template for building full-stack applications with .NET, React, and Docker.

This repository currently contains:

- an ASP.NET Core API host in apps/api/MonolithTemplate.Api
- an Identity module with API, Application, Contracts, Domain, Infrastructure, and test projects
- a Notifications module with Application, Domain, Infrastructure, and test projects
- a shared backend foundation in apps/api/MonolithTemplate.Shared
- a React frontend in apps/web
- local and staging Docker Compose files under infra
- a pull-request CI workflow in .github/workflows/ci.yaml

## Tech stack

- Backend: ASP.NET Core on .NET 10
- Frontend: React, Vite, TypeScript, React Router, TanStack Query, PrimeReact
- Database: PostgreSQL
- Containerization: Docker and Docker Compose
- CI: GitHub Actions

## Project structure

```text
apps/
  api/
    MonolithTemplate.Api/
    MonolithTemplate.Shared/
    Modules/
      Identity/
      Notifications/
  web/
infra/
  docker-compose.dev.yml
  docker-compose.staging.yml
.github/
  workflows/
```

## Running locally

### Requirements

- Docker
- Docker Compose
- .NET SDK 10.x
- Node.js 22

### Docker-based local development

```bash
cd infra
docker compose -f docker-compose.dev.yml up --build
```

The development compose file provides safe local defaults for PostgreSQL, JWT, SMTP, and the frontend API URL. You can still override them with environment variables or an infra/.env file.

Services exposed by the current compose setup:

- Frontend: http://localhost:3000
- API: http://localhost:8080
- Database: localhost:5432

### Local configuration

Backend configuration rules:

- appsettings.json contains non-secret placeholders only
- appsettings.Development.json contains safe local development defaults
- environment variables or user secrets should be used for real credentials outside local development

Important backend settings to override outside local development:

- ConnectionStrings\_\_Default
- Jwt\_\_Key
- SmtpSettings\_\_Server
- SmtpSettings\_\_UserName
- SmtpSettings\_\_Password

Frontend configuration:

- create a local frontend env file from the template:

```bash
cp apps/web/.env.example apps/web/.env.development
```

PowerShell equivalent:

```powershell
Copy-Item apps/web/.env.example apps/web/.env.development
```

- apps/web/.env.example documents the expected VITE_API_URL value
- VITE_API_URL can override the API base URL at build or dev time

### Local app without Docker

Backend:

```bash
dotnet restore MonolithTemplate.sln
dotnet build MonolithTemplate.sln
dotnet run --project apps/api/MonolithTemplate.Api/MonolithTemplate.Api.csproj
```

Frontend:

```bash
cd apps/web
npm ci
npm run dev
```

### Local validation

```bash
dotnet test MonolithTemplate.sln
cd apps/web
npm run lint
npm run test
npm run build
```

## Docker

Backend:

- built using the multi-stage Dockerfile in apps/api
- runs on the ASP.NET runtime image

Frontend:

- development uses apps/web/Dockerfile.dev
- production uses apps/web/Dockerfile and serves the bundle with nginx

## CI pipeline

On every pull request to develop, CI runs:

- dotnet build MonolithTemplate.sln
- dotnet test MonolithTemplate.sln
- npm --prefix apps/web run lint
- npm --prefix apps/web run test
- npm --prefix apps/web run build
- Docker image builds for the API and web app

The workflow stays incremental and pull-request focused while covering the highest-value existing checks.

## Staging notes

The staging compose file expects these environment values:

- GHCR_OWNER
- IMAGE_TAG, optional, defaults to develop
- POSTGRES_DB
- POSTGRES_USER
- POSTGRES_PASSWORD
- Frontend\_\_BaseUrl
- Jwt\_\_Key
- optional SMTP overrides through SmtpSettings\_\_\*

Do not rely on placeholder values from tracked config files for staging or production.

## Purpose

This template is designed to:

- skip repetitive setup
- enforce basic quality gates through CI and containerized workflows
- provide a modular starting point for .NET and React applications

## Starting a new application

Use the repository as a GitHub Template Repository:

1. Select **Use this template** on GitHub.
2. Create and clone the new repository.
3. Run the one-time initializer from the repository root:

```powershell
.\scripts\init-template.ps1 `
  -Name MyBookingApp `
  -DisplayName "My Booking App"
```

The initializer renames the solution, backend projects, namespaces, Docker identifiers, database defaults, CI references, and frontend identity while preserving the Identity, Notifications, Shared, and layered project structure. It requires a clean Git working tree and refuses to run again after successful initialization.

After initialization:

1. Review the generated changes.
2. Create local environment files from the tracked examples where needed.
3. Commit the initialized project.
4. Begin domain development.

For frontend local development, create the ignored environment file from the tracked template:

```powershell
Copy-Item apps/web/.env.example apps/web/.env.development
```

Then configure environment-specific values such as `VITE_API_URL`, `ConnectionStrings__Default`, JWT settings, SMTP settings, and `Frontend__BaseUrl`.
