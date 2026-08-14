import { useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import PageLoader from "../../../../../components/molecules/PageLoader/PageLoader";
import AuthLayout from "../../../../../components/templates/AuthLayout/AuthLayout";
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
    <AuthLayout
      title="Email confirmed"
      description="Your account is ready. Continue to sign in and enter the starter workspace."
    >
      <div className="confirm-page">
        <p className="confirm-page__message">
          Your registration has been confirmed successfully.
        </p>
        <AppButton
          label="Proceed to sign in"
          onClick={() => navigate("/login")}
        />
      </div>
    </AuthLayout>
  );
};

export default ConfirmPage;
