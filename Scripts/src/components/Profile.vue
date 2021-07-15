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
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import * as API from "../types/api";

export default defineComponent({
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