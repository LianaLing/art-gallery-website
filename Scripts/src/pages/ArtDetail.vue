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
        <Icon class="float-left" :icon="icons[1]" />
        <Icon class="float-left" :icon="icons[3]" />
        <span class="inline"> {{ art.likes }}</span>
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

const icons = getStateFromBackend<API.Icon[]>("iconsState");
const arts = getStateFromBackend<API.ArtResponse[]>("artState");
const art = arts[0];

export default defineComponent({
  components: {
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
    };
  },
});
</script>