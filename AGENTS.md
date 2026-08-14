# AGENTS.md

## Project Overview

This repository is a monorepo containing a .NET backend and a React frontend.

Main areas:

```text
apps/
  api/        ASP.NET Core backend
  web/        React + Vite frontend

infra/        Infrastructure, Docker and deployment configuration
.github/      GitHub Actions, Copilot configuration and repository automation
docs/         Architecture and project documentation
```

The backend follows a modular monolith architecture.

The frontend is a React application integrated with the backend through HTTP APIs.

---

# Sources of Truth

Use the following sources in this order when making technical decisions:

1. Existing working implementation
2. `AGENTS.md`
3. `docs/ARCHITECTURE.md`
4. Existing module or feature conventions
5. Tests
6. Public API contracts and DTOs
7. Tool or framework best practices

Do not replace established repository conventions with personal preferences unless the existing approach creates a concrete problem.

For architecture-affecting decisions, use `docs/ARCHITECTURE.md` as the primary architectural reference.

---

# General Engineering Rules

Before changing code:

1. Inspect the relevant implementation.
2. Understand surrounding code and dependencies.
3. Follow existing naming and code-style conventions.
4. Preserve existing behavior unless the requested change requires otherwise.
5. Prefer small, focused and reviewable changes.
6. Avoid unrelated refactoring.
7. Prefer existing abstractions over creating parallel mechanisms.
8. Do not introduce dependencies without a concrete benefit.
9. Do not hide existing errors or warnings.
10. Clearly distinguish pre-existing problems from problems introduced by the current change.
11. Do not claim validation succeeded unless the relevant command actually succeeded.

Prefer:

* simple over clever
* explicit over magical
* incremental changes over rewrites
* existing conventions over personal preference
* maintainability over unnecessary abstraction
* current requirements over hypothetical future needs

---

# Change Planning

For small and localized changes, inspect the code and proceed directly.

For non-trivial or multi-file changes, briefly identify before implementation:

* the current problem
* the proposed approach
* affected areas
* relevant risks

Do not require unnecessary approval for routine implementation or refactoring that stays within the established architecture.

---

# Changes Requiring Approval

Ask for explicit approval before making changes that affect:

* public API contracts
* authentication or authorization behavior
* database schema
* shared persistence conventions
* module boundaries
* major project or folder structure
* shared abstractions used across multiple modules
* core architectural patterns
* externally consumed event contracts
* major frontend architecture
* replacement of a major library or framework

When such a change appears necessary:

1. Explain the current limitation.
2. Propose the smallest safe change.
3. Describe affected areas.
4. Explain compatibility or migration impact.
5. Wait for approval before implementing that specific change.

---

# Backend

## Backend Scope

The backend is located primarily under:

```text
apps/api
```

Use the Visual Studio solution and project references as the source of truth for backend structure.

Typical backend responsibilities include:

* ASP.NET Core APIs
* application logic
* domain logic
* infrastructure
* persistence
* EF Core
* authentication and authorization
* integrations
* backend tests

---

## Backend Architecture

Treat the backend as a modular monolith.

Preserve module boundaries under the existing module structure, including `apps/api/Modules` where applicable.

Respect the intended dependency direction between:

```text
API / Presentation
        ↓
Application
        ↓
Domain

Infrastructure → supports application/domain through established abstractions
```

Follow the actual architecture documented in `docs/ARCHITECTURE.md` when it differs from this simplified representation.

Keep:

* endpoints and controllers thin
* business/use-case orchestration in the application layer
* domain behavior in the domain layer where appropriate
* persistence and external integration concerns in infrastructure

Avoid:

* business logic in controllers or endpoints
* direct cross-module persistence access
* infrastructure concerns leaking into domain code
* circular project dependencies
* generic helper layers created for single use cases
* unnecessary base classes
* speculative abstractions

---

## Backend API

When working with APIs:

* follow existing routing conventions
* follow existing DTO conventions
* follow existing validation conventions
* preserve existing error-response conventions
* preserve status-code behavior unless intentionally changed
* use asynchronous APIs for I/O-bound work
* propagate `CancellationToken` when consistent with surrounding code

Public API contracts must not be changed silently.

Public contracts include:

* routes
* HTTP methods
* request payloads
* response payloads
* status codes
* authentication requirements
* authorization requirements

---

## Backend Persistence

When working with EF Core:

* follow existing DbContext conventions
* follow existing entity configuration patterns
* avoid unnecessary round trips
* avoid N+1 query patterns
* avoid loading data that is not required
* prefer projections where appropriate
* use asynchronous database operations
* preserve transaction and concurrency behavior

Do not generate or modify migrations unless the requested change requires a schema change.

---

## Backend Integrations

Reuse existing integration abstractions.

Consider:

* cancellation
* retries
* timeouts
* idempotency
* error handling
* structured logging

Do not log secrets, credentials or authentication tokens.

Use the outbox for user-facing cross-module notification events unless synchronous delivery is explicitly required.

---

# Frontend

## Frontend Scope

The frontend is located primarily under:

```text
apps/web
```

The primary stack is:

* React
* Vite
* TypeScript
* React Router
* TanStack Query / React Query
* the styling system already present in the application

The .NET backend is the source of truth for API behavior.

Frontend work may inspect backend contracts but should not modify backend code unless explicitly requested.

---

## Frontend Architecture

