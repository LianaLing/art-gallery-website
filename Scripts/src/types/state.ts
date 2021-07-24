/**
 * This is the structure of an ASP.NET Session state data,
 * essentially is just a key-value pair.
 */
export type SessionData = {
  Key: string;
  Value: string;
};

// This is the possible state of an authentication view (Auth Modal)
export type AuthView = {
  modalOpen: boolean;
  view: "login" | "signup" | "forgot_password" | "artist_signup";
};
