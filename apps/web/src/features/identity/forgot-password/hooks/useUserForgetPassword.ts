import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/identity/identityApi";
import type { ForgetPasswordRequest } from "../../../../api/modules/identity/requests";

export const useUserForgetPassword = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (dto: ForgetPasswordRequest) => identityApi.forgetPassword(dto),
    onSuccess: () => {
      onSuccess?.();
    },
  });
};
