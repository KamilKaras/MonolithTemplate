import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import type {
  ForgetPasswordRequest
} from "../../../../api/modules/Identity/requests";

export const useUserForgetPassword = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: (dto: ForgetPasswordRequest) => identityApi.forgetPassword(dto),
    onSuccess: () => {
      onSuccess?.();
    },
  });
};
