import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/identity/identityApi";

export const useLogout = () => {
  return useMutation({
    mutationFn: identityApi.logout,
  });
};
