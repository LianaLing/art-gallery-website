<template>
  <!-- Profile Header -->
  <div class="rounded-full justify-items-center mt-10">
    <a :href="user.profileUrl">
      <img
        class="rounded-full h-[130px] w-[130px] mx-auto block"
        :src="user.avatarUrl"
        alt="User avatar"
      />
    </a>
  </div>
  <!-- <Popover
    class="absolute opacity-0 bottom-4 right-4 group-hover:opacity-100"
    :class="transition"
  >
    <PopoverButton
      class="p-2 rounded-full bg-light focus:outline-none hover:bg-light-hover"
      :class="transition"
    >
      <Search />
    </PopoverButton>
    <PopoverPanel class="absolute z-10 bottom-12 right-0 w-[300px] shadow-xl">
      <h1>TEST</h1>
    </PopoverPanel>
  </Popover> -->
  <br />
  <h1 class="text-center text-4xl font-bold font-garamond">{{ user.name }}</h1>
  <h2 class="text-center text-lg text-gray-500 font-bold font-garamond">
    @{{ user.username }}
  </h2>
  <h2 class="text-center text-lg text-gray-500 font-bold font-garamond">
    {{ user.following }} following
  </h2>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import * as API from "../types/api";

export default defineComponent({
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
  },
  data() {
    return {
      transition: "transition ease-in-out duration-200",
      user: {
        name: "Liana Ling",
        username: "lianaling",
        following: 44,
        avatarUrl: "https://avatars.githubusercontent.com/u/68136684?v=4",
        profileUrl: "/",
      } as API.User,
    };
  },
});
</script>