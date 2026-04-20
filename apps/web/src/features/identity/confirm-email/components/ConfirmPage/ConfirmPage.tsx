import { Card } from "primereact/card";
import { useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import PageLoader from "../../../../../components/molecules/PageLoader/PageLoader";
import { useUserParams } from "../../../../../shared/hooks/useSearchParams";
import { useConfirmEmail } from "../../hooks/useConfirmEmail";
import "./confirm-page.scss";

const ConfirmPage = () => {
  const { token, userId } = useUserParams();

  const navigate = useNavigate();
  const calledRef = useRef(false);

  const { isPending, mutateAsync } = useConfirmEmail();

  useEffect(() => {
    if (!token || !userId) {
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
    return <PageLoader visible />;
  }

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
};

export default ConfirmPage;
