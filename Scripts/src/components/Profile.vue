<template>
  <!-- Profile Header -->
  <div class="rounded-full justify-items-center mt-10">
    <a :href="profile.profileUrl">
      <img
        class="rounded-full h-[130px] w-[130px] mx-auto block"
        :src="profile.avatarUrl"
        alt="User avatar"
      />
    </a>
  </div>
  <br />
  <h1 class="text-center text-4xl font-bold font-garamond">
    {{ profile.name }}
  </h1>
  <h2 class="text-center text-lg text-gray-500 font-bold font-garamond">
    @{{ profile.username }}
  </h2>
  <h2 class="text-center text-lg text-gray-500 font-bold font-garamond">
    {{ profile.following }} following
  </h2>
  <Popover
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
      <div class="p-4 bg-white rounded-3xl">
        <h3 class="w-full text-lg font-bold text-center">Share this artwork</h3>
        <div class="flex items-start justify-around mt-4"></div>
      </div>
    </PopoverPanel>
  </Popover>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import * as API from "../types/api";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";

export default defineComponent({
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
  },
  props: {
    profile: { type: Object as PropType<API.Profile>, required: true },
  },
  data() {
    return {
      transition: "transition ease-in-out duration-200",
    };
  },
});
</script>