import { Navigate, useRoutes, type RouteObject } from "react-router-dom";
import { useAppSelector } from "../../store/store";
import { privateRoutes } from "./privateRoutes";
import { publicRoutes } from "./publicRoutes";

const AppRouter = () => {
  const token = useAppSelector((state) => state.auth.session?.token);
  console.log(token);
  const isAuthenticated = !!token;

  const routes: RouteObject[] = [
    {
      path: "/",
      element: <Navigate to={isAuthenticated ? "/home" : "/login"} replace />,
    },
    ...publicRoutes,
    ...privateRoutes,
  ];

  return useRoutes(routes);
};

export default AppRouter;
