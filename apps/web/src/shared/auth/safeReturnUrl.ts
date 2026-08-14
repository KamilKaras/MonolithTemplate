const DEFAULT_AUTH_REDIRECT_PATH = "/home";

export const sanitizeReturnUrl = (candidate?: string | null) => {
  if (!candidate) {
    return DEFAULT_AUTH_REDIRECT_PATH;
  }

  const trimmed = candidate.trim();

  if (!trimmed.startsWith("/")) {
    return DEFAULT_AUTH_REDIRECT_PATH;
  }

  const pathEndIndex = trimmed.search(/[?#]/);
  const pathname =
    pathEndIndex === -1 ? trimmed : trimmed.slice(0, pathEndIndex);

  if (
    pathname.startsWith("//") ||
    pathname.startsWith("/login") ||
    pathname.includes("%2f") ||
    pathname.includes("%2F") ||
    pathname.includes("%5c") ||
    pathname.includes("%5C") ||
    pathname.includes("\\")
  ) {
    return DEFAULT_AUTH_REDIRECT_PATH;
  }

  return trimmed;
};

export const buildReturnUrl = (
  pathname: string,
  search: string,
  hash: string,
) => {
  return encodeURIComponent(`${pathname}${search}${hash}`);
};
