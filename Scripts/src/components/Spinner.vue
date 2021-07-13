<template>
  <svg
    class="animate-spin"
    :class="sizes[size]"
    xmlns="http://www.w3.org/2000/svg"
    fill="none"
    viewBox="0 0 24 24"
  >
    <path
      fill="currentColor"
      d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
    />
  </svg>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

const sizes = {
  small: "h-2 w-2",
  medium: "h-4 w-4",
  big: "h-6 w-6",
};

export default defineComponent({
  props: {
    size: { type: String as PropType<keyof typeof sizes>, required: true },
  },
  data() {
    return { sizes };
  },
});
</script>