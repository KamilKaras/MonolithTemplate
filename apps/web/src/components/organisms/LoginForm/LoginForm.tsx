import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from "yup";
import AppButton from "../../atoms/AppButton/AppButton";
import AppInputText from "../../molecules/AppInputText/AppInputText";
import AppPasswordText from "../../molecules/AppPasswordText/AppPasswordText";
import "./login-form.scss";
import { loginInitialFormValues, type LoginFormValues } from "./types";

const LoginForm = () => {
  const loginSchema = Yup.object({
    email: Yup.string()
      .email("Niepoprawny email")
      .required("Email jest wymagany"),
    password: Yup.string()
      .min(8, "Min 8 znaków")
      .required("Hasło jest wymagane"),
  });

  const formik = useFormik<LoginFormValues>({
    initialValues: loginInitialFormValues,
    validationSchema: loginSchema,
    onSubmit: (values) => {
      console.log(values);
    },
  });

  return (
    <FormikProvider value={formik}>
      <Form className="login-form" noValidate>
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
        <div className="login-form buttons-container">
          <AppButton label="Zaloguj się" onClick={() => undefined} />
        </div>
      </Form>
    </FormikProvider>
  );
};

export default LoginForm;
