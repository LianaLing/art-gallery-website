<template>
  <template v-if="favs !== undefined">
    <PopoverPanel
      :class="transition"
      class="w-[200px] z-10 absolute shadow-xl rounded-3xl"
    >
      <div class="bg-white rounded-3xl p-4 items-center">
        <p>Your Favourite(s)</p>
        <template v-for="fav in favs" :key="fav.id">
          <template v-if="isSaved(art, saved, fav.id)">
            <button
              class="
                bg-accent
                rounded-3xl
                p-3
                justify-center
                items-center
                block
                text-white
              "
              @click="removeArtHandler($event, art.id, fav.id, btn)"
            >
              {{ fav.name }}
            </button>
          </template>
          <template v-else>
            <button
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
    removeArtHandler: (
      e: Event,
      art_id: number,
      fav_id: number,
      btn: string
    ) => {
      e.preventDefault();
      btn = btn.replace("Save", "Remove");
      const id = `${art_id}` + "," + `${fav_id}`;
      helper.triggerBackendControl(e, btn, id);
    },
    isSaved: (
      art: API.ArtResponse,
      saved: API.FavResponse[],
      fav_id: number
    ): boolean => {
      for (let i = 0; i < saved.length; i++) {
        if (saved[i].art.id === art.id && saved[i].id === fav_id) {
          return true;
        }
      }
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