<!-- User Page Save Collection -->

<template>
  <div
    class="m-14 grid w-[22.1rem] gap-[1px] grid-cols-2 overflow-hidden group"
  >
    <!-- Thumbnail -->
    <div class="relative">
      <a v-if="save.length !== 0 && save[0].art">
        <img
          class="object-cover rounded-l-2xl h-[161px] w-[11rem]"
          :src="save[0].art.url"
          :alt="save[0].art.description"
        />
        <a
          :href="save[0].art.url"
          class="
            h-full
            rounded-l-2xl
            w-full
            top-0
            absolute
            backdrop-filter backdrop-brightness-100
            group-hover:backdrop-brightness-75
          "
          :class="transition"
          target="_blank"
        />
      </a>
      <div
        v-else
        class="bg-light object-cover rounded-l-2xl h-[161px] w-[11rem]"
      />
    </div>

    <div class="h-[10rem] grid gap-[2px] grid-rows-2 relative">
      <a v-if="save.length > 1 && save[1].art">
        <img
          class="object-cover rounded-tr-2xl h-[5rem] w-[5rem]"
          :src="save[1].art.url"
          :alt="save[1].art.description"
        />
        <a
          :href="save[1].art.url"
          class="
            rounded-tr-2xl
            h-[5rem]
            top-0
            w-[5rem]
            absolute
            block
            backdrop-filter backdrop-brightness-100
            group-hover:backdrop-brightness-75
          "
          :class="transition"
          target="_blank"
        />
      </a>
      <div
        v-else
        class="bg-light object-cover rounded-tr-2xl h-[79px] w-[5rem]"
      />

      <a v-if="save.length > 2 && save[2].art">
        <img
          class="object-cover rounded-br-2xl h-[79px] w-[5rem]"
          :src="save[2].art.url"
          :alt="save[2].art.description"
        />
        <a
          :href="save[2].art.url"
          class="
            rounded-br-2xl
            h-[79px]
            top-[81px]
            w-[5rem]
            absolute
            block
            backdrop-filter backdrop-brightness-100
            group-hover:backdrop-brightness-75
          "
          :class="transition"
          target="_blank"
        />
      </a>
      <div
        v-else
        class="bg-light object-cover rounded-br-2xl h-[79px] w-[5rem]"
      />
    </div>

    <!-- Title -->
    <div v-if="save.length > 0" class="font-garamond mt-2">
      <a href="Construction.aspx" class="font-bold text-xl line-clamp-1">{{
        save[0].name
      }}</a>
      <br />
      <h3 v-if="count.totalArt <= 1" class="inline">
        {{ count.totalArt }} art &nbsp;
      </h3>
      <h3 v-else class="inline">{{ count.totalArt }} arts &nbsp;</h3>
      <!-- <h4 class="text-sm text-gray-500 inline">{{}}</h4> -->
    </div>
    <div v-else>No element</div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import * as API from "../types/api";

export default defineComponent({
  methods: {},
  props: {
    save: { type: Array as PropType<API.FavResponse[]>, required: true },
    count: { type: Object as PropType<API.FavArtCount>, required: true },
  },
  data() {
    console.log(this.save);
    console.log(this.count);
    return {
      transition: "transition ease-in-out duration-200",
    };
  },
});
</script>
