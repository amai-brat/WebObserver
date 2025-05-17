import { Link } from "react-router-dom";
import { ROUTES } from "../../app/consts/routes";

export const MainPage = () => {
  return <div className="m-auto mt-1 max-w-sm w-full p-2 text-white rounded-xl bg-primary-darker flex justify-center">
    <Link to={ROUTES.SIGNUP}>Начать</Link>
  </div>;
}