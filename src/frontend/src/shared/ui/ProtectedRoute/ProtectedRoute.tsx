import { Navigate, Outlet } from "react-router-dom";
import { ROUTES } from "../../../app/consts/routes";
import { useAuth } from "../../../app/hooks/useAuth.hook";

export const ProtectedRoute = () => {
  const { user } = useAuth();
  if (!user) {
    return <Navigate to={ROUTES.SIGNIN} />;
  }
  return <Outlet />;
};
