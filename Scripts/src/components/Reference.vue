<template>
  <div id="reference" class="text-sm font-garamond">
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
import Icon from "../components/Icon.vue";

const icons = JSON.parse(
  (<HTMLInputElement>document.getElementById("iconsState")).value
);

export default {
  components: {
    Icon,
  },
  data() {
    return {
      icons,
    };
  },
};
</script>