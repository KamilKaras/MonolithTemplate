import { useMutation } from "@tanstack/react-query";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { ConfirmEmailRequest } from "../../../../api/modules/identity/requests";

export const useConfirmEmail = () => {
  return useMutation({
    mutationFn: (request: ConfirmEmailRequest) =>
      identityEndpoints.confirmEmail(request),
  });
};
