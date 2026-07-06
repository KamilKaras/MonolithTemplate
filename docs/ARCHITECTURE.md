# Architecture Rules

This document is the source of truth for architectural decisions.

AI agents must read this file before suggesting larger backend, frontend, database, or API changes.

## Overview

This repository is a .NET and React monorepo with:

- an ASP.NET Core backend under apps/api
- a React and Vite frontend under apps/web
- Docker and deployment configuration under infra
- repository automation under .github

The codebase is intended to stay modular, incremental, and safe to evolve.

## Backend structure

Primary backend locations:

- apps/api/MonolithTemplate.Api: application host and startup pipeline
- apps/api/MonolithTemplate.Shared: shared CQRS, result, outbox, unit-of-work, and event abstractions
- apps/api/Modules: feature modules

The API host is responsible for:

- shared service registration
- authentication and CORS
- exception handling
- module registration
- OpenAPI
- migration discovery and execution

### Current backend modules

Identity module:

- MonolithTemplate.Identity.Api
- MonolithTemplate.Identity.Application
- MonolithTemplate.Identity.Contracts
- MonolithTemplate.Identity.Domain
- MonolithTemplate.Identity.Infrastructure
- MonolithTemplate.Identity.Tests

Notifications module:

- MonolithTemplate.Notifications.Application
- MonolithTemplate.Notifications.Domain
- MonolithTemplate.Notifications.Infrastructure
- MonolithTemplate.Notifications.Tests

Notifications currently has no dedicated API or Contracts project. It is triggered through integration events and infrastructure services.

### Dependency direction

Within backend modules, keep dependency direction as:

- API layer to Application layer
- Application layer to Domain layer
- Infrastructure supports Application and Domain
- Shared abstractions may be consumed by modules and infrastructure

Keep endpoints and controllers thin. Business logic belongs in handlers, services, and the appropriate module layer.

### Integration event delivery policy

For backend command flows:

- enqueue user-facing cross-module integration events into the outbox when the side effect should survive process failure
- reserve the in-process event bus for explicitly synchronous flows or for dispatching events that have already been persisted through the outbox
- do not publish user-facing notification events directly from request or command handlers through the in-process event bus

## Frontend structure

Primary frontend location:

- apps/web

Current frontend stack includes:

- React
- Vite
- TypeScript
- React Router
- React Query
- Redux Toolkit
- Formik and Yup
- PrimeReact

The source tree is organized under apps/web/src with responsibility-based folders such as:

- app
- components
- features
- shared
- store

The frontend uses Vite environment variables for runtime configuration, including VITE_API_URL.

## Authentication and request flow

- the frontend sends requests through the shared API client
- login uses the Identity endpoints and the backend issues an access_token cookie
- the frontend uses /identity/me as the source of truth for the authenticated user
- forgot-password and reset-password remain part of the Identity module API surface
- notification delivery is performed downstream from integration events, not by the frontend directly

## Build and development process

Backend commands:

- dotnet restore MonolithTemplate.sln
- dotnet build MonolithTemplate.sln
- dotnet test MonolithTemplate.sln

Frontend commands:

- npm --prefix apps/web ci
- npm --prefix apps/web run dev
- npm --prefix apps/web run lint
- npm --prefix apps/web run build

Containerized development uses:

- infra/docker-compose.dev.yml
- infra/docker-compose.staging.yml

Default local ports described by the current compose setup are:

- frontend: 3000
- API: 8080
- database: 5432

## Repository guidance summary

When making changes:

- preserve the existing architecture
- prefer small, safe edits
- follow existing code style and naming conventions
- avoid broad renames or new patterns without discussion
- keep backend transport layers thin
- reuse the frontend API and query layers instead of duplicating request logic
