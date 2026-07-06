# GitHub Copilot instructions

You are assisting with a .NET + React monorepo.

Always:

- Read AGENTS.md before larger or cross-cutting changes.
- Prefer small, safe edits that preserve the current architecture.
- Follow the existing code style and naming conventions.
- Ask before renaming major projects or folders, changing public API contracts, or changing database or auth behavior.
- Explain the planned file changes before editing multiple files.

Backend guidance:

- Use the solution files as the source of truth.
- Keep endpoints thin and transport-focused.
- Put business logic in existing handlers, services, or module application layers.
- Prefer dependency injection and existing shared abstractions.
- Use the outbox for user-facing cross-module notification events.

Frontend guidance:

- Reuse the existing API client, React Query hooks, and route structure.
- Treat the authenticated user query from /identity/me as the source of truth for session state.
- Keep configuration in Vite environment variables such as VITE_API_URL.

Useful validation commands:

- dotnet build MonolithTemplate.sln
- dotnet test MonolithTemplate.sln
- npm --prefix apps/web run lint
- npm --prefix apps/web run build
