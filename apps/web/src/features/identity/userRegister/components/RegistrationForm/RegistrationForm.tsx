import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from "yup";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import AppInputText from "../../../../../components/molecules/AppInputText/AppInputText";
import AppPasswordText from "../../../../../components/molecules/AppPasswordText/AppPasswordText";
import {
  registrationInitialFormValues,
  type RegistrationFormValues,
} from "../../types";
import "./registration-form.scss";

const RegistrationForm = () => {
  const registrationSchema = Yup.object({
    userName: Yup.string()
      .min(2, "Pole wymaga minimum 2 znaków")
      .required("Pole wymagane"),
    email: Yup.string().email("Niepoprawny email").required("Pole wymagane"),
    password: Yup.string().min(8, "Min 8 znaków").required("Pole wymagane"),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref("password")], "Hasła muszą być takie same")
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
      <Form className="registration-form" noValidate>
        <AppInputText
          placeholder="Imię"
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
        <div className="registration-form buttons-container">
          <AppButton
            type="submit"
            label="Utwórz konto"
            onClick={() => undefined}
          />
        </div>
      </Form>
    </FormikProvider>
  );
};

export default RegistrationForm;
