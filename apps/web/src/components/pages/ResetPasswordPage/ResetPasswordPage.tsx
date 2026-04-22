import { Card } from "primereact/card";
import ResetPasswordForm from "../../../features/identity/forgot-password/components/ResetPasswordForm/ResetPasswordForm";
import "./reset-password-page.scss";

const ResetPasswordPage = () => {
  return (
    <div className="reset-password-page">
      <Card className="reset-password-card">
        <ResetPasswordForm />
      </Card>
    </div>
  );
};

export default ResetPasswordPage;
