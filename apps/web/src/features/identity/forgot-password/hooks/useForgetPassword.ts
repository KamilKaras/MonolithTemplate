import { useMutation } from "@tanstack/react-query";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { ForgetPasswordRequest } from "../../../../api/modules/identity/requests";

export const useForgetPassword = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (dto: ForgetPasswordRequest) =>
      identityEndpoints.forgetPassword(dto),
    onSuccess: () => {
      onSuccess?.();
    },
  });
};
