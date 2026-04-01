import { Card } from "primereact/card";
import { useEffect, useMemo } from "react";
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

  const { isPending, isSuccess, mutateAsync } = useUserConfirmEmail(() => {
    toastService.success(
      "Email został poprawnie potwierdzony, przejdź do logowania!",
    );
  });

  useEffect(() => {
    if (!token || !userId) {
      toastService.info(
        "Nie udało się pobrać parametrów do potwierdzenia rejestracji\nProsimy o kontakt!",
      );
      return;
    }
    async function Test(token: string, userId: string) {
      await mutateAsync({ token, userId });
    }
    Test(token, userId);
  }, [mutateAsync, token, userId]);

  const renderContent = useMemo(() => {
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

    return (
      <Card
        title="Nie udało się potwierdzić rejestracji"
        subTitle="Skontaktuj się z helpdesk!"
      ></Card>
    );
  }, [isSuccess, navigate]);

  return (
    <div className="confirm-page">
      <PageLoader visible={isPending} />
      {renderContent}
    </div>
  );
};

export default ConfirmPage;
