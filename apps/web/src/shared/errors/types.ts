export type ApiError = {
  status?: number;
  title: string;
  detail?: string;
  errors?: Record<string, string[]>;
};

export type ProblemDetails = {
  title?: string;
  message?: string;
  detail?: string;
  errors?: Record<string, string[]>;
};
