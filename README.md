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

### Start the app

```bash
cd infra
docker compose --env-file .env -f docker-compose.dev.yml up --build
```

### Services

- Frontend: http://localhost:3000
- API: http://localhost:8080
- Database: localhost:5432

---

## 🐳 Docker

### Backend

- Built using multi-stage Dockerfile
- Runs on ASP.NET runtime

### Frontend

- Dev mode: `Dockerfile.dev`
- Production: built and served via nginx

---

## 🔄 CI Pipeline

On every Pull Request to `develop`:

- Backend is built (.NET)
- Frontend is built (React)
- Docker images are built

Merge is blocked until all checks pass ✅

---

## 🚧 Future improvements

- CD (automatic deployment to VPS)
- Reverse proxy (Caddy / Nginx)
- Monitoring
- Automated database migrations

---

## 📌 Purpose

This template is designed to:

- skip repetitive setup
- enforce good practices (CI/CD, Docker)
- speed up development of new projects

---

## 👨‍💻 Author

Kamil
