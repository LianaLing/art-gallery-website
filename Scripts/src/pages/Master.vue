<template>
  <div class="flex py-5 px-6 items-center justify-between">
    <button class="flex items-center" @click="homePageHandler">
      <Pinterest />
      <h1 class="font-bold font-garamond text-xl text-accent">Art Gallery</h1>
    </button>
    <div class="flex font-bold text-dark items-center">
      <div class="mr-8">
        <a
          class="font-garamond mr-1 py-2 px-3 hover:underline"
          href="About.aspx"
          >About</a
        >
        <template v-if="Object.keys(session).length !== 0">
          <a
            class="font-garamond mr-1 py-2 px-3 hover:underline"
            href="Cart.aspx"
            >Cart</a
          >
        </template>
        <template
          v-if="
            Object.keys(session).length !== 0 &&
            claims[0].claimValue === 'artist'
          "
        >
          <a
            class="font-garamond mr-1 py-2 px-3 hover:underline"
            href="/Dashboard.aspx"
            >Dashboard</a
          >
        </template>
      </div>

      <template v-if="Object.keys(session).length === 0">
        <button
          class="
            bg-accent
            rounded-full
            font-garamond font-bold
            mr-4
            text-white
            py-2
            px-3
            hover:bg-accent-hover
          "
          @click="loginHandler"
        >
          Log in
        </button>
        <button
          class="
            bg-light
            rounded-full
            font-garamond font-bold
            py-2
            px-3
            hover:bg-light-hover
          "
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
          You are logged in as: {{ session.user?.userName }}
        </button>
        <button
          class="
            font-bold
            bg-accent
            rounded-full
            font-garamond
            text-white
            ml-4
            py-2
            px-3
            hover:bg-accent-hover
          "
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
import { getStateFromBackend, triggerBackendControl } from "../utils/helper";
import { logout } from "../utils/auth";
import { useSession } from "../stores/useSession";
import { Claims } from "../types/api";
import AuthController from "../components/auth/AuthController.vue";
import Pinterest from "../components/icons/Pinterest.vue";

export default defineComponent({
  components: { Pinterest, AuthController },
  methods: {
    userPageHandler: (e: Event) => {
      // Prevent button triggers refresh
      e.preventDefault();
      triggerBackendControl(e, "btnUserPage");
    },
    homePageHandler: (e: Event) => {
      e.preventDefault();
      triggerBackendControl(e, "btnHomePage");
    },
  },
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

    return { loginHandler, signupHandler, logoutHandler };
  },
  data() {
    const session = useSession();
    session.value = getStateFromBackend<Session>("session");
    const claims = <Claims[]>session.value.user?.claims;
    // const claims = claimsArr[0];
    return { session, claims };
  },
});
</script>
