import { useQuery } from "@tanstack/react-query";
import { identityApi } from "../../../../api/modules/identity/identityEndpoints";
import { USER_CREDENTIALS_QUERY_KEY } from "./types";

export const useMe = () => {
  return useQuery({
    queryKey: [USER_CREDENTIALS_QUERY_KEY],
    queryFn: () => identityApi.me(),
    retry: false,
    refetchOnWindowFocus: false,
  });
};
