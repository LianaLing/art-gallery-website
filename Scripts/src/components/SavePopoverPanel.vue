<template>
  <template v-if="favs !== undefined">
    <PopoverPanel
      :class="transition"
      class="w-[200px] z-10 absolute shadow-xl rounded-3xl"
    >
      <div class="bg-white rounded-3xl p-4 items-center">
        <p>Your Collection(s)</p>
        <template v-if="isSaved(art, saved)">
          <button
            v-for="fav in favs"
            :key="fav.id"
            class="
              bg-accent
              rounded-3xl
              p-3
              justify-center
              items-center
              block
              text-white
            "
            @click="saveArtHandler($event, art.id, fav.id, btn)"
          >
            {{ fav.name }}
          </button>
        </template>
        <template v-else>
          <button
            v-for="fav in favs"
            :key="fav.id"
            class="
              bg-light-hover
              rounded-3xl
              p-3
              justify-center
              items-center
              block
            "
            @click="saveArtHandler($event, art.id, fav.id, btn)"
          >
            {{ fav.name }}
          </button>
        </template>
      </div>
    </PopoverPanel>
  </template>
  <template v-else> Surprise </template>
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
    saveArtHandler: (e: Event, art_id: number, fav_id: number, btn: string) => {
      e.preventDefault();
      const id = `${art_id}` + "," + `${fav_id}`;
      helper.triggerBackendControl(e, btn, id);
    },
    isSaved: (art: API.ArtResponse, saved: API.FavResponse[]): boolean => {
      for (let i = 0; i < saved.length; i++) {
        if (saved[i].art.id === art.id) {
          console.log("trueeeeeeee");
          return true;
        }
      }
      console.log("false");
      return false;
    },
  },
  props: {
    favs: { type: Array as PropType<API.FavouriteResponse[]>, required: true },
    art: { type: Object as PropType<API.ArtResponse>, required: true },
    transition: { type: String as PropType<string> },
    saved: { type: Array as PropType<API.FavResponse[]>, required: true },
    btn: { type: String as PropType<string>, required: true },
  },
  data() {
    return {};
  },
});
</script>