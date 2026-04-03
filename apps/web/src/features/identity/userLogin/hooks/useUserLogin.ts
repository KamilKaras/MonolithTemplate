import { useMutation } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import type { LoginRequest } from "../../../../api/modules/Identity/requests";
import { setToken } from "../../../../store/slices/authSlice";
import { useAppDispatch } from "../../../../store/store";

export const useUserLogin = (onSuccess?: () => void) => {
  const dispatch = useAppDispatch();

  return useMutation({
    mutationFn: (dto: LoginRequest) => identityApi.login(dto),
    onSuccess: (data) => {
      onSuccess?.();
      dispatch(setToken(data.accessToken));
    },
  });
};
