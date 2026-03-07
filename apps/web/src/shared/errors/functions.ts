
export const getErrorMessage = (error: unknown) => {
  if (typeof error === "object" && error !== null && "title" in error) {
    return (error as { title: string }).title;
  }

  return "Wystąpił nieoczekiwany błąd.";
};
