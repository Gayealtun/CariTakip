
//Token yoksa → Login
//Token varsa → İstenen sayfa
import { Navigate, Outlet } from "react-router-dom";
import { isTokenValid, logout } from "../utils/auth.js";

function ProtectedRoute() {
  if (!isTokenValid()) {
    logout();
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
}

export default ProtectedRoute;