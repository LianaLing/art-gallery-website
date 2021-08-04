<template>
  <!-- Profile Header -->
  <Profile :profile="data[0].user" />

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
    <!-- Iterate through each group -->
    <template v-for="favs in favGroup" :key="favs.toString">
      <Save :save="favs" />
    </template>
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
      <div class="flex flex-col mx-24" v-for="fav in data" :key="fav.id">
        <ArtCard :art="fav.art" :saved="true" />
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
import * as helper from "../utils/helper";

const icons = JSON.parse(
  (<HTMLInputElement>document.getElementById("iconsState")).value
);

const data = helper.getStateFromBackend<API.FavResponse[]>("state");
// const info = data[0];

// const saves = JSON.parse(
//   (<HTMLInputElement>document.getElementById("savesState")).value
// );

// const inprofile = JSON.parse(
//   (<HTMLInputElement>document.getElementById("profileState")).value
// );

// const artsState = JSON.parse(
//   (<HTMLInputElement>document.getElementById("artsState")).value
// );

// const arts2D = sliceIntoChunks<API.FavResponse>(data, 3);

const favGroup = new Array<API.FavResponse[]>();
let g = 0;
// Returns a bunch of favs that is ungrouped
for (let i = 0; i < data.length; i++) {
  if (i !== 0) {
    // Check if favs is in the same group
    if (data[i].name !== data[i - 1].name) {
      g++; //Change group and create new group
      favGroup[g] = new Array<API.FavResponse>();
    }
  } else {
    // At first element, create a new group
    favGroup[g] = new Array<API.FavResponse>();
  }
  favGroup[g].push(data[i]);
}

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
      // saves,
      // inprofile,
      // arts2D,
      data,
      favGroup,
    };
  },
};
</script>
