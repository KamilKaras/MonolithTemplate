import { Link } from "react-router-dom";
import RegistrationForm from "../../../features/identity/register/components/RegistrationForm/RegistrationForm";
import AuthLayout from "../../templates/AuthLayout/AuthLayout";

const RegistrationPage = () => {
  return (
    <AuthLayout
      title="Create your account"
      description="Register once to access the authenticated starter shell and the shared application foundation."
      footer={
        <>
          Already have an account? <Link to="/login">Sign in</Link>
        </>
      }
    >
      <RegistrationForm />
    </AuthLayout>
  );
};

export default RegistrationPage;
