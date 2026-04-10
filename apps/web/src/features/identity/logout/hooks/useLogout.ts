import { useMutation } from "@tanstack/react-query";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";

export const useLogout = () => {
  return useMutation({
    mutationFn: identityEndpoints.logout,
  });
};
