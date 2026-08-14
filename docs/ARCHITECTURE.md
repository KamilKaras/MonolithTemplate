# MonolithTemplate Architecture

This document is the repository source of truth for backend and frontend architecture. It describes the verified architecture currently implemented in code and separates it from known technical debt and intentional exceptions.

## 1. System overview

This repository is a .NET + React monorepo with:

- an ASP.NET Core backend under `apps/api`
- a Vite + React frontend under `apps/web`
- PostgreSQL-backed persistence for the Identity module
- Docker and deployment configuration under `infra`

The backend is a modular monolith. The host project composes feature modules and shared infrastructure. The frontend is a standard React app using a shared API client, route guards, and TanStack Query for server state.

## 2. Architectural invariants

The following are the repository-wide invariants that should be preserved unless a concrete requirement justifies a change:

- The backend is a modular monolith with a composition root in `MonolithTemplate.Api`.
- Endpoint and transport code should remain thin and delegate to handlers or application services.
- Persistence and external I/O stay in infrastructure.
- Cross-module communication happens through contracts and integration events, not by direct reference to another module's implementation.
- Notification side effects are persisted through the outbox before dispatch.
- Frontend session state is derived from `/identity/me` and is treated as the server-authoritative user state.
- Vite `VITE_*` variables are build-time frontend configuration unless the project implements a separate runtime configuration mechanism.

## 3. Backend architecture

### 3.1 Project graph

The verified project references are:

- `MonolithTemplate.Api`
  - references `MonolithTemplate.Identity.Api`
  - references `MonolithTemplate.Identity.Infrastructure`
  - references `MonolithTemplate.Notifications.Infrastructure`
- `MonolithTemplate.Shared`
  - is shared infrastructure used by module and infrastructure projects
- `MonolithTemplate.Identity.Api`
  - references `MonolithTemplate.Shared`
  - references `MonolithTemplate.Identity.Application`
- `MonolithTemplate.Identity.Application`
  - references `MonolithTemplate.Shared`
  - references `MonolithTemplate.Identity.Contracts`
  - references `MonolithTemplate.Identity.Domain`
- `MonolithTemplate.Identity.Infrastructure`
  - references `MonolithTemplate.Shared`
  - references `MonolithTemplate.Identity.Application`
  - references `MonolithTemplate.Identity.Domain`
- `MonolithTemplate.Notifications.Infrastructure`
  - references `MonolithTemplate.Shared`
  - references `MonolithTemplate.Notifications.Application`
- `MonolithTemplate.Notifications.Application`
  - references `MonolithTemplate.Identity.Contracts`
  - references `MonolithTemplate.Notifications.Domain`
- `MonolithTemplate.Notifications.Domain`
  - references `MonolithTemplate.Shared`

This architecture is best understood as a composition-root-based modular monolith. The host composes modules directly, and strict layered purity is not enforced at every boundary.

### 3.2 Module ownership

Identity module (`apps/api/Modules/Identity`):

- `MonolithTemplate.Identity.Api`: endpoint registration and HTTP surface
- `MonolithTemplate.Identity.Application`: commands, queries, handlers, validation
- `MonolithTemplate.Identity.Contracts`: cross-module integration event contracts
- `MonolithTemplate.Identity.Domain`: identity domain model and identity rules
- `MonolithTemplate.Identity.Infrastructure`: EF Core, auth, persistence, outbox persistence, and UoW behavior

Notifications module (`apps/api/Modules/Notifications`):

- `MonolithTemplate.Notifications.Application`: event handlers and email composition logic
- `MonolithTemplate.Notifications.Domain`: email/value-object domain types
- `MonolithTemplate.Notifications.Infrastructure`: SMTP and frontend URL infrastructure

The Notifications module intentionally has no dedicated API project or Contracts project. This is a current implementation shape, not a deficiency to be corrected by default.

### 3.3 Shared infrastructure and CQRS

The shared library (`apps/api/MonolithTemplate.Shared`) contains reusable cross-cutting infrastructure:

- `Dispatcher` and `IDispatcher`
- `IRequestHandler<>` registration
- `IIntegrationEvent` and `IIntegrationEventHandler<>`
- `IEventBus` and in-process dispatch
- `IOutbox`, `Outbox`, `OutboxProcessor`, `OutboxModule<TDbContext>`
- `UnitOfWork` and `UnitOfWorkBehavior<TRequest, TResponse, TUow>`
- EF registration helpers

This library is infrastructure concern support, not a domain layer.

### 3.4 Outbox and event delivery

The repository implements outbox-driven cross-module communication:

- command handlers enqueue integration events into `IOutbox`
- `UnitOfWorkBehavior` persists queued events to the module `OutboxMessages` table as part of the same transaction
- `OutboxProcessor` drains pending messages and dispatches them through the in-process `EventBus`
- notification handlers in `Notifications.Application` are responsible for sending emails

The in-process event bus is used for outbox processing and internal event dispatch. User-facing notification events are not published directly from request handlers without passing through the outbox.

### 3.5 Persistence and migrations

Persistence is handled through EF Core and one PostgreSQL deployment for the application.

- `AddIdentityModule` registers `MyIdentityDbContext` with PostgreSQL using Npgsql.
- startup runs migrations automatically via `RunMigrations()`
- migration discovery scans assemblies for `DbContext` implementations and calls `Database.MigrateAsync()` for each one

