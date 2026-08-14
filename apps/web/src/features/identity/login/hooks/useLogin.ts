import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useLocation, useNavigate, useSearchParams } from "react-router-dom";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { LoginRequest } from "../../../../api/modules/identity/requests";
import { sanitizeReturnUrl } from "../../../../shared/auth/safeReturnUrl";
import { USER_CREDENTIALS_QUERY_KEY } from "../../me/hooks/types";

export const useLogin = (onSuccess?: () => void) => {
  const navigate = useNavigate();
  const location = useLocation();
  const [searchParams] = useSearchParams();
  const queryClient = useQueryClient();

  const getPostLoginPath = () => {
    const state = location.state as
      | { from?: { pathname?: string; search?: string } }
      | undefined;

    const pathname = state?.from?.pathname;
    const search = state?.from?.search ?? "";

    const statePath = pathname ? `${pathname}${search}` : null;
    const returnUrl = searchParams.get("returnUrl");

    if (returnUrl) {
      return sanitizeReturnUrl(returnUrl);
    }

    return sanitizeReturnUrl(statePath);
  };

  return useMutation({
    mutationFn: (request: LoginRequest) => identityEndpoints.login(request),
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: [USER_CREDENTIALS_QUERY_KEY],
      });
      onSuccess?.();
      navigate(getPostLoginPath(), { replace: true });
    },
  });
};
