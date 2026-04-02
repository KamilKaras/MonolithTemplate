import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import type { ConfirmEmailRequest } from "../../../../api/modules/Identity/requests";

export const useUserConfirmEmail = () => {
  return useMutation({
    mutationFn: (dto: ConfirmEmailRequest) => identityApi.confirmEmail(dto),
  });
};
