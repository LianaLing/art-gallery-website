<template>
  <PopoverPanel class="shadow-xl right-0 top-14 w-[200px] z-10 absolute">
    <div class="bg-white rounded-3xl p-4 items-center">
      <p>Your Collection(s)</p>
      <button
        v-for="fav in favs"
        :key="fav.id"
        class="bg-light-hover rounded-3xl p-3 justify-center items-center block"
        @click="saveArtHandler($event, art.id, fav.id)"
      >
        {{ fav.name }}
      </button>
    </div>
  </PopoverPanel>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import * as API from "../types/api";
import * as helper from "../utils/helper";

export default defineComponent({
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
  },
  methods: {
    saveArtHandler: (e: Event, art_id: number, fav_id: number) => {
      e.preventDefault();
      const id = `${art_id}` + "," + `${fav_id}`;
      helper.triggerBackendControl(e, "MainContent_btnSaveArt", id);
    },
  },
  props: {
    favs: { type: Array as any, required: true },
    art: { type: Object as PropType<API.ArtResponse>, required: true },
  },
});
</script>