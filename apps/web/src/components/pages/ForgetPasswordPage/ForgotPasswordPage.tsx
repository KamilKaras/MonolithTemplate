import { Card } from "primereact/card";
import ForgotPasswordForm from "../../../features/identity/forgot-password/components/ForgetPasswordForm/ForgotPasswordForm";
import "./forgot-password-page.scss";

const ForgotPasswordPage = () => {
  return (
    <div className="forgot-password-page">
      <Card className="forgot-password-card">
        <ForgotPasswordForm />
      </Card>
    </div>
  );
};

export default ForgotPasswordPage;
