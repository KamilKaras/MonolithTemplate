import { Navigate, Outlet } from "react-router-dom";
import PageLoader from "../../../components/molecules/PageLoader/PageLoader";
import { useAuth } from "../../../shared/hooks/useAuth";

export const RequireGuest = () => {
  const { isLoading, isAuthenticated } = useAuth();

  if (isLoading) {
    return <PageLoader visible />;
  }

  if (isAuthenticated) {
    return <Navigate to="/home" replace />;
  }

  return <Outlet />;
};
