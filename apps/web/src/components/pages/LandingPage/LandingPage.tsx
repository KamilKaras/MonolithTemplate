import { Card } from "primereact/card";
import { Link } from "react-router-dom";
import {
  APP_CAPABILITIES,
  APP_NAME,
  APP_TAGLINE,
} from "../../../shared/branding/appIdentity";
import { useAuth } from "../../../shared/hooks/useAuth";
import AppStateCard from "../../molecules/AppStateCard/AppStateCard";
import "./landing-page.scss";

const LandingPage = () => {
  const { isAuthenticated, user } = useAuth();

  return (
    <main className="landing-page">
      <header className="landing-page__header">
        <Link to="/" className="landing-page__brand" aria-label={APP_NAME}>
          <span className="landing-page__brand-mark">MT</span>
          <span className="landing-page__brand-text">{APP_NAME}</span>
        </Link>

        <nav className="landing-page__nav" aria-label="Primary">
          <Link to="/login" className="landing-page__nav-link">
            Sign in
          </Link>
          <Link
            to="/register"
            className="landing-page__nav-link landing-page__nav-link--primary"
          >
            Create account
          </Link>
        </nav>
      </header>

      <section className="landing-page__hero">
        <div className="landing-page__hero-copy">
          <p className="landing-page__eyebrow">Application starter</p>
          <h1 className="landing-page__title">{APP_TAGLINE}</h1>
          <p className="landing-page__description">
            {APP_NAME} gives future products a secure authentication foundation,
            a responsive shell, and reusable application states without forcing
            domain-specific decisions.
          </p>

          <div className="landing-page__actions">
            <Link
              to="/login"
              className="landing-page__button landing-page__button--primary"
            >
              Sign in
            </Link>
            <Link
              to="/register"
              className="landing-page__button landing-page__button--secondary"
            >
              Create account
            </Link>
          </div>
        </div>

        <Card className="landing-page__status-card">
          <p className="landing-page__status-label">Current starter status</p>
          <h2 className="landing-page__status-title">
            {isAuthenticated
              ? `Signed in as ${user?.user.name ?? "a user"}`
              : "Ready for sign in"}
          </h2>
          <p className="landing-page__status-copy">
            {isAuthenticated
              ? "The authenticated shell is available and the starter is ready for the first feature module."
              : "Use the secure sign-in flow to enter the authenticated workspace or create a new account if registration is enabled."}
          </p>
        </Card>
      </section>

      <section
        className="landing-page__capabilities"
        aria-labelledby="landing-page-capabilities-title"
      >
        <div className="landing-page__section-heading">
          <p className="landing-page__eyebrow">What is included</p>
          <h2
            id="landing-page-capabilities-title"
            className="landing-page__section-title"
          >
            The template already covers the essentials.
          </h2>
        </div>

        <div className="landing-page__grid">
          {APP_CAPABILITIES.map((capability) => (
            <AppStateCard
              key={capability}
              eyebrow="Included capability"
              title={capability}
              message="Implemented in the starter and ready to reuse for the first product feature."
            />
          ))}
        </div>
      </section>

      <footer className="landing-page__footer">
        <span>{APP_NAME}</span>
        <span>Generic application starter</span>
      </footer>
    </main>
  );
};

export default LandingPage;
