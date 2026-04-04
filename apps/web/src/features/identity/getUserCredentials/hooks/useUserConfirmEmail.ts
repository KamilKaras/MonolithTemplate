import { useQuery } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/Identity/IdentityApi";
import { useAppSelector } from "../../../../store/store";
import { USER_CREDENTIALS_QUERY_KEY } from "./types";

export const useUserCredentials = () => {
  const userId = useAppSelector((state) => state.auth.session?.userId);
  const token = useAppSelector((state) => state.auth.session?.userId);

  return useQuery({
    queryKey: [USER_CREDENTIALS_QUERY_KEY, userId],
    queryFn: () => identityApi.me(userId!),
    enabled: !!token && !!userId,
    retry: false,
  });
};
