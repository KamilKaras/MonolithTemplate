import { Card } from "primereact/card";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { useState } from "react";
import "./login-page.scss";

const LoginPage = () => {
  const [value, setValue] = useState("");
  return (
    <div className="login-wrapper">
      <Card className="login-card">
        <InputText />
        <Password
          value={value}
          onChange={(e) => setValue(e.target.value)}
          toggleMask
        />
      </Card>
    </div>
  );
};

export default LoginPage;
