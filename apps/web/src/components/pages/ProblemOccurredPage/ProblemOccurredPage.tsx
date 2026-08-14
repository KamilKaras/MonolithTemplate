import { useNavigate } from "react-router-dom";
import AppStateCard from "../../molecules/AppStateCard/AppStateCard";
import "./problem-occurred-page.scss";

const ProblemOccurredPage = () => {
  const navigate = useNavigate();

  return (
    <div className="problem-occurred-page">
      <AppStateCard
        title="Unable to process request"
        message="Please verify the link and try again."
        actionLabel="Back to login"
        onAction={() => navigate("/login")}
      />
    </div>
  );
};

export default ProblemOccurredPage;
