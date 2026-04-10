import { Card } from "primereact/card";
import { useEffect, useRef } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import PageLoader from "../../../../../components/molecules/PageLoader/PageLoader";
import { toastService } from "../../../../../shared/toast/ToastService";
import { useUserConfirmEmail } from "../../hooks/useUserConfirmEmail";
import "./confirm-page.scss";

const ConfirmPage = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const calledRef = useRef(false);

  const userId = searchParams.get("userId");
  const token = searchParams.get("token");

  const { isPending, isSuccess, mutateAsync } = useUserConfirmEmail();

  useEffect(() => {
    if (!token || !userId) {
      toastService.info(
        "Nie udało się pobrać parametrów do potwierdzenia rejestracji.\nProsimy o kontakt z helpdeskiem!",
      );
      return;
    }
    if (calledRef.current) return;
    calledRef.current = true;

    const confirmEmail = async () => {
      await mutateAsync({ token, userId });
    };

    void confirmEmail();
  }, [mutateAsync, token, userId]);

  if (isPending) {
    return (
      <div className="confirm-page">
        <PageLoader visible />
        <Card title="Trwa weryfikacja..." />
      </div>
    );
  }

  if (isSuccess) {
    return (
      <div className="confirm-page">
        <Card title="Rejestracja potwierdzona">
          <AppButton
            label="Przejdź do logowania"
            onClick={() => navigate("/login")}
          />
        </Card>
      </div>
    );
  }

  return (
    <div className="confirm-page">
      <Card
        title="Nie udało się potwierdzić rejestracji"
        subTitle="Skontaktuj się z helpdeskiem!"
      />
    </div>
  );
};

export default ConfirmPage;
