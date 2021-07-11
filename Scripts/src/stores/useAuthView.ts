import { createGlobalState, useStorage } from "@vueuse/core";
import { AuthView } from "../types/state";

export const useAuthView = createGlobalState(() =>
  useStorage<AuthView>("art-gallery-auth", {
    modalOpen: false,
    view: "login",
  })
);
