import { Toast } from "primereact/toast";
import { Suspense, useEffect, useRef } from "react";
import { ErrorBoundary } from "react-error-boundary";
import PageLoader from "../components/molecules/PageLoader/PageLoader";
import MainPage from "../components/templates/MainPage/MainPage";
import ErrorFallback from "../shared/errors/ErrorFallback/ErrorFallback";
import { setToastRef } from "../shared/toast/ToastService";
import "./App.scss";
import AppRouter from "./routes/router";

function App() {
  const toastRef = useRef<Toast>(null);

  useEffect(() => {
    setToastRef(toastRef.current);
  }, []);

  return (
    <ErrorBoundary
      FallbackComponent={ErrorFallback}
      onReset={() => window.location.reload()}
    >
      <Suspense fallback={<PageLoader visible />}>
        <MainPage>
          <Toast ref={toastRef} position="bottom-right" />
          <AppRouter />
        </MainPage>
      </Suspense>
    </ErrorBoundary>
  );
}

export default App;
