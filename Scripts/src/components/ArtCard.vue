<template>
  <div class="relative group filter drop-shadow-none w-[300px]">
    <img
      :alt="art.title"
      class="
        w-[300px]
        h-full
        object-cover
        rounded-3xl
        backdrop-filter backdrop-brightness-50
      "
      :src="art.imageSrc"
    />
    <div
      class="
        absolute
        top-0
        w-full
        h-full
        rounded-3xl
        backdrop-filter backdrop-brightness-100
        group-hover:backdrop-brightness-75 group-hover:cursor-[zoom-in]
      "
      :class="transition"
    ></div>
    <button
      class="
        absolute
        px-4
        py-2
        font-bold
        rounded-full
        opacity-0
        bg-red
        hover:bg-red-hover
        text-light
        top-4
        right-4
        group-hover:opacity-100
      "
      :class="transition"
    >
      Save
    </button>
    <p
      class="
        absolute
        px-4
        py-2
        font-semibold
        rounded-full
        opacity-0
        bottom-4
        left-4
        group-hover:opacity-100
        text-dark
        bg-light
      "
      :class="transition"
    >
      RM {{ art.price.toFixed(2) }}
    </p>
    <Popover
      class="absolute opacity-0 bottom-4 right-4 group-hover:opacity-100"
      :class="transition"
    >
      <PopoverButton
        class="
          p-2
          rounded-full
          bg-light
          focus:outline-none
          hover:bg-light-hover
        "
        :class="transition"
      >
        <Search />
      </PopoverButton>

      <PopoverPanel class="absolute z-10 bottom-12 right-0 w-[300px] shadow-xl">
        <div class="p-4 bg-white rounded-3xl">
          <h3 class="w-full text-lg font-bold text-center">
            Share this artwork
          </h3>
          <div class="flex items-start justify-around mt-4">
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
                  p-3
                  w-[48px]
                  h-[48px]
                  flex
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
                  p-3
                  w-[48px]
                  h-[48px]
                  flex
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
  <div class="w-[300px] mb-8">
    <p class="px-2 mt-1 font-bold line-clamp-2">{{ art.title }}</p>
    <div class="flex items-center px-2 mt-1">
      <img :src="art.author.avatarUrl" class="w-[24px] rounded-full" />
      <span class="ml-1 text-sm">{{ art.author.name }}</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import Whatsapp from "../components/icons/Whatsapp.vue";
import Facebook from "../components/icons/Facebook.vue";
import Email from "../components/icons/Email.vue";
import Link from "../components/icons/Link.vue";
import Search from "../components/icons/Search.vue";
import * as API from "../types/api";

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
    art: { type: Object as PropType<API.Art>, required: true },
  },
  data() {
    return {
      transition: "transition ease-in-out duration-200",
    };
  },
});
</script>