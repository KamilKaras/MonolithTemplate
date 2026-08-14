import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../shared/hooks/useAuth";
import AppStateCard from "../../molecules/AppStateCard/AppStateCard";
import "../ProblemOccurredPage/problem-occurred-page.scss";

const NotFoundPage = () => {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();

  return (
    <div className="problem-occurred-page">
      <AppStateCard
        eyebrow="404"
        title="Page not found"
        message="The page you requested does not exist. You can go to the starter landing page or return to the authenticated workspace."
        actionLabel={isAuthenticated ? "Go to home" : "Go to landing page"}
        onAction={() => navigate(isAuthenticated ? "/home" : "/")}
        secondaryActionLabel={
          isAuthenticated ? "Go to landing page" : "Sign in"
        }
        onSecondaryAction={() => navigate(isAuthenticated ? "/" : "/login")}
      />
    </div>
  );
};

export default NotFoundPage;
