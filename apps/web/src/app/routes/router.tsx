import { Navigate, useRoutes, type RouteObject } from "react-router-dom";
import NotFoundPage from "../../components/pages/NotFoundPage/NotFoundPage";
import ProblemOccurredPage from "../../components/pages/ProblemOccurredPage/ProblemOccurredPage";
import { privateRoutes } from "./privateRoutes";
import { publicRoutes } from "./publicRoutes";

const routes: RouteObject[] = [
  {
    path: "/",
    element: <Navigate to={"/home"} replace />,
  },
  {
    path: "/problem",
    element: <ProblemOccurredPage />,
  },
  ...publicRoutes,
  ...privateRoutes,
  {
    path: "*",
    element: <NotFoundPage />,
  },
];

const AppRouter = () => {
  return useRoutes(routes);
};

export default AppRouter;
