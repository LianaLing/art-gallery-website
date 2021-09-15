<template>
  <div class="flex py-5 px-6 items-center justify-between">
    <button class="flex items-center" @click="homePageHandler">
      <Pinterest />
      <h1 class="font-bold font-garamond text-xl text-accent">Art Gallery</h1>
    </button>
    <div class="flex font-bold text-dark items-center">
      <div class="flex items-center mr-8">
        <a
          class="font-garamond mr-1 py-2 px-3 hover:underline"
          href="About.aspx"
          >About</a
        >
        <template
          v-if="
            Object.keys(session).length !== 0 &&
            session &&
            session.user &&
            claims[0].claimValue === 'artist'
          "
        >
          <a
            class="font-garamond mr-1 py-2 px-3 hover:underline"
            href="/Dashboard.aspx"
            >Dashboard</a
          >
        </template>
        <div
          class="relative"
          v-if="
            Object.keys(session).length !== 0 &&
            session &&
            session.cart &&
            session.cart.total !== 0
          "
        >
          <a
            class="font-garamond mr-1 py-2 px-3 hover:underline cursor-pointer"
            @click="cartHandler"
          >
            Cart
          </a>
          <div
            class="
              flex
              justify-center
              items-center
              absolute
              top-0
              right-0
              w-4
              h-4
              rounded-full
              bg-accent
              text-white
            "
            style="font-size: 10px"
          >
            {{ session.cart.total }}
          </div>
        </div>
      </div>

      <template
        v-if="Object.keys(session).length !== 0 && session && session.user"
      >
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

      <template v-else>
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
    const session = useSession();
    const backendSession = getStateFromBackend<Session>("session");

    if (!backendSession) {
      session.value = { user: null, cart: null };
    } else if (backendSession && backendSession.user && backendSession.cart) {
      session.value = backendSession;
    } else if (backendSession && backendSession.user) {
      session.value = { ...backendSession, cart: null };
    } else if (backendSession && backendSession.cart) {
      session.value = { ...backendSession, user: null };
    }

    console.log(session.value);

    const claims = <Claims[]>session.value.user?.claims;

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

    const cartHandler = (event: Event) => {
      event.preventDefault();

      if (Object.keys(session.value).length === 0) {
        alert("Error occured");
        return;
      }

      if (!session.value.cart) {
        alert("You don't have anything in your cart yet");
        return;
      }

      window.location.href = "Cart.aspx";
    };

    return {
      loginHandler,
      signupHandler,
      logoutHandler,
      cartHandler,
      session,
      claims,
    };
  },
});
</script>
