# AGENTS.md

## Project overview

This is a monorepo with a .NET backend and React frontend.

Main folders:

- `apps/api` or `api` - ASP.NET Core backend
- `apps/web` or `web` - React frontend
- `infra` - infrastructure, Docker, deployment
- `.github` - GitHub Actions and repository automation

## General rules

Before changing code:

1. Inspect the existing structure.
2. Follow existing naming conventions.
3. Make small, focused changes.
4. Do not rename folders without asking.
5. Do not introduce new architecture patterns without asking.
6. Explain what files will be changed before editing.
7. Run or suggest relevant build/test commands after changes.

## Backend rules

- Use the Visual Studio solution as the source of truth.
- Keep business logic out of controllers.
- Prefer dependency injection.
- Use async/await for I/O.
- Follow existing DTO, validation and error handling style.
- Do not change public API contracts without explaining the impact.

Backend commands:

```bash
dotnet restore MonolithTemplate.sln
dotnet build MonolithTemplate.sln
dotnet test

## Architecture review mode

For every non-trivial change, act as a software architect first.

Before implementation:
- explain the design,
- check module boundaries,
- verify dependency direction,
- identify risks,
- suggest tests,
- wait for approval if architecture is affected.

Use `docs/ARCHITECTURE.md` as the source of truth.
```
