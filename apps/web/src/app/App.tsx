import { Toast } from "primereact/toast";
import { useEffect, useRef } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import LoginPage from "../components/pages/LoginPage/LoginPage";
import { setToastRef } from "../shared/toast/ToastService";
import "./App.scss";

function App() {
  const toastRef = useRef<Toast>(null);

  useEffect(() => {
    setToastRef(toastRef.current);
  }, []);

  return (
    <>
      <Toast ref={toastRef} position="bottom-right" />
      <Routes>
        <Route path="/" element={<Navigate to="/login" replace />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/confirmation" element={<div></div>} />
      </Routes>
    </>
  );
}

export default App;
