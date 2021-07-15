<template>
  <div
    class="
      m-14
      mr-0
      grid grid-cols-2
      gap-[1px]
      w-[22.1rem]
      rounded-2xl
      overflow-hidden
    "
  >
    <!-- Thumbnail -->
    <div class="">
      <a :href="save.href">
        <img
          v-if="save.arts[0] !== undefined"
          class="h-[161px] w-[11rem] object-cover rounded-l-2xl"
          :src="save.arts[0].imageSrc"
          :alt="save.arts[0].title"
        />
        <div
          v-else
          class="h-[161px] w-[11rem] object-cover rounded-l-2xl bg-light"
        />
      </a>
    </div>

    <div class="grid grid-rows-2 gap-[1px] h-[10rem]">
      <a :href="save.href">
        <img
          v-if="save.arts[1] !== undefined"
          class="h-[5rem] w-[5rem] object-cover rounded-tr-2xl"
          :src="save.arts[1].imageSrc"
          :alt="save.arts[1].title"
        />
        <div
          v-else
          class="h-[79.5px] w-[5rem] object-cover rounded-tr-2xl bg-light"
        />
      </a>
      <a :href="save.href">
        <img
          v-if="save.arts[2] !== undefined"
          class="h-[5rem] w-[5rem] object-cover rounded-br-2xl"
          :src="save.arts[2].imageSrc"
          :alt="save.arts[2].title"
        />
        <div
          v-else
          class="h-[79.5px] w-[5rem] object-cover rounded-br-2xl bg-light"
        />
      </a>
    </div>

    <!-- Title -->
    <div class="mt-2 font-garamond">
      <a :href="save.href" class="text-xl font-bold line-clamp-1">{{
        save.title
      }}</a>
      <br />
      <h3 v-if="save.pinNo <= 1" class="inline">{{ save.pinNo }} art &nbsp;</h3>
      <h3 v-else class="inline">{{ save.pinNo }} arts &nbsp;</h3>
      <h4 class="inline text-sm text-gray-500">{{ save.updatedAt }}d</h4>
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
    save: { type: Object as PropType<API.Save>, required: true },
  },
  data() {
    return {};
  },
});
</script>