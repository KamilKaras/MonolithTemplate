import { Link } from "react-router-dom";
import { useAuth } from "../../../shared/hooks/useAuth";
import AppStateCard from "../../molecules/AppStateCard/AppStateCard";
import "./home-page.scss";

const HomePage = () => {
  const { user } = useAuth();

  return (
    <main className="home-page">
      <section className="home-page__hero">
        <p className="home-page__eyebrow">Authenticated workspace</p>
        <h1 className="home-page__title">
          Welcome back{user?.user.name ? `, ${user.user.name}` : ""}.
        </h1>
        <p className="home-page__description">
          Authentication is active and the starter shell is ready for the first
          product feature.
        </p>

        <div className="home-page__actions">
          <Link to="/" className="home-page__link">
            View public landing page
          </Link>
        </div>
      </section>

      <section className="home-page__grid" aria-label="Starter overview">
        <AppStateCard
          eyebrow="Session"
          title="Signed in"
          message={`Account: ${user?.user.name ?? "Authenticated user"}.`}
        />
        <AppStateCard
          eyebrow="Shell"
          title="Responsive layout"
          message="The header, content container, and recovery pages share one visual system."
        />
        <AppStateCard
          eyebrow="Starter"
          title="Ready for features"
          message="Add the first product module here without replacing the foundation."
        />
      </section>
    </main>
  );
};

export default HomePage;
