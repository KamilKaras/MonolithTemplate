import { useMe } from "../../features/identity/me/hooks/useMe";

export const useAuth = () => {
  const { data, isLoading, isError } = useMe();

  return {
    isLoading,
    isAuthenticated: !!data && !isError,
    user: data ?? null,
  };
};
