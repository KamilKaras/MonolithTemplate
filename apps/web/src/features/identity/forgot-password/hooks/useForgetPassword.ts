import { useMutation } from "@tanstack/react-query";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { ForgetPasswordRequest } from "../../../../api/modules/identity/requests";
import { toastService } from "../../../../shared/toast/ToastService";

export const useForgetPassword = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (request: ForgetPasswordRequest) =>
      identityEndpoints.forgetPassword(request),
    onSuccess: () => {
      onSuccess?.();
      toastService.success("Jeśli konto istnieje, wysłaliśmy email", 5000);
    },
  });
};
