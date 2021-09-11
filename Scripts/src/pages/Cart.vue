<template>
  <!-- Cart -->
  <div class="font-garamond mx-[350px] my-[50px]">
    <div class="pb-5 border-b-2">
      <p class="font-bold text-4xl">Your Cart</p>
    </div>
    <!-- Item -->
    <div v-for="art in arts" :key="art.id" class="py-5">
      <!-- Image -->
      <div>
        <img
          class="object-cover w-[8.5em] h-[8.5em]"
          :src="art.url"
          :alt="art.description"
        />
      </div>
      <!-- Description -->
      <div>
        {{ art.description }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { getStateFromBackend } from "../utils/helper";
import * as API from "../types/api";

const arts = getStateFromBackend<API.ArtResponse[]>("arts");

export default defineComponent({
  data() {
    return {
      arts,
    };
  },
});
</script>