import { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { toastService } from "../toast/ToastService";

export const useUserParams = () => {
  const [searchParams] = useSearchParams();

  const navigate = useNavigate();

  const userId = searchParams.get("userId");
  const token = searchParams.get("token");

  useEffect(() => {
    if (token && userId) {
      return;
    }

    toastService.info("Nie udało się pobrać parametrów!.\nProsimy o kontakt!");
    navigate("/problem", { replace: true });
  }, [navigate, token, userId]);

  return {
    token,
    userId,
  };
};
