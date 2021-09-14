<template>
  <div class="bg-transparent hover:bg-light-hover inline m-3">
    <a :href="icon.href">
      <img :src="icon.src" :alt="icon.alt" />
    </a>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import * as API from "../types/api";

export default defineComponent({
  methods: {},
  props: {
    icon: { type: Object as PropType<API.Icon>, required: true },
  },
  data() {
    return {};
  },
});
</script>
