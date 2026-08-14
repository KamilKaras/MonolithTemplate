import { useNavigate } from "react-router-dom";
import AppStateCard from "../../molecules/AppStateCard/AppStateCard";
import "../ProblemOccurredPage/problem-occurred-page.scss";

const NotFoundPage = () => {
  const navigate = useNavigate();

  return (
    <div className="problem-occurred-page">
      <AppStateCard
        title="Page not found"
        message="The page you requested does not exist."
        actionLabel="Go to home"
        onAction={() => navigate("/home")}
      />
    </div>
  );
};

export default NotFoundPage;
