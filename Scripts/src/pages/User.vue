<template>
  <!-- Profile Header -->
  <Profile :profile="inprofile" />

  <!-- Icons -->
  <div class="w-full">
    <Icon v-for="i in icons" :key="i.id" :icon="i" />
  </div>

  <!-- My saves -->
  <div class="flex w-full border-b-2">
    <Save v-for="save in saves" :key="save.id" :save="save" />
  </div>

  <!-- Unorganised Saves -->
  <div class="m-14 font-garamond border-b-2">
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
    <div class="flex justify-around w-full py-8 mx-auto max-w-7xl">
      <div
        class="flex flex-col mx-4"
        v-for="arts in arts2D"
        :key="arts.toString()"
      >
        <ArtCard v-for="art in arts" :key="art.id" :art="art" />
      </div>
    </div>
  </div>

  <div id="reference" class="text-sm font-garamond">
    <h1>Credits to:</h1>
    <a
      v-for="ref in icons"
      :key="ref.id"
      :href="ref.creditRef"
      class="text-gray-500"
    >
      -&nbsp;{{ ref.alt }} by {{ ref.author }} -&nbsp;</a
    >
  </div>
</template>

<script lang="ts">
import Icon from "../components/Icon.vue";
import Save from "../components/Save.vue";
import Profile from "../components/Profile.vue";
import ArtCard from "../components/ArtCard.vue";
import { sliceIntoChunks } from "../utils/Helper";
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
  components: {
    Icon,
    Save,
    Profile,
    ArtCard,
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
