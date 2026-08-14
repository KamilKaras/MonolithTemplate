import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import { isApiError } from "../../../../shared/errors/functions";
import { toastService } from "../../../../shared/toast/ToastService";
import { USER_CREDENTIALS_QUERY_KEY } from "../../../identity/me/hooks/types";

export const useLogout = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: identityEndpoints.logout,
    onSuccess: async () => {
      await queryClient.removeQueries({
        queryKey: [USER_CREDENTIALS_QUERY_KEY],
      });
      navigate("/login");
    },
    onError: (error) => {
      if (isApiError(error) && error.status === 401) {
        navigate("/login", { replace: true });
        return;
      }

      toastService.error("Błąd z połączeniem do serwera!");
    },
  });
};
