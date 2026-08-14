import { Link } from "react-router-dom";
import LoginForm from "../../../features/identity/login/components/LoginForm/LoginForm";
import AuthLayout from "../../templates/AuthLayout/AuthLayout";

const LoginPage = () => {
  return (
    <AuthLayout
      title="Sign in to continue"
      description="Use your account to enter the authenticated starter shell and continue building the next application."
      footer={
        <>
          No account yet? <Link to="/register">Create one</Link>
        </>
      }
    >
      <LoginForm />
    </AuthLayout>
  );
};

export default LoginPage;
