import type { QueryClient } from "@tanstack/react-query";
import { USER_CREDENTIALS_QUERY_KEY } from "../../features/identity/me/hooks/types";
import { toastService } from "../toast/ToastService";

const AUTH_ENDPOINTS_WITH_EXPECTED_401 = [
  "/identity/me",
  "/identity/login",
  "/identity/register",
  "/identity/forgot-password",
  "/identity/reset-password",
  "/identity/confirm-email",
  "/identity/logout",
];

const SESSION_NOTICE_COOLDOWN_MS = 5_000;

let authQueryClient: QueryClient | null = null;
let lastSessionNoticeAt = 0;

const hasExpectedUnauthorized = (requestUrl?: string) => {
  if (!requestUrl) {
    return false;
  }

  return AUTH_ENDPOINTS_WITH_EXPECTED_401.some((path) =>
    requestUrl.includes(path),
  );
};

export const registerSessionRecovery = (queryClient: QueryClient) => {
  authQueryClient = queryClient;
};

export const handleUnauthorizedResponse = (requestUrl?: string) => {
  if (!authQueryClient) {
    return;
  }

  const hasExistingSession =
    authQueryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY]) !== undefined;

  authQueryClient.setQueryData([USER_CREDENTIALS_QUERY_KEY], undefined);

  void authQueryClient.invalidateQueries({
    queryKey: [USER_CREDENTIALS_QUERY_KEY],
  });

  if (!hasExistingSession || hasExpectedUnauthorized(requestUrl)) {
    return;
  }

  const now = Date.now();
  if (now - lastSessionNoticeAt < SESSION_NOTICE_COOLDOWN_MS) {
    return;
  }

  lastSessionNoticeAt = now;
  toastService.info("Sesja wygasła. Zaloguj się ponownie.");
};
