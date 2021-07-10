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
        @click="loginHandler"
      >
        Sign up
      </button>
    </div>
  </div>

  <Dialog :open="loginOpen" @close="setLoginOpen" class>
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
        py-8
        px-12
        shadow-2xl
        flex flex-col
        items-center
      "
    >
      <Pinterest />
      <button
        @click="setLoginOpen(false)"
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
      <h3 class="mt-6 text-4xl font-semibold text-center">
        Welcome to Art Gallery
      </h3>
      <Form
        @submit="sayHelloWorld"
        :validation-schema="loginValidationSchema"
        v-slot="{ errors, isSubmitting }"
        class="flex flex-col w-full px-8 mt-6"
      >
        <Field
          name="email"
          type="email"
          placeholder="Email"
          class="
            w-full
            px-4
            py-2
            border-2
            rounded-2xl
            focus:outline-none
            focus:border-light-hover
            focus:ring focus:ring-accent
          "
          :class="[errors.email ? 'border-red' : 'border-light-hover']"
        />
        <ErrorMessage name="email" class="mt-2 text-xs text-red" />

        <Field
          name="password"
          type="password"
          placeholder="Password"
          class="
            w-full
            px-4
            py-2
            mt-2
            border-2
            rounded-2xl
            focus:outline-none
            focus:border-light-hover
            focus:ring focus:ring-accent
          "
          :class="[errors.password ? 'border-red' : 'border-light-hover']"
        />
        <ErrorMessage name="password" class="mt-2 text-xs text-red" />

        <a class="mt-2 text-sm font-bold text-gray-700" href="/"
          >Forgotten your password?
        </a>

        <button
          class="
            px-4
            py-2
            mt-6
            font-semibold
            text-white
            rounded-3xl
            bg-accent
            focus:outline-none
            hover:bg-accent-hover
          "
        >
          <Spinner v-if="isSubmitting" :size="'medium'" :class="'mx-auto'" />
          <span v-else>Log In</span>
        </button>
      </Form>
      <p class="text-xs text-center mt-6 text-gray-500 px-8">
        By continuing, you agree to Art Gallery's
        <span class="text-black font-bold">Terms of Service</span> and
        acknowledge that you've read our
        <span class="text-black font-bold">Privacy Policy</span>
      </p>
      <div
        class="w-28 border-b-[1px] border-black my-4"
        style="-webkit-transform: scaleY(0.1)"
      />
      <a class="text-xs text-center font-bold"
        >Not on Art Gallery yet? Sign-up</a
      >
      <a class="text-xs text-center font-bold mt-2"
        >Are you an artist? Get started here!</a
      >
    </div>
  </Dialog>
</template>

<script lang="ts">
import { ref } from "vue";
import { Dialog, DialogOverlay } from "@headlessui/vue";
import { Form, Field, ErrorMessage, configure } from "vee-validate";
import { sleep } from "../utils/Helper";
import * as Yup from "yup";
import Pinterest from "../components/icons/Pinterest.vue";
import Spinner from "../components/Spinner.vue";

// Configure vee-validate params (form validation)
configure({
  validateOnBlur: false, // will not validate when user clicks outside of input
});

export default {
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
    const loginOpen = ref(false);

    const setLoginOpen = (val: boolean) => (loginOpen.value = val);
    const loginHandler = (event: Event) => {
      // prevent button triggers refresh
      event.preventDefault();
      setLoginOpen(true);
    };

    return {
      loginOpen,
      setLoginOpen,
      loginHandler,
    };
  },
  data() {
    const loginValidationSchema = Yup.object({
      email: Yup.string()
        .email("That doesn't look like an email address")
        .required("Email cannot be empty"),
      password: Yup.string()
        .required("Password cannot be empty")
        .min(8, "Password must be longer than 8 character"),
    });
    return { loginValidationSchema };
  },
  methods: {
    async sayHelloWorld() {
      await sleep(2000);
      const res = await fetch("UserAuth.asmx/HelloWorld", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const resJSON = await res.json();
      alert(`Server Response: ${resJSON.d}`);
    },
  },
};
</script>