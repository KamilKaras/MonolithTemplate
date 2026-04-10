import type { FallbackProps } from "react-error-boundary";
import "./error-fallback.scss";

const ErrorFallback = ({ error, resetErrorBoundary }: FallbackProps) => {
  return (
    <div className="error-fallback">
      <div className="error-fallback__card">
        <div className="error-fallback__icon">⚠️</div>

        <h1 className="error-fallback__title">Coś poszło nie tak</h1>

        <p className="error-fallback__description">
          Wystąpił nieoczekiwany błąd aplikacji.
        </p>

        <div className="error-fallback__actions">
          <button
            className="error-fallback__button error-fallback__button--primary"
            onClick={resetErrorBoundary}
          >
            Spróbuj ponownie
          </button>

          <button
            className="error-fallback__button error-fallback__button--secondary"
            onClick={() => window.location.reload()}
          >
            Odśwież stronę
          </button>
        </div>

        {import.meta.env.DEV && (
          <pre className="error-fallback__debug">
            {error instanceof Error ? error.stack : String(error)}
          </pre>
        )}
      </div>
    </div>
  );
};

export default ErrorFallback;
