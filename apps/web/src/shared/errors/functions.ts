import type { ApiError } from "./types";

const DEFAULT_ERROR_MESSAGE = "Wystąpił nieoczekiwany błąd serwera.";

export const isApiError = (error: unknown): error is ApiError => {
  return typeof error === "object" && error !== null && "status" in error;
};

export const getErrorMessage = (error: unknown) => {
  if (isApiError(error)) {
    if (error.status === 403) {
      return error.detail ?? "Nie masz uprawnień do wykonania tej akcji.";
    }

    if (error.status === 401) {
      return error.detail ?? "Sesja wygasła. Zaloguj się ponownie.";
    }

    return error.detail ?? error.title ?? DEFAULT_ERROR_MESSAGE;
  }

  return DEFAULT_ERROR_MESSAGE;
};
