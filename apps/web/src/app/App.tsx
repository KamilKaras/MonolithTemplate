import { Toast } from "primereact/toast";
import { Suspense, useEffect, useRef } from "react";
import { ErrorBoundary } from "react-error-boundary";
import { Navigate, Route, Routes } from "react-router-dom";
import ForgetPasswordPage from "../components/pages/ForgetPasswordPage/ForgetPasswordPage";
import LoginPage from "../components/pages/LoginPage/LoginPage";
import PageLoader from "../components/pages/PageLoader/PageLoader";
import ErrorFallback from "../shared/errors/ErrorFallback/ErrorFallback";
import { setToastRef } from "../shared/toast/ToastService";
import "./App.scss";

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
        <Toast ref={toastRef} position="bottom-right" />
        <Routes>
          <Route path="/" element={<Navigate to="/login" replace />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/confirmation" element={<div></div>} />
          <Route path="/forgot-password" element={<ForgetPasswordPage />} />
        </Routes>
      </Suspense>
    </ErrorBoundary>
  );
}

export default App;
