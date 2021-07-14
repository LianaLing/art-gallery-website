<template>
  <!-- <div class="m-14 mr-0 grid grid-cols-2 gap-[1px] w-[22.1rem]">
    <div class="inline">
      <a href="/">
        <img
          class="inline h-[161px] w-[11rem] object-cover rounded-l-2xl"
          src="https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg"
          :alt="title"
        />
      </a>
    </div>

    <div class="inline grid grid-rows-2 gap-[1px] h-[10rem]">
      <a href="/">
        <img
          class="inline h-[5rem] w-[5rem] object-cover rounded-tr-2xl"
          src="https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg"
          :alt="title"
        />
      </a>
      <a href="/">
        <img
          class="inline h-[5rem] w-[5rem] object-cover rounded-br-2xl"
          src="https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg"
          :alt="title"
        />
      </a>
    </div>
  </div> -->

  <div
    v-for="save in saves"
    :key="save.id"
    class="m-14 mr-0 grid grid-cols-2 gap-[1px] w-[22.1rem]"
  >
    <!-- Thumbnail -->
    <div class="inline">
      <a href="/">
        <img
          class="inline h-[161px] w-[11rem] object-cover rounded-l-2xl"
          :src="art[0]"
          :alt="title[0]"
        />
      </a>
    </div>

    <div class="inline grid grid-rows-2 gap-[1px] h-[10rem]">
      <a href="/">
        <img
          class="inline h-[5rem] w-[5rem] object-cover rounded-tr-2xl"
          :src="art[1]"
          :alt="title[1]"
        />
      </a>
      <a href="/">
        <img
          class="inline h-[5rem] w-[5rem] object-cover rounded-br-2xl"
          :src="art[2]"
          :alt="title[2]"
        />
      </a>
    </div>

    <!-- Title -->
    <div class="mt-14 font-garamond">
      <strong class="text-xl">
        {{ save.title }}
      </strong>
      <br />
      <h3 class="inline">{{ save.pinNo }}&nbsp;</h3>
      <h4 class="inline text-sm text-gray-500 font-bold">
        {{ save.updatedAt }}
      </h4>
      <button
        @onclick="lastUpdated($event, save.updatedAt)"
        class="block bg-accent"
      >
        {{ save.updatedAt }}
      </button>
    </div>
  </div>

  <!-- Title -->
  <!-- <div class="m-14 font-garamond">
    <strong class="text-xl">My saves</strong>
    <br />
    <h3 class="inline">12 Arts&nbsp;</h3>
    <h4 class="inline text-sm text-gray-500 font-bold">9d</h4>
  </div>

  <div v-for="save in saves" :key="save.id" class="m-14 font-garamond">
    <strong class="text-xl">
      {{ save.title }}
    </strong>
    <br />
    <h3 class="inline">{{ save.pinNo }}&nbsp;</h3>
    <h4 class="inline text-sm text-gray-500 font-bold">{{ save.updatedAt }}</h4>
    <button
      @onclick="lastUpdated($event, save.updatedAt)"
      class="block bg-accent"
    >
      {{ save.updatedAt }}
    </button>
  </div> -->
  <!-- <button
    v-for="save in saves"
    :key="save.id"
    lastUpdated($event,
    save.updatedAt)
  >
    Last updated timestamp {{ save.updatedAt }}
  </button> -->
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
    art: { type: Array as PropType<string[]>, required: true },
    title: { type: Array as PropType<string[]>, required: true },
    saves: { type: Array as PropType<API.Save[]>, required: true },
  },
  data() {
    return {};
  },
});
</script>