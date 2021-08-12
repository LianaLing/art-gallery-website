/**
 * This is the structure of user's session stored at
 * ASP.NET
 */
export type Session = {
  user?: User;
};

export type User = {
  accessFailedCount: number;
  authorId: number | null;
  avatarUrl: string | null;
  claims: unknown[];
  dob: string | null;
  email: string;
  emailConfirmed: boolean;
  ic: string | null;
  id: number;
  lockoutEnabled: boolean;
  lockoutEndDateUtc: string | null;
  logins: unknown[];
  name: string | null;
  passwordHash: string | null;
  phoneNumber: string | null;
  phoneNumberConfirmed: boolean;
  roles: unknown[];
  securityStamp: string;
  twoFactorEnabled: boolean;
  userName: string;
};

// This is the possible state of an authentication view (Auth Modal)
export type AuthView = {
  modalOpen: boolean;
  view: "login" | "signup" | "forgot_password" | "artist_signup";
};
