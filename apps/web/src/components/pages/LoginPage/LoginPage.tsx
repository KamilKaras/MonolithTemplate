import { Card } from "primereact/card";
import LoginForm from "../../../features/identity/login/components/LoginForm/LoginForm";
import RegistrationForm from "../../../features/identity/register/components/RegistrationForm/RegistrationForm";
import AppTabViewer from "../../molecules/AppTabViewer/AppTabViewer";
import "./login-page.scss";

const LoginPage = () => {
  return (
    <div className="login-page">
      <Card className="login-card">
        <AppTabViewer
          tabs={[
            {
              header: "Zaloguj się",
              content: <LoginForm />,
            },
            { header: "Zarejestruj się", content: <RegistrationForm /> },
          ]}
        />
      </Card>
    </div>
  );
};

export default LoginPage;
