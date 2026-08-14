import { useRoutes, type RouteObject } from "react-router-dom";
import LandingPage from "../../components/pages/LandingPage/LandingPage";
import NotFoundPage from "../../components/pages/NotFoundPage/NotFoundPage";
import ProblemOccurredPage from "../../components/pages/ProblemOccurredPage/ProblemOccurredPage";
import { privateRoutes } from "./privateRoutes";
import { publicRoutes } from "./publicRoutes";

const routes: RouteObject[] = [
  {
    path: "/",
    element: <LandingPage />,
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
