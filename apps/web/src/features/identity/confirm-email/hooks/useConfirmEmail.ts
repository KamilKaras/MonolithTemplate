import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/identity/identityEndpoints";
import type { ConfirmEmailRequest } from "../../../../api/modules/identity/requests";

export const useConfirmEmail = () => {
  return useMutation({
    mutationFn: (dto: ConfirmEmailRequest) => identityApi.confirmEmail(dto),
  });
};
