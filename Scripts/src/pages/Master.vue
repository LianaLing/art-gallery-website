<template>
  <div class="flex items-center justify-between px-6 py-5">
    <div class="flex items-center">
      <Pinterest />
      <h1 class="text-xl font-bold font-garamond text-accent">Art Gallery</h1>
    </div>
    <div class="flex items-center font-bold text-dark">
      <div class="mr-8">
        <a class="px-3 py-2 mr-1 hover:underline font-garamond" href="/"
          >About</a
        >
        <a class="px-3 py-2 mr-1 hover:underline font-garamond" href="/"
          >Business</a
        >
        <a class="px-3 py-2 hover:underline font-garamond" href="/">Blog</a>
      </div>
      <button
        class="
          px-3
          py-2
          mr-4
          text-white
          rounded-full
          bg-accent
          hover:bg-accent-hover
          font-garamond
        "
        @click="loginHandler"
      >
        Log in
      </button>
      <button
        class="
          px-3
          py-2
          rounded-full
          bg-light
          hover:bg-light-hover
          font-garamond
        "
        @click="signupHandler"
      >
        Sign up
      </button>
    </div>
  </div>
  <AuthController />
</template>

<script lang="ts">
import { useAuthView } from "../stores/useAuthView";
import { AuthView } from "../types/state";
import AuthController from "../components/auth/AuthController.vue";
import Pinterest from "../components/icons/Pinterest.vue";

export default {
  components: { Pinterest, AuthController },
  setup() {
    const authView = useAuthView();

    const setAuthView = (val: AuthView) => (authView.value = val);

    const loginHandler = (event: Event) => {
      // prevent button triggers refresh
      event.preventDefault();
      setAuthView({ modalOpen: true, view: "login" });
    };

    const signupHandler = (event: Event) => {
      // prevent button triggers refresh
      event.preventDefault();
      setAuthView({ modalOpen: true, view: "signup" });
    };

    return {
      loginHandler,
      signupHandler,
    };
  },
};
</script>