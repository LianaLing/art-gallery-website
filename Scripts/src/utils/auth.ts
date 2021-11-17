import { ApiError, ApiResponse } from "../types/api";
import { post } from "./api";

enum AuthRoute {
  Login = "Api/Auth.aspx/Login",
  Logout = "Api/Auth.aspx/Logout",
  Signup = "Api/Auth.aspx/Signup",
  Delete = "Api/Auth.aspx/Delete",
}

type AuthRole = "personal" | "artist";

type AuthResponse = ApiResponse<{ redirectUrl: string }>;

export type BaseAuthParam = { email: string; password: string };
type LoginParam = BaseAuthParam & { remember: boolean };
type SignupParam = BaseAuthParam & { role: AuthRole };

export const login = async ({
  email,
  password,
  remember,
}: LoginParam): Promise<AuthResponse> => {
  const { data, error } = await post<AuthResponse["data"]>(AuthRoute.Login, {
    email,
    password,
    remember,
  });
  return { data, error };
};

export const logout = async (): Promise<AuthResponse> => {
  const { data, error } = await post<AuthResponse["data"]>(AuthRoute.Logout);
  return { data, error };
};

export const signup = async ({
  email,
  password,
  role,
}: SignupParam): Promise<AuthResponse> => {
  const { data, error } = await post<AuthResponse["data"]>(AuthRoute.Signup, {
    email,
    password,
    role,
  });
  return { data, error };
};
