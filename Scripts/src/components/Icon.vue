<template>
  <div>
    <a v-for="icon in icons" :key="icon.id" :href="icon.href">
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
import { defineComponent } from "vue";

export default defineComponent({
  methods: {
    toggleFloatRight(alt: string) {
      switch (alt) {
        case "Settings Icon":
        case "Add Icon":
          return this.position;
        default:
          return "";
      }
    },
  },
  data() {
    return {
      floatRight: false,
      position: "float-right",
      icons: [
        {
          id: "001",
          src: "https://img.icons8.com/material/24/000000/pencil--v1.png",
          alt: "Edit Icon",
          href: "/",
        },
        {
          id: "002",
          src: "https://img.icons8.com/material-rounded/24/000000/share.png",
          alt: "Share Icon",
          href: "/",
        },
        {
          id: "003",
          src: "https://img.icons8.com/material-outlined/24/000000/settings--v1.png",
          alt: "Settings Icon",
          href: "/Home",
        },
        {
          id: "004",
          src: "https://img.icons8.com/material-rounded/24/000000/add.png",
          alt: "Add Icon",
          href: "/",
        },
      ],
    };
  },
});
</script>