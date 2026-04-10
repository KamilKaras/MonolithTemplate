import { Form, FormikProvider, useFormik } from "formik";
import { Link } from "react-router-dom";
import * as Yup from "yup";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import AppInputText from "../../../../../components/molecules/AppInputText/AppInputText";
import AppPasswordText from "../../../../../components/molecules/AppPasswordText/AppPasswordText";
import { useLogin } from "../../hooks/useLogin";
import { loginInitialFormValues, type LoginFormValues } from "../../types";
import "./login-form.scss";

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
      mutateAsync({
        email: values.email!,
        password: values.password!,
      });
    },
  });

  const { isPending, mutateAsync } = useLogin(() => formik.resetForm());

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
        <Link to={"/forgot-password"} className="login-form__forgot-password">
          Zapomniałeś hasła?
        </Link>
        <div className="login-form buttons-container">
          <AppButton loading={isPending} type="submit" label="Zaloguj się" />
        </div>
      </Form>
    </FormikProvider>
  );
};

export default LoginForm;
