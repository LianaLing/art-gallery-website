<template>
  <Icon class="float-left" :icon="icons[0]" />
  <div class="m-4 flex justify-center font-garamond">
    <div class="rounded-[30px] grid grid-cols-2" :class="artDivTransition">
      <img
        :src="art.url"
        :alt="art.description"
        class="object-fill rounded-tl-[30px] rounded-bl-[30px]"
      />
      <!-- Star -->
      <div class="p-10">
        <Popover class="inline float-left">
          <PopoverButton class="bg-transparent hover:bg-light-hover inline m-3">
            <img :src="icons[1].src" :alt="icons[1].alt" />
          </PopoverButton>
          <SavePopoverPanel :favs="favs" :art="art" :saved="saved" />
        </Popover>
        <!-- Like button which is not working -->
        <template v-if="!liked">
          <Icon class="float-left" :icon="icons[3]" @click="likeArtHandler" />
          <span class="inline"> {{ art.likes }} </span>
        </template>
        <template v-else>
          <Icon class="float-left" :icon="icons[4]" @click="likeArtHandler" />
          <span class="inline"> {{ art.likes }}</span>
        </template>
        <!-- Share -->
        <Share class="inline-block" :shareIcon="icons[2]" />
        <!-- Purchase -->
        <Popover class="inline float-right">
          <PopoverButton
            class="
              bg-accent
              float-right
              rounded-full
              font-bold
              text-light
              py-2
              px-4
              hover:bg-accent-hover
            "
          >
            Purchase
          </PopoverButton>
          <!-- Add to Cart, Purchase Now -->
          <PopoverPanel
            class="
              w-[160px]
              top-[210px]
              right-[445px]
              z-10
              absolute
              shadow-xl
              rounded-3xl
            "
          >
            <div class="bg-white rounded-3xl p-4 items-center">
              <template v-if="false">
                <button
                  class="
                    font-bold
                    bg-accent
                    rounded-full
                    py-2
                    px-4
                    m-2
                    justify-center
                    items-center
                    block
                    text-white
                  "
                  @click="addToCartHandler($event, art.id)"
                >
                  Add to Cart
                </button>
              </template>
              <template v-else>
                <button
                  class="
                    font-bold
                    bg-light-hover
                    rounded-full
                    py-2
                    px-4
                    m-2
                    justify-center
                    items-center
                    block
                  "
                  @click="addToCartHandler($event, art.id)"
                >
                  Add to Cart
                </button>
              </template>
              <button
                class="
                  font-bold
                  bg-accent
                  rounded-full
                  py-2
                  px-4
                  justify-center
                  items-center
                  block
                  text-white
                "
                @click="cartPageHandler"
              >
                Purchase Now
              </button>
            </div>
          </PopoverPanel>
        </Popover>

        <!-- Art Detail -->
        <p class="font-bold text-accent text-5xl py-4">{{ art.description }}</p>
        <img
          :src="art.author.avatarUrl"
          :alt="art.author.name + '\'s photo'"
          class="h-[32px] w-[32px] rounded-full object-fill inline-block mr-4"
        />
        <p class="text-xl inline py-4">{{ art.author.name }}</p>
        <p class="text-3xl py-4">RM {{ art.price }}</p>
        <p class="">Stock: {{ art.stock }}</p>
      </div>
    </div>
  </div>
  <!-- Reference -->
  <!-- <Reference /> -->
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import Icon from "../components/Icon.vue";
import Reference from "../components/Reference.vue";
import Share from "../components/Share.vue";
import { getStateFromBackend, triggerBackendControl } from "../utils/helper";
import * as API from "../types/api";
import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";
import SavePopoverPanel from "../components/SavePopoverPanel.vue";

const icons = getStateFromBackend<API.Icon[]>("iconsState");
const arts = getStateFromBackend<API.ArtResponse[]>("artState");
const favs = getStateFromBackend<API.FavouriteResponse[]>("favsState");
const saved = getStateFromBackend<API.FavResponse[]>("savedState");
const liked = getStateFromBackend<boolean>("likedState");

// All valid data is in the first response
const art = arts[0];

console.log(liked);

export default defineComponent({
  components: {
    Popover,
    PopoverButton,
    PopoverPanel,
    SavePopoverPanel,
    Icon,
    Share,
    Reference,
  },
  methods: {
    cartPageHandler(e: Event) {
      // Prevent button triggers refresh
      e.preventDefault();
      triggerBackendControl(e, "MainContent_btnCartPage");
    },
    addToCartHandler(e: Event, id: number) {
      // Prevent button triggers refresh
      e.preventDefault();
      alert("Clicked on add to cart button");
      triggerBackendControl(e, "MainContent_btnAddToCart", `${id}`);
    },
    likeArtHandler(e: Event) {
      e.preventDefault();
      triggerBackendControl(e, "MainContent_btnLikeHandler");
    },
  },
  data() {
    return {
      artDivTransition: "h[900px] w-[1000px] shadow-2xl",
      icons,
      art,
      favs,
      saved,
      liked,
    };
  },
});
</script>
