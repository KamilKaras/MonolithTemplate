import { Navigate, Outlet, useLocation } from "react-router-dom";
import PageLoader from "../../../components/molecules/PageLoader/PageLoader";
import { useAuth } from "../../../shared/hooks/useAuth";

export const RequireAuth = () => {
  const { isLoading, isAuthenticated } = useAuth();
  const location = useLocation();

  if (isLoading) {
    return <PageLoader visible />;
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  return <Outlet />;
};
