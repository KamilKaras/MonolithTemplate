import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from "yup";
import AppButton from "../../atoms/AppButton/AppButton";
import AppInputText from "../../molecules/AppInputText/AppInputText";
import AppPasswordText from "../../molecules/AppPasswordText/AppPasswordText";
import "./registration-form.scss";
import {
  registrationInitialFormValues,
  type RegistrationFormValues,
} from "./types";

const RegistrationForm = () => {
  const registrationSchema = Yup.object({
    userName: Yup.string().email("Niepoprawny email").required("Pole wymagane"),
    email: Yup.string().email("Niepoprawny email").required("Pole wymagane"),
    password: Yup.string().min(8, "Min 8 znaków").required("Pole wymagane"),
    confirmPassword: Yup.string()
      .min(8, "Min 8 znaków")
      .required("Pole wymagane"),
  });

  const formik = useFormik<RegistrationFormValues>({
    initialValues: registrationInitialFormValues,
    validationSchema: registrationSchema,
    onSubmit: (values) => {
      console.log(values);
    },
  });

  return (
    <FormikProvider value={formik}>
      <Form noValidate>
        <div className="registration-form">
          <AppInputText
            placeholder="Nazwa użytkownika"
            value={formik.values.userName}
            onChange={(v) => formik.setFieldValue("userName", v)}
            error={formik.errors.userName}
          />
          <AppInputText
            placeholder="Email"
            value={formik.values.email}
            onChange={(v) => formik.setFieldValue("email", v)}
            error={formik.errors.email}
          />
          <AppPasswordText
            placeholder="Hasło"
            value={formik.values.password}
            onChange={(v) => formik.setFieldValue("password", v)}
            error={formik.errors.password}
          />
          <AppPasswordText
            placeholder="Potwierdź hasło"
            value={formik.values.confirmPassword}
            onChange={(v) => formik.setFieldValue("confirmPassword", v)}
            error={formik.errors.confirmPassword}
          />
          <div className="login-form save-button">
            <AppButton label="Rejestruj" onClick={() => undefined} />
          </div>
        </div>
      </Form>
    </FormikProvider>
  );
};

export default RegistrationForm;
