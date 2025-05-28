import { Link, NavLink, useNavigate } from "react-router-dom";
import logo from "../../../assets/logo.png";
import { ROUTES } from "../../../app/consts/routes";
import styles from './Header.module.scss';
import { useAuth } from "../../../app/hooks/useAuth.hook";

export const Header: React.FC = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const navs = [
    { link: ROUTES.HOME, name: "Главная" }
  ];

  const onLogoutClicked = async () => {
    await logout();
    await navigate(ROUTES.MAIN);
  }

  return (
    <>
      <header className="mx-1">
        <div className={styles.leftHeader}>
          <div className={styles.logo}>
            <Link to="/">
              <img src={logo} height={50} width={50} alt={"logo"} />
              <p>WebObserver</p>
            </Link>
          </div>
          <nav>
            {navs.map((tuple) => (
              <NavLink key={tuple.link}
                to={tuple.link}
                className={({ isActive, isPending }) =>
                  isActive
                    ? "text-purple-700"
                    : isPending
                      ? "pending"
                      : ""
                }>{tuple.name}</NavLink>
            ))}
          </nav>
        </div>
        <div className={"mr-2 flex gap-2 overflow bg-primary p-1 rounded-2xl"}>
          {user
            ? (<div className="bg-primary-darker rounded-2xl p-2 cursor-pointer" onClick={() => void onLogoutClicked()}>Выйти</div>)
            : (<>
              <Link to={ROUTES.SIGNUP} className="bg-primary-darker rounded-2xl p-2">Зарегистрироваться</Link>
              <Link to={ROUTES.SIGNIN} className="bg-primary-darker rounded-2xl p-2">Войти</Link>
            </>)}
        </div>
      </header>
    </>
  )
}