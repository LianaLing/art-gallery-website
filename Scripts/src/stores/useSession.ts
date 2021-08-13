import { createGlobalState, useStorage } from "@vueuse/core";
import { Session } from "../types/state";

/**
 * Create a global state that is shareable across vue instances,
 * this is achieved using browser's local storage.
 */
export const useSession = createGlobalState(() =>
  useStorage<Session>("art-gallery-session", {})
);
