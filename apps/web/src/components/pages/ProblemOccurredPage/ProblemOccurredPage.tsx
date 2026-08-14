import { useNavigate } from "react-router-dom";
import AppStateCard from "../../molecules/AppStateCard/AppStateCard";
import "./problem-occurred-page.scss";

const ProblemOccurredPage = () => {
  const navigate = useNavigate();

  return (
    <div className="problem-occurred-page">
      <AppStateCard
        eyebrow="Unexpected problem"
        title="Something went wrong"
        message="The application could not complete the request. You can return to sign in or go back to the landing page."
        actionLabel="Back to sign in"
        onAction={() => navigate("/login")}
        secondaryActionLabel="Go to landing page"
        onSecondaryAction={() => navigate("/")}
      />
    </div>
  );
};

export default ProblemOccurredPage;
