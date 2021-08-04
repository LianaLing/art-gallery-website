<template>
  <div
    class="
      m-14
      grid grid-cols-2
      gap-[1px]
      w-[22.1rem]
      rounded-2xl
      overflow-hidden
      group
    "
  >
    <!-- Thumbnail -->
    <div class="relative">
      <a :href="save[0].art.url">
        <img
          v-if="save[0].art !== undefined"
          class="h-[161px] w-[11rem] object-cover rounded-l-2xl"
          :src="save[0].art.url"
          :alt="save[0].art.description"
        />
        <div
          v-else
          class="h-[161px] w-[11rem] object-cover rounded-l-2xl bg-light"
        />
      </a>
      <a
        :href="save[0].art.url"
        class="
          absolute
          top-0
          w-full
          h-full
          rounded-l-2xl
          backdrop-filter backdrop-brightness-100
          group-hover:backdrop-brightness-75
        "
        :class="transition"
      />
    </div>

    <div class="relative grid grid-rows-2 gap-[2px] h-[10rem]">
      <a :href="save[1].art.url">
        <img
          v-if="save[1].art !== undefined"
          class="h-[5rem] w-[5rem] object-cover rounded-tr-2xl"
          :src="save[1].art.url"
          :alt="save[1].art.description"
        />
        <div
          v-else
          class="h-[79px] w-[5rem] object-cover rounded-tr-2xl bg-light"
        />
        <a
          :href="save[1].art.url"
          class="
            absolute
            block
            top-0
            h-[5rem]
            w-[5rem]
            rounded-tr-2xl
            backdrop-filter backdrop-brightness-100
            group-hover:backdrop-brightness-75
          "
          :class="transition"
        />
      </a>
      <a :href="save[2].art.url">
        <img
          v-if="save[2].art !== undefined"
          class="h-[79px] w-[5rem] object-cover rounded-br-2xl"
          :src="save[2].art.url"
          :alt="save[3].art.description"
        />
        <div
          v-else
          class="h-[79px] w-[5rem] object-cover rounded-br-2xl bg-light"
        />
        <a
          :href="save[2].art.url"
          class="
            absolute
            block
            top-[81px]
            h-[79px]
            w-[5rem]
            rounded-br-2xl
            backdrop-filter backdrop-brightness-100
            group-hover:backdrop-brightness-75
          "
          :class="transition"
        />
      </a>
    </div>

    <!-- Title -->
    <div class="mt-2 font-garamond">
      <a :href="save[0].art.url" class="text-xl font-bold line-clamp-1">{{
        save[0].name
      }}</a>
      <br />
      <h3 v-if="save[0].id <= 1" class="inline">{{ save[0].id }} art &nbsp;</h3>
      <h3 v-else class="inline">{{ save[0].id }} arts &nbsp;</h3>
      <h4 class="inline text-sm text-gray-500">{{}}</h4>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import * as API from "../types/api";

export default defineComponent({
  methods: {
    // parseWithTitle() {},
    lastUpdated(e: Event, updatedAt: number) {
      // Not working for some reason
      updatedAt = updatedAt + e.timeStamp;
      console.log(JSON.parse(JSON.stringify(e)));
    },
  },
  props: {
    save: { type: Object as PropType<API.FavResponse[]>, required: true },
  },
  data() {
    return {
      transition: "transition ease-in-out duration-200",
    };
  },
});
</script>