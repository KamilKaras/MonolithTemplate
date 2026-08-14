const DEFAULT_AUTH_REDIRECT_PATH = "/home";

export const sanitizeReturnUrl = (candidate?: string | null) => {
  if (!candidate) {
    return DEFAULT_AUTH_REDIRECT_PATH;
  }

  const trimmed = candidate.trim();

  if (!trimmed.startsWith("/")) {
    return DEFAULT_AUTH_REDIRECT_PATH;
  }

  if (trimmed.startsWith("//") || trimmed.startsWith("/login")) {
    return DEFAULT_AUTH_REDIRECT_PATH;
  }

  return trimmed;
};

export const buildReturnUrl = (pathname: string, search: string) => {
  return encodeURIComponent(`${pathname}${search}`);
};
