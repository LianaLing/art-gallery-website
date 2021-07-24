import { createGlobalState, useStorage } from "@vueuse/core";
import { AuthView } from "../types/state";

/**
 * Create a global state that is shareable across vue instances,
 * this is achieved using browser's local storage.
 */
export const useAuthView = createGlobalState(() =>
  useStorage<AuthView>("art-gallery-auth", {
    modalOpen: false,
    view: "login",
  })
);
