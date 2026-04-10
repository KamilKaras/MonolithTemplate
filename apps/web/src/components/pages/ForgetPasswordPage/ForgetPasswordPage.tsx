import { Card } from "primereact/card";
import ForgetPasswordForm from "../../../features/identity/forgot-password/components/ForgetPasswordForm/ForgetPasswordForm";
import "./forget-password-page.scss";

const ForgetPasswordPage = () => {
  return (
    <div className="forget-password-page">
      <Card className="forget-password-card">
        <ForgetPasswordForm />
      </Card>
    </div>
  );
};

export default ForgetPasswordPage;
