<template>
  <div class="text-sm font-garamond">
    <h1>Credits to:</h1>
    <a
      v-for="ref in icons"
      :key="ref.id"
      :href="ref.creditRef"
      class="text-gray-500"
    >
      -&nbsp;{{ ref.alt }} by {{ ref.author }} -&nbsp;</a
    >
  </div>
</template>

  <script lang="ts">
import { defineComponent, PropType } from "vue";
import Icon from "../components/Icon.vue";

const icons = JSON.parse(
  (<HTMLInputElement>document.getElementById("iconsState")).value
);

export default defineComponent({
  prop: {},
  components: {
    Icon,
  },
  data() {
    return {
      icons,
    };
  },
});
</script>