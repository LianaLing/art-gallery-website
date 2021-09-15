<template>
  <div class="">
    <!-- Profile Header -->
    <Profile :profile="session.user" />

    <!-- Icons -->
    <div class="w-full flex justify-center">
      <!-- <Icon
        :class="toggleFloatRight(i.alt)"
        v-for="i in icons"
        :key="i.id"
        :icon="i"
      /> -->

      <div v-for="i in icons" :key="i.id" class="bg-transparent px-10 py-6">
        <button @click="btnHandler($event, i.alt)">
          <img
            :src="i.src"
            :alt="i.alt"
            class="inline hover:bg-light-hover rounded-full"
          />
        </button>
      </div>
    </div>

    <!-- My saves -->
    <div class="flex mx-auto w-full max-w-7xl pb-2 justify-around">
      <div class="flex flex-row mx-4">
        <!-- Iterate through each group -->
        <template v-for="(favs, key, i) in favGroup" :key="favs.id">
          <Save :save="favs" :count="counts[i]" />
        </template>
      </div>
    </div>

    <!-- Unorganised Saves -->
    <div class="font-garamond border-b-2 border-t-2 pt-24 mt-14">
      <p class="text-3xl text-center font-bold pb-6">All Saves</p>
      <div class="flex mx-auto w-full max-w-7xl py-8 justify-around">
        <div
          class="flex flex-col mx-4"
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

    <!-- <Reference /> -->

    <br />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useSession } from "../stores/useSession";
import Icon from "../components/Icon.vue";
import Save from "../components/Save.vue";
import Profile from "../components/Profile.vue";
import SaveCard from "../components/SaveCard.vue";
import Reference from "../components/Reference.vue";
import { sliceIntoChunks, triggerBackendControl } from "../utils/helper";
import * as API from "../types/api";
import * as helper from "../utils/helper";
import { Session } from "../types/state";

const icons = JSON.parse(
  (<HTMLInputElement>document.getElementById("iconsState")).value
);

const data = helper.getStateFromBackend<API.FavResponse[]>("state");
const saves2D = helper.splitIntoNArrays<API.FavResponse>(data, 3);
const counts = helper.getStateFromBackend<API.FavArtCount[]>("countsState");

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

export default defineComponent({
  methods: {
    // triggerFloatRight(str: string) {
    //   if (str === "Settings Icon" || str === "Add Icon") {
    //     this.floatright = "float-right";
    //     return this.floatright;
    //   } else {
    //     this.floatright = "";
    //     return this.floatright;
    //   }
    // },
    btnHandler(e: Event, str: string) {
      e.preventDefault();
      if (str === "Settings Icon") {
        triggerBackendControl(e, "MainContent_btnUserDetailPage");
      } else if (str === "Add Icon") {
        triggerBackendControl(e, "MainContent_btnShowCreateFav");
      } else if ((str = "Activity History Icon")) {
        triggerBackendControl(e, "MainContent_btnShowPH");
      } else {
        window.alert("Button function is not implemented yet.");
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
    const session = useSession();
    return {
      icons,
      saves2D,
      data,
      session,
      favGroup,
      counts,
      // floatright: "",
    };
  },
});
</script>
