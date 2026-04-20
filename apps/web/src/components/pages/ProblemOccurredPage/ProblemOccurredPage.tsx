import { Card } from "primereact/card";
import "./problem-occurred-page.scss";

const ProblemOccurredPage = () => {
  return (
    <div className="problem-occurred-page">
      <Card
        title="Nie udało się potwierdzić rejestracji"
        subTitle="Skontaktuj się z helpdeskiem!"
      />
    </div>
  );
};

export default ProblemOccurredPage;
