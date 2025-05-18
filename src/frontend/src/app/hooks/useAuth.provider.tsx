import { type PropsWithChildren, useCallback, useEffect, useMemo, useState } from "react";
import { getToken as getStorageToken, setToken as setStorageToken } from "../../shared/utils/token";
import { jwtDecode } from "jwt-decode";
import { authApi, type SignInDto } from "../../shared/api/authApi";
import { AuthContext } from "./useAuth.hook";

interface User {
  id: number;
  role: string;
}

export interface AuthContextType {
  user: User | null,
  signin: (dto: SignInDto) => Promise<void>;
  logout: () => Promise<void>;
}

export const AuthProvider: React.FC<PropsWithChildren> = ({ children }) => {
  const [token, setToken] = useState<string>(getStorageToken());

  const onStorageUpdate = (event: StorageEvent) => {
    const { key, newValue } = event;
    if (key === "accessToken") {
      setToken(newValue!);
    }
  };

  useEffect(() => {
    setToken(getStorageToken());
    window.addEventListener("storage", onStorageUpdate);
    return () => {
      window.removeEventListener("storage", onStorageUpdate);
    };
  }, []);

  const signin = useCallback(async (dto: SignInDto) => {
    const token = await authApi.signin(dto);
    setToken(token);
    setStorageToken(token);
  }, []);


  const logout = useCallback(async () => {
    await authApi.logout();
    setToken("");
    setStorageToken("");
  }, []);

  let user: User | null;
  try {
    if (jwtDecode(token).exp! < (+new Date() / 1000)) {
      user = null;
    } else {
      user = jwtDecode<User>(token);
    }
  }
  catch {
    user = null;
  }

  const contextValue = useMemo<AuthContextType>(() => ({
    user,
    signin,
    logout,
  }), [user, signin, logout]);

  return (
    <AuthContext value={contextValue}>
      {children}
    </AuthContext>
  );
};


