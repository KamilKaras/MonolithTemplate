import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import type { LoginRequest } from "../../../../api/modules/Identity/requests";
import { setSession } from "../../../../store/slices/authSlice";
import { useAppDispatch } from "../../../../store/store";
import { USER_CREDENTIALS_QUERY_KEY } from "../../getUserCredentials/hooks/types";

export const useUserLogin = (onSuccess?: () => void) => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const client = useQueryClient();

  return useMutation({
    mutationFn: (dto: LoginRequest) => identityApi.login(dto),
    onSuccess: async (data) => {
      onSuccess?.();
      dispatch(setSession(data));
      await client.invalidateQueries({
        queryKey: [USER_CREDENTIALS_QUERY_KEY],
      });
      onSuccess?.();
      navigate("/home");
    },
  });
};
