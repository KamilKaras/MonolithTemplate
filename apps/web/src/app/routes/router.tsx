import { Navigate, useRoutes, type RouteObject } from "react-router-dom";
import { privateRoutes } from "./privateRoutes";
import { publicRoutes } from "./publicRoutes";

const routes: RouteObject[] = [
  {
    path: "/",
    element: <Navigate to={"/home"} replace />,
  },
  ...publicRoutes,
  ...privateRoutes,
];

const AppRouter = () => {
  return useRoutes(routes);
};

export default AppRouter;
