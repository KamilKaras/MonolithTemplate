import { useMutation } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { ResetPasswordRequest } from "../../../../api/modules/identity/requests";
import { toastService } from "../../../../shared/toast/ToastService";

export const useResetPassword = (onSuccess?: () => void) => {
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (request: ResetPasswordRequest) =>
      identityEndpoints.resetPassword(request),
    onSuccess: () => {
      onSuccess?.();
      toastService.success(
        "Hasło zostało zmienione pomyślnie. Zaloguj się ponownie",
        5000,
      );
      navigate("/login");
    },
  });
};
