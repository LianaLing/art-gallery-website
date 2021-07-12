<template>
  <div class="m-3 inline">
    <!-- <a :href="icon.href">
        <img
          class="bg-transparent hover:bg-light-hover inline"
          :src="icon.iconSrc"
          :alt="icon.title"
        />
      </a> -->
    <button onclick="(() => alert('Clicked on edit button'))()">
      <EditIcon />
    </button>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import EditIcon from "../components/icons/EditIcon.vue";
import * as API from "../types/api";

export default defineComponent({
  components: {
    EditIcon,
  },
  props: {
    icon: { type: Object as PropType<API.Icon>, required: true },
  },
  data() {
    return {};
  },
});
</script>
