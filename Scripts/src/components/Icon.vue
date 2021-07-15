<template>
  <div>
    <a :href="icon.href">
      <img
        class="bg-transparent hover:bg-light-hover inline m-3"
        :class="toggleFloatRight(icon.alt)"
        :src="icon.src"
        :alt="icon.alt"
      />
    </a>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import * as API from "../types/api";

export default defineComponent({
  methods: {
    toggleFloatRight(alt: string) {
      switch (alt) {
        case "Settings Icon":
        case "Add Icon":
          return "float-right";
        default:
          return "float-left";
      }
    },
  },
  props: {
    icon: { type: Object as PropType<API.Icon>, required: true },
  },
  data() {
    return {};
  },
});
</script>