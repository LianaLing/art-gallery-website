<template>
  <!-- Profile Header -->
  <Profile :profile="inprofile" />

  <!-- Icons -->
  <div class="w-full">
    <Icon
      :class="toggleFloatRight(i.alt)"
      v-for="i in icons"
      :key="i.id"
      :icon="i"
    />
  </div>

  <!-- My saves -->
  <div class="flex mx-10 w-full border-b-2">
    <Save v-for="save in saves" :key="save.id" :save="save" />
  </div>

  <!-- Unorganised Saves -->
  <div class="my-14 justify-around font-garamond border-b-2">
    <strong class="inline text-xl">Unorganised Saves</strong>

    <a
      class="
        inline
        float-right
        px-3
        py-2
        font-bold
        rounded-full
        bg-light
        hover:bg-light-hover
      "
      href="/"
      >Organise</a
    >
    <div class="flex w-full my-14 max-w-7xl">
      <div
        class="flex flex-col mx-24"
        v-for="arts in arts2D"
        :key="arts.toString()"
      >
        <ArtCard v-for="art in arts" :key="art.id" :art="art" :saved="true" />
      </div>
    </div>
  </div>

  <Reference />

  <br />
</template>

<script lang="ts">
import Icon from "../components/Icon.vue";
import Save from "../components/Save.vue";
import Profile from "../components/Profile.vue";
import ArtCard from "../components/ArtCard.vue";
import Reference from "../components/Reference.vue";
import { sliceIntoChunks } from "../utils/helper";
import * as API from "../types/api";

const icons = JSON.parse(
  (<HTMLInputElement>document.getElementById("iconsState")).value
);

const saves = JSON.parse(
  (<HTMLInputElement>document.getElementById("savesState")).value
);

const inprofile = JSON.parse(
  (<HTMLInputElement>document.getElementById("profileState")).value
);

const artsState = JSON.parse(
  (<HTMLInputElement>document.getElementById("artsState")).value
);

const arts2D = sliceIntoChunks<API.Art>(artsState, 3);

export default {
  methods: {
    toggleFloatRight(alt: string) {
      switch (alt) {
        case "Settings Icon":
        case "Add Icon":
          return "float-right";
        default:
          return "float-left";
      }
    },
  },
  components: {
    Icon,
    Save,
    Profile,
    ArtCard,
    Reference,
  },
  data() {
    return {
      icons,
      saves,
      inprofile,
      arts2D,
    };
  },
};
</script>
