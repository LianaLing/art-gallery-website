<template>
  <Dialog
    :open="authView.modalOpen"
    @close="setAuthView({ ...authView, modalOpen: !authView.modalOpen })"
    class="font-garamond"
  >
    <DialogOverlay class="fixed inset-0 bg-black opacity-50" />

    <div
      class="
        bg-white
        fixed
        w-[95%]
        sm:w-[484px]
        top-[50%]
        left-[50%]
        transform
        translate-x-[-50%] translate-y-[-50%]
        rounded-2xl
        shadow-2xl
        flex flex-col
        items-center
      "
      :class="authView.view === 'login' ? 'py-8' : 'pt-8'"
    >
      <Pinterest />
      <button
        @click="setAuthView({ ...authView, modalOpen: false })"
        class="
          absolute
          p-2
          rounded-full
          focus:outline-none
          right-6
          top-6
          hover:bg-light-hover
        "
      >
        <svg
          class="w-6 h-6"
          fill="currentColor"
          viewBox="0 0 20 20"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            fill-rule="evenodd"
            d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
            clip-rule="evenodd"
          />
        </svg>
      </button>
      <h3 class="mt-6 px-8 text-4xl font-semibold text-center">
        Welcome to Art Gallery
      </h3>
      <p v-show="authView.view !== 'login'" class="mt-1">
        {{
          authView.view === "signup"
            ? "Look for new Art"
            : "Make living out of passion"
        }}
      </p>
      <Form
        @submit="handleAuth"
        :validation-schema="validationSchema"
        v-slot="{ errors, isSubmitting }"
        class="flex flex-col w-full px-24 mt-6"
      >
        <Field
          name="email"
          type="email"
          :placeholder="authView.view === 'login' ? 'Email' : 'Email Address'"
          :class="[
            errors.email ? 'border-red' : 'border-light-hover',
            inputClass,
          ]"
        />
        <ErrorMessage name="email" class="mt-2 text-xs text-red" />

        <Field
          name="password"
          type="password"
          :placeholder="
            authView.view === 'login' ? 'Password' : 'Create a password'
          "
          :class="[
            errors.password ? 'border-red' : 'border-light-hover',
            inputClass,
          ]"
        />
        <ErrorMessage name="password" class="mt-2 text-xs text-red" />

        <a
          v-show="authView.view === 'login'"
          class="mt-2 text-sm font-bold text-gray-700"
          href="/"
          >Forgotten your password?</a
        >

        <button
          class="
            px-4
            py-2
            mt-6
            font-bold
            text-white
            rounded-3xl
            bg-accent
            focus:outline-none
            hover:bg-accent-hover
          "
        >
          <Spinner v-if="isSubmitting" :size="'medium'" :class="'mx-auto'" />
          <span v-else>
            {{
              authView.view === "login"
                ? "Log In"
                : authView.view === "artist_signup"
                ? "Create account"
                : "Continue"
            }}
          </span>
        </button>

        <button
          v-show="authView.view === 'artist_signup'"
          class="
            px-4
            py-2
            mt-2
            font-bold
            rounded-3xl
            bg-light
            focus:outline-none
            hover:bg-light-hover
          "
        >
          <span>Log in to existing account</span>
        </button>
      </Form>
      <p class="text-xs text-center mt-6 text-gray-500 px-24">
        By continuing, you agree to Art Gallery's
        <span class="text-black font-bold">Terms of Service</span> and
        acknowledge that you've read our
        <span class="text-black font-bold">Privacy Policy</span>
      </p>
      <div
        class="w-28 border-b-[1px] border-black my-4"
        style="-webkit-transform: scaleY(0.1)"
      />
      <button
        class="text-xs text-center font-bold"
        @click="
          setAuthView({
            ...authView,
            view: authView.view === 'login' ? 'signup' : 'login',
          })
        "
      >
        {{
          authView.view === "login"
            ? "Not on Art Gallery yet? Sign-up"
            : "Already a member? Log in"
        }}
      </button>
      <a class="text-xs text-center font-bold mt-2"
        >Are you an artist? Get started here!</a
      >
      <button
        v-show="authView.view === 'signup' || authView.view === 'artist_signup'"
        class="
          font-bold
          w-full
          mt-6
          py-5
          rounded-b-2xl
          text-center
          bg-light-hover
        "
        @click="
          setAuthView({
            ...authView,
            view: authView.view === 'signup' ? 'artist_signup' : 'signup',
          })
        "
      >
        {{
          authView.view === "signup"
            ? "Create a free artist account"
            : "Create a personal account"
        }}
      </button>
    </div>
  </Dialog>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Dialog, DialogOverlay } from "@headlessui/vue";
import { Form, Field, ErrorMessage, configure } from "vee-validate";
import * as Yup from "yup";
import { sleep } from "../../utils/Helper";
import { useAuthView } from "../../stores/useAuthView";
import { AuthView } from "../../types/state";
import Pinterest from "../icons/Pinterest.vue";
import Spinner from "../Spinner.vue";
import { post } from "../../utils/http";
import { string } from "yup/lib/locale";

type AuthPayload = {
  email: string;
  password: string;
  type?: "personal" | "artist";
};

// Configure vee-validate params (form validation)
configure({
  validateOnBlur: false, // will not validate when user clicks outside of input
});

export default defineComponent({
  components: {
    Dialog,
    DialogOverlay,
    Pinterest,
    Form,
    Field,
    ErrorMessage,
    Spinner,
  },
  setup() {
    const authView = useAuthView();

    const setAuthView = (val: AuthView) => (authView.value = val);

    const loginHandler = (event: Event) => {
      // prevent button triggers refresh
      event.preventDefault();
      setAuthView({ ...authView.value, modalOpen: true });
    };

    return {
      authView,
      setAuthView,
      loginHandler,
    };
  },
  data() {
    const validationSchema = Yup.object({
      email: Yup.string()
        .email("That doesn't look like an email address")
        .required("Email cannot be empty"),
      password: Yup.string()
        .required("Password cannot be empty")
        .min(8, "Password must be longer than 8 character"),
    });

    const inputClass = `
      w-full
      px-4
      py-2
      mt-2
      border-2
      rounded-2xl
      focus:outline-none
      focus:border-light-hover
      focus:ring focus:ring-accent
    `;

    return { validationSchema, inputClass };
  },
  methods: {
    async handleAuth({ email, password }: AuthPayload) {
      await sleep(2000);

      const payload: AuthPayload = {
        email,
        password,
        type:
          this.authView.view === "signup"
            ? "personal"
            : this.authView.view === "artist_signup"
            ? "artist"
            : undefined,
      };

      switch (this.authView.view) {
        case "login": {
          const { data, ok } = await post<{
            d: { email: string; password: string };
          }>("UserAuth.asmx/Login", payload);
          if (!ok || !data) {
            alert("Something went wrong");
            return;
          }
          alert(JSON.stringify(data.d, null, 2));
          break;
        }
        case "signup": {
          const { data, ok } = await post<{
            d: { email: string; password: string; type: string };
          }>("UserAuth.asmx/SignUp", payload);
          if (!ok || !data) {
            alert("Something went wrong");
            return;
          }
          alert(JSON.stringify(data.d, null, 2));
          break;
        }
        case "artist_signup": {
          const { data, ok } = await post<{
            d: { email: string; password: string; type: string };
          }>("UserAuth.asmx/SignUp", payload);
          if (!ok || !data) {
            alert("Something went wrong");
            return;
          }
          alert(JSON.stringify(data.d, null, 2));
          break;
        }
        case "forgot_password":
          break;
      }
    },
  },
});
</script>