import { useQuery } from "@tanstack/react-query";
import { USER_CREDENTIALS_QUERY_KEY } from "./types";
import { identityApi } from "../../../../api/modules/identity/identityApi";

export const useMe = () => {
  return useQuery({
    queryKey: [USER_CREDENTIALS_QUERY_KEY],
    queryFn: () => identityApi.me(),
    retry: false,
    refetchOnWindowFocus: false,
  });
};
