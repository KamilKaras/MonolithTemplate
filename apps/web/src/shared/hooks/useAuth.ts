import { useMe } from "../../features/identity/me/hooks/useMe";
import { isApiError } from "../errors/functions";

export const useAuth = () => {
  const { data, error, isLoading } = useMe();

  const isUnauthorized = isApiError(error) && error.status === 401;
  const hasAuthCheckError = !!error && !isUnauthorized;
  const isAuthenticated = !!data && !hasAuthCheckError && !isUnauthorized;

  return {
    isLoading,
    isAuthenticated,
    hasAuthCheckError,
    isUnauthorized,
    user: data ?? null,
  };
};
