<template>
  <Popover>
    <PopoverButton class="bg-transparent hover:bg-light-hover inline m-3">
      <img :src="shareIcon.src" :alt="shareIcon.alt" />
    </PopoverButton>

    <PopoverPanel class="shadow-xl w-[300px] z-10 absolute rounded-3xl">
      <div class="bg-white rounded-3xl p-4">
        <h3 class="font-bold text-lg text-center w-full">Share this artwork</h3>
        <div class="flex mt-4 items-start justify-around">
          <button
            onclick="(() =>  {window.open('https://api.whatsapp.com/send?phone=60163066883&text=Check%20out%20this%20artwork!%0D%0Dhttps://github.com/lianaling/art-gallery-website', '_blank');})()"
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