<template>
  <div class="">
    <!-- Profile Header -->
    <Profile :profile="session.user" />

    <!-- Icons -->
    <div class="w-full flex justify-center">
      <div
        v-for="i in icons"
        :key="i.id"
        class="
          bg-transparent
          mx-6
          my-2
          px-4
          py-4
          cursor-pointer
          hover:bg-light-hover
          rounded-full
        "
      >
        <button @click="btnHandler($event, i.alt)">
          <img :src="i.src" :alt="i.alt" class="inline" />
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
          <template v-for="fav in saves" :key="fav.id">
            <SaveCard
              v-if="fav && fav.art && fav.author"
              class="mb-10"
              :card="fav"
            />
          </template>
        </div>
      </div>
    </div>
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

console.log(favGroup);
console.log(counts);

export default defineComponent({
  methods: {
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
