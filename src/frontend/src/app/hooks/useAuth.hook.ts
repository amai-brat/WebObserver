import { createContext, use } from "react";
import { type AuthContextType } from "./useAuth.provider";

export const AuthContext = createContext<AuthContextType | null>(null);

export const useAuth = (): AuthContextType => {
  return use(AuthContext)!;
};
