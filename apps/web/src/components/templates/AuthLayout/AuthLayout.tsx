import { Card } from "primereact/card";
import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import {
  APP_CAPABILITIES,
  APP_DESCRIPTION,
  APP_NAME,
} from "../../../shared/branding/appIdentity";
import "./auth-layout.scss";

type AuthLayoutProps = {
  title: string;
  description: string;
  footer?: ReactNode;
  children: ReactNode;
};

const AuthLayout = ({
  title,
  description,
  footer,
  children,
}: AuthLayoutProps) => {
  return (
    <main className="auth-layout">
      <div className="auth-layout__frame">
        <section
          className="auth-layout__intro"
          aria-labelledby="auth-layout-title"
        >
          <Link to="/" className="auth-layout__brand" aria-label={APP_NAME}>
            <span className="auth-layout__brand-mark">MT</span>
            <span className="auth-layout__brand-text">{APP_NAME}</span>
          </Link>

          <p className="auth-layout__eyebrow">Secure access</p>
          <h1 id="auth-layout-title" className="auth-layout__title">
            {title}
          </h1>
          <p className="auth-layout__description">{description}</p>

          <div className="auth-layout__summary-card">
            <p className="auth-layout__summary-label">
              What the starter already provides
            </p>
            <p className="auth-layout__summary-copy">{APP_DESCRIPTION}</p>
          </div>

          <ul className="auth-layout__capabilities">
            {APP_CAPABILITIES.map((capability) => (
              <li key={capability} className="auth-layout__capability">
                {capability}
              </li>
            ))}
          </ul>
        </section>

        <section className="auth-layout__panel" aria-label={title}>
          <Card className="auth-layout__card">{children}</Card>
          {footer ? <div className="auth-layout__footer">{footer}</div> : null}
        </section>
      </div>
    </main>
  );
};

export default AuthLayout;
