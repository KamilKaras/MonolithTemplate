import { Card } from "primereact/card";
import ForgotPasswordForm from "../../../features/identity/forgot-password/components/ForgetPasswordForm/ForgotPasswordForm";
import "./forget-password-page.scss";

const ForgetPasswordPage = () => {
  return (
    <div className="forget-password-page">
      <Card className="forget-password-card">
        <ForgotPasswordForm />
      </Card>
    </div>
  );
};

export default ForgetPasswordPage;
