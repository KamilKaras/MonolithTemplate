## 2. `.github/copilot-instructions.md`

Utwórz folder `.github`, jeśli nie istnieje, i wklej:

```md
# GitHub Copilot instructions

You are assisting with a .NET + React monorepo.

Always:

- Read `AGENTS.md` before making larger changes.
- Prefer small, safe edits.
- Follow existing code style.
- Preserve the current architecture.
- Ask before moving files, renaming projects, or changing public API contracts.
- Explain the plan before modifying multiple files.

For backend:

- Use ASP.NET Core conventions.
- Prefer dependency injection.
- Keep controllers thin.
- Put business logic in existing services/handlers.
- Use existing validation and error handling style.

For frontend:

- Use existing React patterns.
- Reuse components.
- Keep API calls in existing API/client/query layer.
- Use existing styling approach.

After changes, suggest relevant commands:

- `dotnet build MonolithTemplate.sln`
- `dotnet test`
- `npm run lint`
- `npm run build`
```
