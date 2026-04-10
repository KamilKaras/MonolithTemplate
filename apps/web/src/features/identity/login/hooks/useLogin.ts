import { useMutation } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { identityApi } from "../../../../api/modules/identity/identityApi";
import type { LoginRequest } from "../../../../api/modules/identity/requests";
import { setSession } from "../../../../store/slices/authSlice";
import { useAppDispatch } from "../../../../store/store";

export const useLogin = (onSuccess?: () => void) => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (dto: LoginRequest) => identityApi.login(dto),
    onSuccess: async (data) => {
      onSuccess?.();
      dispatch(setSession(data));
      onSuccess?.();
      navigate("/home");
    },
  });
};
