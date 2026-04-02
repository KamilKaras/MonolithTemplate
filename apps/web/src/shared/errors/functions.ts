export const getErrorMessage = (error: unknown) => {
  if (typeof error === "object" && error !== null && "detail" in error) {
    return (error as { detail: string }).detail;
  }

  return "Wystąpił nieoczekiwany błąd.";
};
