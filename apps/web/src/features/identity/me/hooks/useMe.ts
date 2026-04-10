import { useQuery } from "@tanstack/react-query";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import { USER_CREDENTIALS_QUERY_KEY } from "./types";

export const useMe = () => {
  return useQuery({
    queryKey: [USER_CREDENTIALS_QUERY_KEY],
    queryFn: () => identityEndpoints.me(),
    retry: false,
    refetchOnWindowFocus: false,
  });
};
