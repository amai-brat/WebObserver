import {useState, useEffect} from "react";
import { getToken } from "../../shared/utils/token";
import { ENDPOINTS } from "../../app/consts/endpoints";
import { Link } from "react-router-dom";
import { config } from "../../shared/utils/config";

export const HangfirePage = () => {
  const [iframeError, setIframeError] = useState(false);
  const url = `${ENDPOINTS.HANGFIRE.DASHBOARD}?jwt=${getToken()}`;

  useEffect(() => {
    (async() => {
      const resp = await fetch(url);
      if (!resp.ok) {
        setIframeError(true);
      }
    })()
  }, [url]);
  
  return (
    <>
      {iframeError 
        ? <p>403 Forbidden</p> 
        : <Link to={config.MAIN_API_URL + ENDPOINTS.HANGFIRE.DASHBOARD.replace("/api", "")} className="m-auto mt-1 max-w-sm w-full p-2 text-white rounded-xl bg-primary-darker flex justify-center">Зайти в dashboard</Link>
      }
    </>
  );
};