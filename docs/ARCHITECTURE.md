# Architecture Rules

This document is the source of truth for architectural decisions.

AI agents must read this file before suggesting larger backend, frontend, database, or API changes.

Technical Architecture Document
Overview
This repository is a .NET + React monorepo designed as a starter template for building full-stack applications. It combines:

an ASP.NET Core backend under the apps/api area
a React + Vite frontend under the apps/web area
Docker and deployment configuration under infra
repository automation and CI guidance under .github
The project is organized to support modular development, clear separation of concerns, and containerized local execution.

1. Backend Structure
   Primary backend location
   The backend is located in api.
   Core backend entry point
   The application bootstrap is in Program.cs.
   The startup pipeline registers:
   shared services
   authentication and CORS
   exception handling
   module registrations
   OpenAPI
   migrations
   Backend layering approach
   The backend is organized into modular feature areas under Modules. Each feature follows a layered structure with separate projects for:

Api
Application
Contracts
Domain
Infrastructure
Current modules
Identity module:
Identity
Notifications module:
Notifications
Shared backend infrastructure
Shared cross-cutting concerns are centralized in MonolithTemplate.Shared, including:

CQRS abstractions and dispatcher
database and unit-of-work support
domain base types
event abstractions and event bus infrastructure
outbox/result-pattern helpers
value objects
Backend architectural intent
The repository guidance in AGENTS.md emphasizes:

using the Visual Studio solution as the source of truth
keeping business logic out of controllers
preferring dependency injection
using async/await for I/O
preserving existing DTO, validation, and error handling styles

2. Frontend Structure
   Primary frontend location
   The frontend is located in web.
   Frontend stack
   The frontend uses:

React
Vite
TypeScript
Redux Toolkit
React Query
React Router
Formik/Yup
PrimeReact
Frontend package configuration
The frontend dependency and script setup is defined in package.json.

Frontend source layout
The frontend source tree is organized under src with folders such as:

app
components
features
shared
store
Frontend runtime configuration
The Vite development server is configured in vite.config.ts to run on port 3000 with host access enabled.

3. Dependency Flow
   End-to-end flow
   The architecture follows a conventional full-stack dependency flow:

The frontend client is served by the Vite development server.
The frontend interacts with backend endpoints through the API layer.
The ASP.NET Core host receives requests and routes them through the configured modules.
Module-specific application services handle business logic.
Infrastructure services are used for persistence, integration, and cross-cutting concerns.
Shared abstractions and patterns are reused by both modules and application services.
Backend dependency direction
Within the backend, the dependency direction is generally:

API layer → Application layer → Domain layer
Infrastructure layer supports the application/domain layers
Shared abstractions are consumed by modules and infrastructure
Frontend dependency direction
Within the frontend:

feature components depend on shared UI and state abstractions
API integration is handled through the frontend’s API layer
state management and query logic are kept separate from presentation

4. Project Modules
   Backend modules
   The backend currently includes two feature modules:

Identity

Supports identity-related workflows and infrastructure
Split into dedicated projects for API, application, contracts, domain, and infrastructure
Notifications

Supports notification-related workflows
Structured with application, domain, and infrastructure projects
Shared foundation
The shared project provides common primitives used across modules:

CQRS
domain entities
database abstractions
event infrastructure
result handling
Frontend modules
The frontend is organized by functional areas in the source tree:

app
components
features
shared
store
This structure supports a scalable UI architecture without forcing a rigid framework-specific pattern.

5. Naming Conventions
   The repository follows fairly conventional naming patterns consistent with .NET and React development.

Backend naming conventions
Project names use the MonolithTemplate prefix, for example:
MonolithTemplate.Identity.Api
MonolithTemplate.Identity.Application
MonolithTemplate.Notifications.Infrastructure
Folder names are descriptive and aligned with domain/module responsibilities.
Modules are grouped by domain names such as Identity and Notifications.
Frontend naming conventions
Source folders are descriptive and responsibility-based:
app
components
features
shared
store
File naming appears to follow a conventional TypeScript/React style with lowercase and descriptive structure.
Repository conventions
The guidance in AGENTS.md stresses:

inspecting the existing structure before changes
following existing naming conventions
making small, focused changes
avoiding renames or architectural shifts without discussion

6. Build and Development Process
   Backend build process
   The backend build flow is based on the .NET solution model:

The main solution is MonolithTemplate.sln
The backend solution is api.sln
Suggested backend commands from the repository guidance include:

dotnet restore MonolithTemplate.sln
dotnet build MonolithTemplate.sln
dotnet test
Frontend build process
The frontend build process is defined by the package scripts in package.json:

npm install
npm run dev
npm run build
npm run lint
npm run preview
Build behavior
The frontend uses TypeScript compilation and Vite bundling for production builds.
The backend is built as an ASP.NET Core application.
Containerized build process
The project also supports Docker-based development and deployment:

docker-compose.dev.yml
docker-compose.staging.yml
The README describes a local startup flow using Docker Compose, with services exposed on:

frontend: port 3000
API: port 8080
database: port 5432

7. Repository Guidance Summary
   The repository instructions in AGENTS.md and copilot-instructions.md emphasize:

preserving the existing architecture
making small, safe edits
following established code style
reusing existing patterns rather than introducing new ones
keeping backend controllers thin and business logic in appropriate services/handlers
reusing frontend components and existing API/query layers
This makes the repository a modular, convention-driven monorepo with clear separation between application, infrastructure, and delivery concerns.
