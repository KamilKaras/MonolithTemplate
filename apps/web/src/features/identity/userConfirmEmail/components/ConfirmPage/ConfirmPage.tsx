import { Card } from "primereact/card";
import { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import PageLoader from "../../../../../components/molecules/PageLoader/PageLoader";
import { toastService } from "../../../../../shared/toast/ToastService";
import { useUserConfirmEmail } from "../../hooks/useUserConfirmEmail";
import "./confirm-page.scss";

const ConfirmPage = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const userId = searchParams.get("userId");
  const token = searchParams.get("token");

  const { isPending, isSuccess, isError, mutateAsync } = useUserConfirmEmail(
    () => {
      toastService.success(
        "Email został poprawnie potwierdzony, przejdź do logowania!",
      );
    },
  );

  useEffect(() => {
    if (!token || !userId) {
      toastService.info(
        "Nie udało się pobrać parametrów do potwierdzenia rejestracji\nProsimy o kontakt!",
      );
      return;
    }
    mutateAsync({ token, userId });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [token, userId]);

  const renderContent = () => {
    if (isSuccess) {
      return (
        <Card title="Rejestracja potwierdzona">
          <AppButton
            label="Przejdź do logowania"
            onClick={() => navigate("/login")}
          ></AppButton>
        </Card>
      );
    }
    if (isError) {
      return (
        <Card
          title="Nie udało się potwierdzić rejestracji"
          subTitle="Skontaktuj się z helpdesk!"
        ></Card>
      );
    }
  };

  return (
    <div className="confirm-page">
      <PageLoader visible={isPending} />
      {renderContent()}
    </div>
  );
};

export default ConfirmPage;
