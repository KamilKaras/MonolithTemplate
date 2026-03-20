import { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import PageLoader from "../../../../../components/pages/PageLoader/PageLoader";
import { toastService } from "../../../../../shared/toast/ToastService";
import { useUserConfirmEmail } from "../../hooks/useUserConfirmEmail";

const ConfirmPage = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const userId = searchParams.get("userId");
  const token = searchParams.get("token");

  const { isPending, mutateAsync } = useUserConfirmEmail(() => {
    toastService.success(
      "Email został poprawnie potwierdzony, przejdź do logowania!",
    );
    navigate("/login");
  });

  useEffect(() => {
    if (!token || !userId) return;
    mutateAsync({ token, userId });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [token, userId]);

  return (
    <div>
      <PageLoader visible={isPending} />
    </div>
  );
};

export default ConfirmPage;
