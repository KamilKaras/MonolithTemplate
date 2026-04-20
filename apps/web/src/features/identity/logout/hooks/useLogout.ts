import { useMutation } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { identityEndpoints } from "../../../../api/modules/identity/identityEndpoints";
import { toastService } from "../../../../shared/toast/ToastService";
import { clearSession } from "../../../../store/slices/authSlice";
import { useAppDispatch } from "../../../../store/store";

export const useLogout = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  return useMutation({
    mutationFn: identityEndpoints.logout,
    onSuccess: () => {
      navigate("/login");
      dispatch(clearSession());
    },
    onError: () => {
      toastService.error("Błąd z połączeniem do serwera!");
    },
  });
};
