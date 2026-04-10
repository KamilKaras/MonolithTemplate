import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/identity/identityApi";
import type { ConfirmEmailRequest } from "../../../../api/modules/identity/requests";

export const useUserConfirmEmail = () => {
  return useMutation({
    mutationFn: (dto: ConfirmEmailRequest) => identityApi.confirmEmail(dto),
  });
};
