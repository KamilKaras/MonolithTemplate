import type { QueryClient } from "@tanstack/react-query";
import type { InternalAxiosRequestConfig } from "axios";
import { USER_CREDENTIALS_QUERY_KEY } from "../../features/identity/me/hooks/types";
import { toastService } from "../toast/ToastService";

const AUTH_ENDPOINTS_WITH_EXPECTED_401 = [
  "/identity/login",
  "/identity/register",
  "/identity/forgot-password",
  "/identity/reset-password",
  "/identity/confirm-email",
  "/identity/logout",
];

const SESSION_NOTICE_COOLDOWN_MS = 5_000;
const AUTH_CHECK_PATH = "/identity/me";

let authQueryClient: QueryClient | null = null;
let lastSessionNoticeAt = 0;

const getRequestPath = (requestConfig?: InternalAxiosRequestConfig) => {
  const requestUrl = requestConfig?.url;

  if (!requestUrl) {
    return null;
  }

  try {
    const resolvedUrl = new URL(
      requestUrl,
      requestConfig?.baseURL ?? window.location.origin,
    );

    return resolvedUrl.pathname;
  } catch {
    return null;
  }
};

const hasExpectedUnauthorized = (requestPath?: string | null) => {
  if (!requestPath) {
    return false;
  }

  if (requestPath === AUTH_CHECK_PATH) {
    return true;
  }

  return AUTH_ENDPOINTS_WITH_EXPECTED_401.includes(requestPath);
};

export const registerSessionRecovery = (queryClient: QueryClient) => {
  authQueryClient = queryClient;
};

export const handleUnauthorizedResponse = (
  requestConfig?: InternalAxiosRequestConfig,
) => {
  if (!authQueryClient) {
    return;
  }

  const requestPath = getRequestPath(requestConfig);

  if (requestPath === AUTH_CHECK_PATH) {
    return;
  }

  if (hasExpectedUnauthorized(requestPath)) {
    return;
  }

  const hasExistingSession =
    authQueryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY]) !== undefined;

  authQueryClient.removeQueries({
    queryKey: [USER_CREDENTIALS_QUERY_KEY],
  });

  if (!hasExistingSession) {
    return;
  }

  const now = Date.now();
  if (now - lastSessionNoticeAt < SESSION_NOTICE_COOLDOWN_MS) {
    return;
  }

  lastSessionNoticeAt = now;
  toastService.info("Sesja wygasła. Zaloguj się ponownie.");
};
