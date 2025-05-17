import axios from "axios";
import { ENDPOINTS } from "../../app/consts/endpoints";

export interface SignUpDto {
  name: string;
  email: string;
  password: string;
}

export interface SignInDto {
  email: string;
  password: string;
}

interface SignInResponse {
  accessToken: string;
}

const signup = async (dto: SignUpDto): Promise<number> => {
  const { data } = await axios.post<number>(ENDPOINTS.AUTH.SIGNUP, dto);
  return data;
}

const signin = async (dto: SignInDto): Promise<string> => {
  const { data } = await axios.post<SignInResponse>(ENDPOINTS.AUTH.SIGNIN, dto);
  return data.accessToken;
}

const logout = async (): Promise<void> => {
  return Promise.resolve();
}

export const authApi = {
  signup,
  signin,
  logout
};
