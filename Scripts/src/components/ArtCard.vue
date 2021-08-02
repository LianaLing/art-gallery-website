<template>
  <div class="font-garamond w-[300px] relative group filter drop-shadow-none">
    <img
      :alt="art.description"
      class="
        h-full
        object-cover
        rounded-3xl
        w-[300px]
        backdrop-filter backdrop-brightness-50
      "
      :src="art.url"
    />
    <div
      class="
        h-full
        rounded-3xl
        w-full
        top-0
        absolute
        backdrop-filter
        group-hover:cursor-[zoom-in]
        backdrop-brightness-100
        group-hover:backdrop-brightness-75
      "
      @click="artDetailPageHandler($event, art.id)"
      :class="transition"
    ></div>
    <template v-if="!saved">
      <button
        class="
          bg-accent
          rounded-full
          font-bold
          text-light
          opacity-0
          py-2
          px-4
          top-4
          right-4
          absolute
          hover:bg-accent-hover
          group-hover:opacity-100
        "
        :class="transition"
      >
        Save
      </button>
    </template>
    <p
      class="
        bg-light
        rounded-full
        font-semibold
        text-dark
        opacity-0
        py-2
        px-4
        bottom-4
        left-4
        absolute
        group-hover:opacity-100
      "
      :class="transition"
    >
      RM {{ art.price.toFixed(2) }}
    </p>
    <Popover
      class="opacity-0 right-4 bottom-4 absolute group-hover:opacity-100"
      :class="transition"
    >
      <PopoverButton
        class="
          bg-light
          rounded-full
          p-2
          focus:outline-none
          hover:bg-light-hover
        "
        :class="transition"
      >
        <Search />
      </PopoverButton>

      <PopoverPanel class="shadow-xl right-0 bottom-12 w-[300px] z-10 absolute">
        <div class="bg-white rounded-3xl p-4">
          <h3 class="font-bold text-lg text-center w-full">
            Share this artwork
          </h3>
          <div class="flex mt-4 items-start justify-around">
            <button onclick="(() => alert('Share {{ id }} WhatsApp'))()">
              <Whatsapp />
              <span class="mt-1 text-xs">WhatsApp</span>
            </button>
            <button
              class="flex flex-col items-center"
              onclick="(() => alert('Share to Facebook'))()"
            >
              <Facebook />
              <span class="mt-1 text-xs">Facebook</span>
            </button>
            <button
              class="flex flex-col items-center"
              onclick="(() => alert('Share to Email'))()"
            >
              <div
                class="
                  bg-light-hover
                  rounded-full
                  flex
                  h-[48px]
                  p-3
                  w-[48px]
                  justify-center
                  items-center
                "
              >
                <Email />
              </div>
              <span class="mt-1 text-xs">Email</span>
            </button>
            <button
              class="flex flex-col items-center"
              onclick="(() => alert('Copy Link'))()"
            >
              <div
                class="
                  bg-light-hover
                  rounded-full
                  flex
                  h-[48px]
                  p-3
                  w-[48px]
                  justify-center
                  items-center
                "
              >
                <Link />
              </div>
              <span class="mt-1 text-xs">Copy Link</span>
            </button>
          </div>
        </div>
      </PopoverPanel>
    </Popover>
  </div>
  <div class="font-garamond mb-8 w-[300px]">
    <p class="font-bold mt-1 px-2 line-clamp-2">{{ art.description }}</p>
    <div class="flex mt-1 px-2 items-center">
      <img :src="art.author.avatarUrl" class="rounded-full h-7 w-7" />
      <span class="text-sm ml-2">{{ art.author.name }}</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import { ArtResponse } from "../types/api";
import Whatsapp from "../components/icons/Whatsapp.vue";
import Facebook from "../components/icons/Facebook.vue";
import Email from "../components/icons/Email.vue";
import Link from "../components/icons/Link.vue";
import Search from "../components/icons/Search.vue";
import * as helper from "../utils/helper";

export default defineComponent({
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
    Whatsapp,
    Facebook,
    Email,
    Link,
    Search,
  },
  props: {
    art: { type: Object as PropType<ArtResponse>, required: true },
    saved: { type: Object as PropType<boolean> },
  },
  methods: {
    artDetailPageHandler: (e: Event, id: number) => {
      // Prevent button triggers refresh
      e.preventDefault();
      // alert("clicked on art card from artcard");
      helper.triggerBackendControl(e, "btnArtDetailPage");
    },
  },
  emits: ["detail"],
  data() {
    return {
      transition: "transition ease-in-out duration-200",
    };
  },
});
</script>
