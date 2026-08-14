import { Link } from "react-router-dom";
import ForgotPasswordForm from "../../../features/identity/forgot-password/components/ForgetPasswordForm/ForgotPasswordForm";
import AuthLayout from "../../templates/AuthLayout/AuthLayout";

const ForgotPasswordPage = () => {
  return (
    <AuthLayout
      title="Reset your password"
      description="We will send a recovery link to the email address on your account."
      footer={
        <>
          Remembered it already? <Link to="/login">Back to sign in</Link>
        </>
      }
    >
      <ForgotPasswordForm />
    </AuthLayout>
  );
};

export default ForgotPasswordPage;
