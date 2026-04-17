import { useMutation } from "@tanstack/react-query";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { ForgetPasswordRequest } from "../../../../api/modules/identity/requests";
import { toastService } from "../../../../shared/toast/ToastService";

export const useForgetPassword = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (dto: ForgetPasswordRequest) =>
      identityEndpoints.forgetPassword(dto),
    onSuccess: () => {
      onSuccess?.();
      toastService.success(
        "Sprawdź skrzynkę email, aby zresetować hasło",
        5000,
      );
    },
  });
};
