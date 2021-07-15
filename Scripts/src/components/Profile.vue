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
import * as API from "../types/api";

export default defineComponent({
  data() {
    return {
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