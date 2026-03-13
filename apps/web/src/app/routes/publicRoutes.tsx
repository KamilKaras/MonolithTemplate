import type { RouteObject } from "react-router-dom";
import ForgetPasswordPage from "../../components/pages/ForgetPasswordPage/ForgetPasswordPage";
import LoginPage from "../../components/pages/LoginPage/LoginPage";

export const publicRoutes: RouteObject[] = [
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/forgot-password",
    element: <ForgetPasswordPage />,
  },
];
