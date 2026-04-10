import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/identity/identityEndpoints";

export const useLogout = () => {
  return useMutation({
    mutationFn: identityApi.logout,
  });
};
