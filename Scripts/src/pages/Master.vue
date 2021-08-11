<template>
  <div class="flex py-5 px-6 items-center justify-between">
    <button class="flex items-center" @click="homePageHandler">
      <Pinterest />
      <h1 class="font-bold font-garamond text-xl text-accent">Art Gallery</h1>
    </button>
    <div class="flex font-bold text-dark items-center">
      <div class="mr-8">
        <a class="font-garamond mr-1 py-2 px-3 hover:underline" href="/"
          >About</a
        >
        <a class="font-garamond mr-1 py-2 px-3 hover:underline" href="/"
          >Business</a
        >
        <a class="font-garamond py-2 px-3 hover:underline" href="/">Blog</a>
      </div>
      <template v-if="Object.keys(session).length === 0">
        <button
          class="bg-accent rounded-full font-garamond mr-4 text-white py-2 px-3 hover:bg-accent-hover"
          @click="loginHandler"
        >
          Log in
        </button>
        <button
          class="bg-light rounded-full font-garamond py-2 px-3 hover:bg-light-hover"
          @click="signupHandler"
        >
          Sign up
        </button>
      </template>

      <template v-else>
        <button
          class="font-bold font-garamond hover:underline"
          @click="userPageHandler"
        >
          You are logged in as: {{ session.user?.email }}
        </button>
        <button
          class="bg-accent rounded-full font-garamond text-white ml-4 py-2 px-3 hover:bg-accent-hover"
          @click="logoutHandler"
        >
          Log out
        </button>
      </template>
    </div>
  </div>
  <AuthController />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useAuthView } from "../stores/useAuthView";
import { AuthView, Session } from "../types/state";
import { getStateFromBackend } from "../utils/helper";
import AuthController from "../components/auth/AuthController.vue";
import Pinterest from "../components/icons/Pinterest.vue";
import * as helper from "../utils/helper";
import { logout } from "../utils/auth";

export default defineComponent({
  methods: {
    userPageHandler: (e: Event) => {
      // Prevent button triggers refresh
      e.preventDefault();
      helper.triggerBackendControl(e, "btnUserPage");
    },
    homePageHandler: (e: Event) => {
      e.preventDefault();
      helper.triggerBackendControl(e, "btnHomePage");
    },
  },
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

    const logoutHandler = async (event: Event) => {
      event.preventDefault();
      const { data, error } = await logout();
      if (error || !data) {
        alert(error?.message);
        return;
      }

      window.location.href = data.redirectUrl;
    };

    return {
      loginHandler,
      signupHandler,
      logoutHandler,
    };
  },
  data() {
    const session = getStateFromBackend<Session>("session");

    console.log(session);

    return { session };
  },
});
</script>