Preserve clear responsibility between:

* application/bootstrap configuration
* routes
* pages
* features
* reusable UI
* API infrastructure
* hooks
* types
* utilities

Do not introduce architecture patterns such as Feature-Sliced Design, Clean Architecture, Redux, Zustand or similar mechanisms unless the current project has a demonstrated need.

Avoid:

* generic folders becoming dumping grounds
* duplicated API abstractions
* unnecessary Context
* unnecessary global state
* server state copied into client state without reason
* duplicated business logic
* circular imports
* abstractions created only for hypothetical reuse

---

## React

Follow modern React practices.

Prefer:

* functional components
* component composition
* focused responsibilities
* derived values instead of duplicated state
* local state for local UI concerns
* URL state for navigation/shareable state
* TanStack Query for server state

Avoid:

* side effects during render
* unnecessary `useEffect`
* effects used for derived state
* unnecessary `useMemo`
* unnecessary `useCallback`
* excessive prop drilling
* giant components
* premature memoization

Extract hooks when they improve reuse, readability, responsibility separation or testability.

---

## TypeScript

Prefer strong and practical typing.

Avoid:

* `any`
* unnecessary assertions
* unsafe casting
* duplicated DTO definitions
* overly broad types
* unnecessary generic complexity

Handle nullable and optional values explicitly.

Frontend API types must reflect verified backend contracts.

---

## Frontend API Integration

Use the existing API abstraction.

Do not create ad-hoc HTTP calls inside components if the project already provides an API client or established data-access layer.

Verify backend behavior rather than guessing:

* endpoint path
* HTTP method
* request payload
* response payload
* authentication
* authorization
* validation errors
* nullable fields
* pagination
* filtering
* sorting

Use Vite environment variables for frontend configuration.

Example:

```text
VITE_API_URL
```

Never commit secrets into frontend code.

---

## Authentication

Treat the authenticated user query from:

```text
/identity/me
```

as the source of truth for frontend session state unless the backend contract or architecture documentation explicitly defines another mechanism.

Frontend authorization is only a UX concern.

Backend authorization remains the security boundary.

Keep handling of:

* 401
* 403
* login
* logout
* session initialization
* protected routes
* post-login redirects

consistent across the application.

---

## TanStack Query

Use TanStack Query as the primary owner of server state when already configured.

Maintain consistent:

* query keys
* query functions
* mutations
* invalidation
* stale-time behavior
* error states
* loading states

Avoid duplicating query data into Context or another global state store without a concrete reason.

---

## Frontend UX and Accessibility

Data-driven views should intentionally handle applicable states:

* loading
* empty
* success
* validation error
* API error
* unauthorized
* forbidden
* not found

Use semantic HTML.

Ensure:

* inputs have labels
* icon-only controls have accessible names
* keyboard interaction works
* focus states remain visible
* buttons and links use appropriate semantic elements

---

## Frontend Template Cleanup

The frontend may contain code inherited from a template.

Actively identify:

* demo pages
* mock data
* sample widgets
* placeholder content
* unused routes
* unused components
* unused hooks
* unused utilities
* unused assets
* unused styles
* template branding
* obsolete dependencies
* commented example code

Do not remove template code until its usage has been verified.

Classify cleanup candidates as:

* safe to remove
* requires review
* useful infrastructure to keep

---

# Architecture Review

For architecture-affecting work:

1. Inspect the current implementation.
2. Read `docs/ARCHITECTURE.md`.
3. Identify the concrete architectural problem.
4. Verify module and dependency boundaries.
5. Propose the smallest safe solution.
6. Identify affected areas.
7. Identify realistic risks.
8. Suggest validation.

Do not change architecture merely because another pattern is more fashionable or theoretically cleaner.

A valid architecture review may conclude that no structural change is necessary.

---

# Code Review

Reviews should prioritize:

1. correctness
2. security
3. data integrity
4. regression risk
5. public contract compatibility
6. architecture
7. reliability
8. maintainability
9. meaningful performance concerns
10. minor readability issues

Prefer high-confidence findings.

Do not manufacture issues simply to make a review appear comprehensive.

Review comments should explain:

* what is wrong
* why it matters
* where it occurs
* the smallest practical correction

---

# Validation

Use repository-defined scripts whenever possible.

## Backend

Typical validation commands:

```bash
dotnet restore MonolithTemplate.sln
dotnet build MonolithTemplate.sln
dotnet test MonolithTemplate.sln
```

More focused project or test commands may be used when sufficient.

## Frontend

Inspect `apps/web/package.json` and use the configured package manager.

Typical validation commands include:

```bash
npm --prefix apps/web run typecheck
npm --prefix apps/web run lint
npm --prefix apps/web run test
npm --prefix apps/web run build
```

Run only scripts that actually exist.

Do not treat a missing optional script as a failed validation.

---

# Final Response Expectations

After completing implementation work, briefly report:

* what changed
* why it changed
* important files or areas affected
* validation performed
* validation failures, if any
* remaining risks or relevant technical debt

For review-only tasks, do not modify code unless explicitly requested.

For audit-only tasks, analyze before proposing major refactoring.

---

# Final Engineering Principle

Do not optimize for:

* the number of changed files
* the amount of new architecture
* the number of abstractions
* stylistic perfection

Optimize for:

* correctness
* clarity
* maintainability
* stable boundaries
* predictable behavior
* safe future changes
