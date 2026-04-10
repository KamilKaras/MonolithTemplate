import { useMutation } from "@tanstack/react-query";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { RegisterRequest } from "../../../../api/modules/identity/requests";
import { toastService } from "../../../../shared/toast/ToastService";

export const useRegister = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (dto: RegisterRequest) => identityEndpoints.register(dto),
    onSuccess: () => {
      toastService.success(
        "Konto zostało poprawnie utworzone, sprawdź skrzynkę email",
        5000,
      );
      onSuccess?.();
    },
  });
};
