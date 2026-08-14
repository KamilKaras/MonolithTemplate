import { Link } from "react-router-dom";
import ResetPasswordForm from "../../../features/identity/forgot-password/components/ResetPasswordForm/ResetPasswordForm";
import AuthLayout from "../../templates/AuthLayout/AuthLayout";

const ResetPasswordPage = () => {
  return (
    <AuthLayout
      title="Choose a new password"
      description="Set a new password to regain access to the starter application."
      footer={
        <>
          Need another link?{" "}
          <Link to="/forgot-password">Request recovery email</Link>
        </>
      }
    >
      <ResetPasswordForm />
    </AuthLayout>
  );
};

export default ResetPasswordPage;