Identity owns the `Identity` schema and the outbox storage in its database context.

## 4. Frontend architecture

The frontend implementation under `apps/web` currently includes:

- React 19
- Vite
- TypeScript
- React Router
- TanStack Query
- Axios
- PrimeReact
- Formik + Yup

The source tree is organized into:

- `app`: routing and app bootstrap
- `components`: page and reusable presentation components
- `features`: feature-specific logic, forms, and hooks
- `shared`: API client, providers, common hooks, and error handling

The active server-state and session ownership is TanStack Query.

### 4.1 Frontend request flow

The frontend uses:

- `apps/web/src/api/client.ts` with `withCredentials: true`
- `import.meta.env.VITE_API_URL` to build the API base URL
- `identityEndpoints` for `/identity/me`, `/identity/login`, `/identity/logout`, `/identity/register`, `/identity/forgot-password`, `/identity/reset-password`, and `/identity/confirm-email`
- `useLogin` to invalidate current-user state after successful login
- `RequireAuth` and `RequireGuest` to gate navigation based on `/identity/me`

## 5. Authentication flow

The actual session flow is:

1. The browser sends credentials to `POST /identity/login`.
2. The backend validates the credentials and sets an `access_token` cookie.
3. The cookie is read by JWT bearer authentication via `OnMessageReceived` in the host auth configuration.
4. The frontend calls `GET /identity/me` with credentials included via the browser cookie.
5. The response from `/identity/me` is treated as the authoritative user state.
6. `POST /identity/logout` clears the cookie and invalidates the query cache.
7. Protected and guest-only routes are resolved through route guards that call the authenticated-user query.

There is no separate refresh-token or alternative frontend auth store in the current implementation.

## 6. Configuration and deployment topology

### 6.1 Frontend configuration

The frontend uses Vite environment variables for frontend build-time configuration:

- `apps/web/.env.development`
- `apps/web/.env.production`
- `VITE_API_URL` is read in `apps/web/src/api/client.ts`

This is build-time frontend configuration. It is not a runtime backend configuration mechanism and not a general-purpose application configuration system.

### 6.2 Backend configuration

The backend reads standard ASP.NET Core configuration from:

- `apps/api/MonolithTemplate.Api/appsettings.json`
- `apps/api/MonolithTemplate.Api/appsettings.Development.json`

Key configuration includes:

- `ConnectionStrings:Default`
- `Frontend:BaseUrl`
- `SmtpSettings:*`
- `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`

### 6.3 Docker and ports

The current Docker topology is:

- PostgreSQL on `5432`
- API on `8080`
- frontend on `3000`

The host CORS policy is built from `Frontend:BaseUrl` and falls back to localhost origins when not explicitly configured.

## 7. Dependency Rules

The dependency rules below are the current architectural guardrails for the repository.

### 7.1 Allowed dependencies

- `MonolithTemplate.Api` may depend on module API and infrastructure projects as the composition root.
- `Shared` may be depended on by any backend project that requires cross-cutting infrastructure.
- `Identity.Api` may depend on `Identity.Application` and `Shared`.
- `Identity.Application` may depend on `Identity.Domain`, `Identity.Contracts`, and `Shared`.
- `Identity.Infrastructure` may depend on `Identity.Application`, `Identity.Domain`, and `Shared`.
- `Identity.Contracts` may depend on `Shared` only when needed for shared contract types.
- `Identity.Domain` may depend on `Shared` only for shared domain primitives.
- `Notifications.Application` may depend on `Notifications.Domain`, `Identity.Contracts`, and `Shared`.
- `Notifications.Infrastructure` may depend on `Notifications.Application`, `Notifications.Domain`, and `Shared`.
- `Notifications.Domain` may depend on `Shared` only for shared domain primitives.

### 7.2 Forbidden dependencies

- No module may directly depend on another module's Domain, Application, Infrastructure, or API implementation.
- Cross-module communication must happen through contracts, integration events, and shared infrastructure boundaries.
- Domain code must not depend on infrastructure or transport concerns.
- Infrastructure must not leak into application or domain code.
- Host code must not bypass module boundaries by reaching into a module's application logic except through that module's API or registered services.

### 7.3 Cross-module communication rule

Modules should communicate by contract and event boundaries only.

This repository uses `Identity.Contracts` for integration event contracts and the shared outbox/event mechanisms for asynchronous cross-module delivery. Modules must not directly depend on another module's concrete domain objects, handlers, controllers, or infrastructure implementation.

## 8. Known Technical Debt and Open Architectural Decisions

These items are current-state notes, not planned changes. They are only relevant when a concrete requirement appears.

- The Notifications module intentionally does not currently include a dedicated API project or Contracts project.
- The host composition model is a modular monolith, but it does not enforce a strict layered project graph at every boundary.
- The frontend uses Vite environment variables for build-time configuration and should not be described as a generic runtime configuration mechanism.

## 9. Current-state summary

The repository is best described as:

- a modular monolith backend with a composition root and shared infrastructure
- a module-based Identity implementation with clear API/application/domain/infrastructure ownership
- a Notifications module built around integration events and email infrastructure
- a React/Vite frontend using TanStack Query as the active server-state source of truth
- cookie-based JWT authentication with `/identity/me` as the authoritative user session source

This document intentionally focuses on architecture and architectural invariants rather than generic development workflow rules, which belong in AGENTS.md.
