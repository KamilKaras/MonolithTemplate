import { Card } from "primereact/card";
import AppTabViewer from "../../molecules/AppTabViewer/AppTabViewer";
import LoginForm from "../../organisms/LoginForm/LoginForm";
import RegistrationForm from "../../organisms/RegistrationForm/RegistrationForm";
import "./login-page.scss";

const LoginPage = () => {
  return (
    <div className="login-page">
      <Card
        title="Witamy ponownie"
        subTitle="Przetestuj naszą aplikację przez miesiąc za darmo"
        className="login-card"
      >
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
