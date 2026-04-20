import { Card } from "primereact/card";
import ForgotPasswordForm from "../../../features/identity/forgot-password/components/ForgetPasswordForm/ForgotPasswordForm";
import { useUserParams } from "../../../shared/hooks/useSearchParams";
import "./reset-password-page.scss";

const ResetPasswordPage = () => {
  useUserParams();

  return (
    <div className="reset-password-page">
      <Card className="reset-password-card">
        <ForgotPasswordForm />
      </Card>
    </div>
  );
};

export default ResetPasswordPage;
