import { Card } from "primereact/card";
import { useState } from "react";
import AppInputText from "../../molecules/AppInputText/AppInputText";
import AppPasswordText from "../../molecules/AppPasswordText/AppPasswordText";
import AppTabViewer from "../../molecules/AppTabViewer/AppTabViewer";
import "./login-page.scss";
const LoginPage = () => {
  const [value, setValue] = useState("");
  const [value1, setValue1] = useState("");
  return (
    <div className="login-wrapper">
      <Card
        title="Witamy ponownie"
        subTitle="Przetestuj naszą aplikację przez miesiąc za darmo"
        className="login-card"
      >
        <AppTabViewer
          tabs={[
            {
              header: "Zarejestruj się",
              content: (
                <>
                  <AppInputText
                    placeholder="Email"
                    value={value}
                    onChange={setValue}
                  />
                  <AppPasswordText
                    placeholder="Hasło"
                    value={value1}
                    onChange={setValue1}
                  />
                </>
              ),
            },
            { header: "Zaloguj się", content: <div></div> },
          ]}
        />
      </Card>
    </div>
  );
};

export default LoginPage;
