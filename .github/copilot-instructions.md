# GitHub Copilot Instructions

This repository is a .NET + React monorepo.

## Repository Guidance

For non-trivial, multi-file, architectural or cross-cutting work:

* Read and follow `AGENTS.md`.
* Use `docs/ARCHITECTURE.md` as the source of truth for architectural decisions.
* Inspect the existing implementation before proposing changes.
* Follow existing naming, code-style and architectural conventions.
* Prefer small, focused and reviewable changes.
* Avoid unrelated refactoring.
* Prefer existing abstractions over introducing parallel mechanisms.

Do not silently change:

* public API contracts
* authentication or authorization behavior
* database schema
* module boundaries
* major project/folder structure
* shared cross-module abstractions
* major frontend architecture

Explain the impact and request approval first.

## Backend

The backend is under `apps/api` and follows a modular monolith architecture.

* Use the solution/project structure as the source of truth.
* Preserve module boundaries.
* Keep endpoints/controllers thin and transport-focused.
* Keep business/use-case logic in the appropriate application or domain layer.
* Keep persistence and integrations in infrastructure.
* Prefer dependency injection and existing abstractions.
* Use async APIs for I/O-bound operations.
* Follow existing DTO, validation and error-handling conventions.
* Use the outbox for user-facing cross-module notification events unless synchronous delivery is explicitly required.

Do not modify public backend contracts silently.

## Frontend

The frontend is under `apps/web`.

Primary technologies include React, Vite, TypeScript, React Router and TanStack Query.

* Reuse the existing API client and frontend abstractions.
* Use TanStack Query as the source of truth for server state when applicable.
* Treat `/identity/me` as the source of truth for authenticated frontend session state unless repository architecture explicitly states otherwise.
* Do not guess backend contracts; inspect the relevant backend DTOs/endpoints when needed.
* Keep configuration in Vite environment variables such as `VITE_API_URL`.
* Avoid unnecessary global state, Context, effects and memoization.
* Preserve the existing styling approach unless a change is explicitly justified.
* Do not modify backend code during frontend-only work unless explicitly requested.

## Validation

Use repository-defined commands.

Backend:

```bash
dotnet build MonolithTemplate.sln
dotnet test MonolithTemplate.sln
```

Frontend:

```bash
npm --prefix apps/web run lint
npm --prefix apps/web run build
```

Also run frontend typecheck and tests when corresponding scripts exist.

Never claim validation succeeded unless the command actually succeeded.
