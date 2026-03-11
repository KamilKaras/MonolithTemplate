import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import type { LoginRequest } from "../../../../api/modules/Identity/requests";

export const useUserLogin = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (dto: LoginRequest) => identityApi.login(dto),
    onSuccess: () => {
      onSuccess?.();
    },
  });
};
