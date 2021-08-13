<template>
  <Dialog
    :open="authView.modalOpen"
    @close="setAuthView({ ...authView, modalOpen: !authView.modalOpen })"
    class="font-garamond"
  >
    <DialogOverlay class="bg-black opacity-50 inset-0 fixed" />

    <div
      class="
        bg-white
        flex flex-col
        rounded-2xl
        transform
        top-[50%]
        left-[50%]
        shadow-2xl
        w-[95%]
        translate-x-[-50%] translate-y-[-50%]
        fixed
        items-center
        sm:w-[484px]
      "
      :class="authView.view === 'login' ? 'py-8' : 'pt-8'"
    >
      <Pinterest />
      <button
        @click="setAuthView({ ...authView, modalOpen: false })"
        class="
          rounded-full
          p-2
          top-6
          right-6
          absolute
          hover:bg-light-hover
          focus:outline-none
        "
      >
        <svg
          class="h-6 w-6"
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
      <h3 class="font-semibold mt-6 text-center px-8 text-4xl">
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
        class="flex flex-col mt-6 w-full px-24"
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
          class="font-bold mt-2 text-sm text-gray-700"
          href="/"
          >Forgotten your password?</a
        >

        <button
          class="
            bg-accent
            font-bold
            rounded-3xl
            mt-6
            text-white
            py-2
            px-4
            hover:bg-accent-hover
            focus:outline-none
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
            bg-light
            font-bold
            rounded-3xl
            mt-2
            py-2
            px-4
            hover:bg-light-hover
            focus:outline-none
          "
        >
          <span>Log in to existing account</span>
        </button>
      </Form>
      <p class="mt-6 text-xs text-center px-24 text-gray-500">
        By continuing, you agree to Art Gallery's
        <span class="font-bold text-black">Terms of Service</span> and
        acknowledge that you've read our
        <span class="font-bold text-black">Privacy Policy</span>
      </p>
      <div
        class="border-black border-b-[1px] my-4 w-28"
        style="-webkit-transform: scaleY(0.1)"
      />
      <button
        class="font-bold text-xs text-center"
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
      <a class="font-bold mt-2 text-xs text-center"
        >Are you an artist? Get started here!</a
      >
      <button
        v-show="authView.view === 'signup' || authView.view === 'artist_signup'"
        class="
          bg-light-hover
          font-bold
          rounded-b-2xl
          mt-6
          text-center
          w-full
          py-5
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
import * as Yup from "yup";
import { defineComponent } from "vue";
import { Dialog, DialogOverlay } from "@headlessui/vue";
import { Form, Field, ErrorMessage, configure } from "vee-validate";
import { useAuthView } from "../../stores/useAuthView";
import { AuthView } from "../../types/state";
import { regex } from "../../utils/regex";
import { BaseAuthParam, login, signup } from "../../utils/auth";
import Pinterest from "../icons/Pinterest.vue";
import Spinner from "../Spinner.vue";

// Configure vee-validate params (form validation)
configure({
  validateOnBlur: false, // will not validate when user clicks outside of input
  validateOnChange: true,
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
    // This is the validaion schema for login/signup form
    const validationSchema = Yup.object({
      email: Yup.string()
        .email("That doesn't look like an email address")
        .required("Email cannot be empty"),
      password: Yup.string()
        .required("Password cannot be empty")
        .min(8, "Password must be longer than 8 character")
        .matches(
          regex.password,
          "Must have at least one uppercase letter, one lowercase letter, one number and one special character"
        ),
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
    async handleAuth({ email, password }: BaseAuthParam) {
      const role =
        this.authView.view === "artist_signup" ? "artist" : "personal";

      switch (this.authView.view) {
        case "login": {
          const { data, error } = await login({
            email,
            password,
            remember: false,
          });

          // If failed to authenticate user
          if (error || !data) {
            console.error(error);
            alert(error?.message);
            return;
          }

          // If successfully authenticate user
          this.setAuthView({ ...this.authView, modalOpen: false }); // close the modal
          window.location.href = data.redirectUrl; // redirect to the page told by backend
          break;
        }
        case "signup":
        case "artist_signup": {
          const { data, error } = await signup({ email, password, role });

          if (error || !data) {
            console.error(error);
            alert(error?.message);
            return;
          }

          this.setAuthView({ ...this.authView, modalOpen: false });
          window.location.href = data.redirectUrl;
          break;
        }
        case "forgot_password":
          break;
      }
    },
  },
});
</script>
