# Frontend application

This folder contains the React and Vite frontend for MonolithTemplate.

## Stack

- React 19
- TypeScript
- Vite
- React Router
- React Query
- Redux Toolkit
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

The frontend reads its API base URL from Vite environment files.

Local development:

- apps/web/.env.development
- current repository default: https://localhost:7263

Production build:

- apps/web/.env.production
- current repository default: http://localhost:8080

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
- npm run preview: preview the production bundle locally

## Useful local checks

```bash
cd apps/web
npm run lint
npm run build
```
