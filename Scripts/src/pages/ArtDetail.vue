<template>
  <Icon class="float-left" :icon="icons[0]" />
  <div class="m-4 flex justify-center font-garamond">
    <div class="rounded-[30px] grid grid-cols-2" :class="artDivTransition">
      <img
        :src="art.url"
        :alt="art.description"
        class="object-fill rounded-tl-[30px] rounded-bl-[30px]"
      />
      <div class="p-10">
        <Popover class="inline float-left">
          <PopoverButton class="bg-transparent hover:bg-light-hover inline m-3">
            <img :src="icons[1].src" :alt="icons[1].alt" />
          </PopoverButton>
          <SavePopoverPanel
            :favs="favs"
            :art="art"
            btn="MainContent_btnSaveStar"
          />
        </Popover>
        <template v-if="!like">
          <Icon class="float-left" :icon="icons[3]" @onclick="like = true" />
          <span class="inline"> {{ art.likes }} </span>
        </template>
        <template v-else>
          <Icon class="float-left" :icon="icons[4]" @onclick="like = false" />
          <span class="inline"> {{ art.likes + 1 }}</span>
        </template>
        <Share class="inline-block" :shareIcon="icons[2]" />
        <button
          class="
            bg-accent
            float-right
            rounded-full
            font-bold
            text-light
            py-2
            px-4
            m-2
            hover:bg-accent-hover
          "
        >
          Purchase
        </button>

        <p class="font-bold text-accent text-5xl py-4">{{ art.description }}</p>
        <img
          :src="art.author.avatarUrl"
          :alt="art.author.name + '\'s photo'"
          class="h-[32px] w-[32px] rounded-full object-fill inline-block mr-4"
        />
        <p class="text-xl inline py-4">{{ art.author.name }}</p>
        <p class="text-3xl py-4">RM{{ art.price }}</p>
        <p class="">Stock: {{ art.stock }}</p>
      </div>
    </div>
  </div>
  <Reference />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Icon from "../components/Icon.vue";
import Reference from "../components/Reference.vue";
import Share from "../components/Share.vue";
import { getStateFromBackend } from "../utils/helper";
import * as API from "../types/api";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import SavePopoverPanel from "../components/SavePopoverPanel.vue";

const icons = getStateFromBackend<API.Icon[]>("iconsState");
const arts = getStateFromBackend<API.ArtResponse[]>("artState");
const favs = getStateFromBackend<API.FavouriteResponse[]>("favsState");
const art = arts[0];

const like = false;

export default defineComponent({
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
    SavePopoverPanel,
    Icon,
    Share,
    Reference,
  },
  methods: {},
  data() {
    return {
      artDivTransition: "h[900px] w-[1000px] shadow-2xl",
      icons,
      art,
      favs,
      like,
    };
  },
});
</script>