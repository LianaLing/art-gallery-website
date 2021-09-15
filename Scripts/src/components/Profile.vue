<template>
  <template v-if="profile">
    <!-- Profile Header -->
    <div class="rounded-full justify-items-center mt-10">
      <a href="" v-if="profile.avatarUrl">
        <img
          class="rounded-full h-[130px] w-[130px] mx-auto block object-cover"
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
      @{{ profile.userName }}
    </h2>
    <h2 class="text-center text-lg text-gray-500 font-bold font-garamond">
      {{ profile.email }}
    </h2>
    <Popover>
      <PopoverButton
        class="
          p-2
          rounded-full
          bg-light
          focus:outline-none
          hover:bg-light-hover
        "
        :class="transition"
      >
        <Search />
      </PopoverButton>

      <PopoverPanel class="absolute z-10 bottom-12 right-0 w-[300px] shadow-xl">
        <div class="p-4 bg-white rounded-3xl">
          <h3 class="w-full text-lg font-bold text-center">
            Share this artwork
          </h3>
          <div class="flex items-start justify-around mt-4"></div>
        </div>
      </PopoverPanel>
    </Popover>
  </template>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import * as API from "../types/api";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import { Session } from "../types/state";
import Search from "../components/icons/Search.vue";

export default defineComponent({
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
    Search,
  },
  props: {
    profile: { type: Object as PropType<Session["user"]>, required: true },
  },
  data() {
    return {
      transition: "transition ease-in-out duration-200",
    };
  },
});
</script>
