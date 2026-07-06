import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { LoginRequest } from "../../../../api/modules/identity/requests";
import { USER_CREDENTIALS_QUERY_KEY } from "../../me/hooks/types";

export const useLogin = (onSuccess?: () => void) => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (request: LoginRequest) => identityEndpoints.login(request),
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: [USER_CREDENTIALS_QUERY_KEY],
      });
      onSuccess?.();
      navigate("/home");
    },
  });
};
