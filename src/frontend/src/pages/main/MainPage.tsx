import { Link } from "react-router-dom";
import { ROUTES } from "../../app/consts/routes";
import { useAuth } from "../../app/hooks/useAuth.hook";

export const MainPage = () => {
  const { user } = useAuth();

  return <div>
    <Link to={user
      ? ROUTES.HOME
      : ROUTES.SIGNUP
    } className="m-auto mt-1 max-w-sm w-full p-2 text-white rounded-xl bg-primary-darker flex justify-center">Начать</Link>
  </div>;
}