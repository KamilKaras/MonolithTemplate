import { useState } from "react";
import AppInputText from "../../molecules/AppInputText/AppInputText";
import AppPasswordText from "../../molecules/AppPasswordText/AppPasswordText";
import "./registration-form.scss";

const RegistrationForm = () => {
  const [value, setValue] = useState("");
  const [value1, setValue1] = useState("");
  const [value2, setValue2] = useState("");
  const [value3, setValue3] = useState("");
  return (
    <div className="registration-form">
      <AppInputText
        placeholder="Nazwa użytkownika"
        value={value}
        onChange={setValue}
      />
      <AppInputText placeholder="Email" value={value1} onChange={setValue1} />
      <AppPasswordText
        placeholder="Hasło"
        value={value2}
        onChange={setValue2}
      />
      <AppPasswordText
        placeholder="Potwierdź hasło"
        value={value3}
        onChange={setValue3}
      />
    </div>
  );
};

export default RegistrationForm;
