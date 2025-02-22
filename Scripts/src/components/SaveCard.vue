<template>
  <div class="font-garamond w-[300px] relative group filter drop-shadow-none">
    <img
      :alt="card.art.description"
      class="
        h-full
        object-cover
        rounded-3xl
        w-[300px]
        backdrop-filter backdrop-brightness-50
      "
      :src="card.art.url"
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
      @click="artDetailPageHandler($event, card.art.id)"
      :class="transition"
    ></div>

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
      onclick=""
    >
      {{ card.name }}
    </button>
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
      RM {{ card.art.price.toFixed(2) }}
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
            <button
              onclick="(() => {window.open('https://api.whatsapp.com/send?phone=60163066883&text=Check%20out%20this%20artwork!%0D%0Dhttps://github.com/lianaling/art-gallery-website', '_blank');})()"
            >
              <Whatsapp />
              <span class="mt-1 text-xs">WhatsApp</span>
            </button>
            <button
              class="flex flex-col items-center"
              onclick="(() => {window.open('https://www.facebook.com/sharer/sharer.php?u=https://GitHub.com/lianaling/art-gallery-website', '_blank');})()"
            >
              <Facebook />
              <span class="mt-1 text-xs">Facebook</span>
            </button>
            <button
              class="flex flex-col items-center"
              onclick="(() => {window.open('https://mail.google.com/mail/u/0/?fs=1&to=lianalingliya@gmail.com&su=Greetings%20from%20Art%20Gallery!&body=BODY&bcc=leeky-wp18@student.tarc.edu.my&tf=cm', '_blank');})()"
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
              onclick="(() =>{ const el = document.createElement('textarea'); el.value = 'https://github.com/lianaling/art-gallery-website'; document.body.appendChild(el); el.select(); document.execCommand('copy'); document.body.removeChild(el); alert('Copied share link')})()"
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
    card: { type: Object as PropType<API.FavResponse>, required: true },
  },
  methods: {
    artDetailPageHandler: (e: Event, id: number) => {
      // Prevent button triggers refresh
      e.preventDefault();
      // alert("clicked on art card from artcard");
      helper.triggerBackendControl(
        e,
        "MainContent_btnSaveArtDetailPage",
        `${id}`
      );
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
