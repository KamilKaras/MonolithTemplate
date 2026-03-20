import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import type { RegisterRequest } from "../../../../api/modules/Identity/requests";
import { toastService } from "../../../../shared/toast/ToastService";

export const useUserRegister = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (dto: RegisterRequest) => identityApi.register(dto),
    onSuccess: () => {
      toastService.success(
        "Konto zostało poprawnie utworzone, sprawdź skrzynkę email",
        5000,
      );
      onSuccess?.();
    },
  });
};
