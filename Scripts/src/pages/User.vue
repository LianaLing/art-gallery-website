<template>
  <!-- Profile Header -->
  <Profile :profile="session.user" />

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
  <div class="flex border-b-2 mx-10 w-full">
    <!-- Iterate through each group -->
    <template v-for="favs in favGroup" :key="favs.id">
      <Save :save="favs" />
    </template>
  </div>

  <!-- Unorganised Saves -->
  <div class="font-garamond border-b-2 my-14 justify-around">
    <strong class="text-xl inline">All Saves</strong>

    <!-- <a
      class="bg-light rounded-full font-bold py-2 px-3 inline float-right hover:bg-light-hover"
      href="/"
      >Organise</a
    > -->
    <div class="flex my-14 w-full max-w-7xl">
      <div
        class="flex flex-col mx-24"
        v-for="saves in saves2D"
        :key="saves.toString"
      >
        <SaveCard
          class="mb-10"
          v-for="fav in saves"
          :key="fav.id"
          :card="fav"
        />
      </div>
    </div>
  </div>

  <Reference />

  <br />
</template>

<script lang="ts">
import { useSession } from "../stores/useSession";
import Icon from "../components/Icon.vue";
import Save from "../components/Save.vue";
import Profile from "../components/Profile.vue";
import SaveCard from "../components/SaveCard.vue";
import Reference from "../components/Reference.vue";
import { sliceIntoChunks } from "../utils/helper";
import * as API from "../types/api";
import * as helper from "../utils/helper";
import { Session } from "../types/state";

const icons = JSON.parse(
  (<HTMLInputElement>document.getElementById("iconsState")).value
);

const data = helper.getStateFromBackend<API.FavResponse[]>("state");
const session = helper.getStateFromBackend<Session>("session");
const saves2D = sliceIntoChunks<API.FavResponse>(data, 2);

const names = [...new Set(data.map((d) => d.name))];

const favGroup: Record<string, API.FavResponse[]> = {};

names.forEach((name) => {
  favGroup[name] = data.filter((d) => d.name === name);
});

// const favGroup = new Array<API.FavResponse[]>();
// let g = 0;
// // Returns a bunch of favs that is ungrouped
// for (let i = 0; i < data.length; i++) {
//   if (i !== 0) {
//     // Check if favs is in the same group
//     if (data[i].name !== data[i - 1].name) {
//       //Change group and create new group
//       favGroup[++g] = new Array<API.FavResponse>();
//     }
//   } else {
//     // At first element, create a new group
//     favGroup[g] = new Array<API.FavResponse>();
//   }
//   favGroup[g].push(data[i]);
// }

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
    SaveCard,
    Reference,
  },
  data() {
    return {
      icons,
      // saves,
      // inprofile,
      saves2D,
      data,
      session,
      favGroup,
    };
  },
};
</script>
