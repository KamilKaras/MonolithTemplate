import { useMutation } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import type { LoginRequest } from "../../../../api/modules/identity/requests";
import { setSession } from "../../../../store/slices/authSlice";
import { useAppDispatch } from "../../../../store/store";

export const useLogin = (onSuccess?: () => void) => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (request: LoginRequest) => identityEndpoints.login(request),
    onSuccess: async (data) => {
      onSuccess?.();
      dispatch(setSession(data));
      onSuccess?.();
      navigate("/home");
    },
  });
};
