<template>
  <Popover>
    <PopoverButton class="bg-transparent hover:bg-light-hover inline m-3">
      <img :src="shareIcon.src" :alt="shareIcon.alt" />
    </PopoverButton>

    <PopoverPanel class="shadow-xl right-0 bottom-12 w-[300px] z-10 absolute">
      <div class="bg-white rounded-3xl p-4">
        <h3 class="font-bold text-lg text-center w-full">Share this artwork</h3>
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
</template>

<script lang="ts">
import { PropType } from "vue";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import Whatsapp from "../components/icons/Whatsapp.vue";
import Facebook from "../components/icons/Facebook.vue";
import Email from "../components/icons/Email.vue";
import Link from "../components/icons/Link.vue";
import Icon from "../components/Icon.vue";
import Search from "../components/icons/Search.vue";
import * as API from "../types/api";

export default {
  props: {
    shareIcon: { type: Object as PropType<API.Icon>, required: true },
  },
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
    Whatsapp,
    Facebook,
    Email,
    Link,
    Search,
    Icon,
  },
  data() {
    return { transition: "" };
  },
};
</script>