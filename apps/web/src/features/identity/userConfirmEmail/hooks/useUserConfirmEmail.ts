import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import type { ConfirmEmailRequest } from "../../../../api/modules/Identity/requests";

export const useUserConfirmEmail = (onSuccess: () => void) => {
  return useMutation({
    mutationFn: (dto: ConfirmEmailRequest) => identityApi.confirmEmail(dto),
    onSuccess: onSuccess,
    onError: (error) => {
      console.log(error);
    },
  });
};
