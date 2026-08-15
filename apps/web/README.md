# Frontend application

This folder contains the React and Vite frontend for MonolithTemplate.

## Stack

- React 19
- TypeScript
- Vite
- React Router
- TanStack Query
- PrimeReact
- Formik and Yup

## Requirements

- Node.js 22
- npm

## Install and run

```bash
cd apps/web
npm ci
npm run dev
```

The Vite development server runs on port 3000 and listens on the network host.

## Environment variables

The frontend reads its API base URL from Vite build-time environment files.

Create local development env from the committed template:

```bash
cp apps/web/.env.example apps/web/.env.development
```

PowerShell:

```powershell
Copy-Item apps/web/.env.example apps/web/.env.development
```

Local development:

- apps/web/.env.development
- use the value from apps/web/.env.example or override per environment

Production build:

- apps/web/.env.production
- define an environment-specific value during build/deployment

Supported variable:

- VITE_API_URL: base URL for backend API requests

You can override VITE_API_URL in the shell or by editing the matching env file for the target environment.

## Auth and API basics

- Login posts credentials to /identity/login.
- The backend sets an access_token cookie on successful login.
- The frontend uses /identity/me as the source of truth for the current authenticated user.
- Logout clears the auth cookie through the backend and invalidates the current user query.
- Forgot-password and reset-password use /identity/forgot-password and /identity/reset-password.

Because authentication is cookie-based, make sure VITE_API_URL points to the backend origin you are actually running.

## Scripts

- npm run dev: start the Vite development server
- npm run build: type-check and create a production bundle
- npm run lint: run ESLint
- npm run test: run Vitest once in non-interactive mode
- npm run test:watch: run Vitest in watch mode
- npm run preview: preview the production bundle locally

## Useful local checks

```bash
cd apps/web
npm run lint
npm run test
npm run build
```
