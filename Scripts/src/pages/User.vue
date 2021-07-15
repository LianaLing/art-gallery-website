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
  <div class="m-14 font-garamond">
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
  </div>

  <div id="reference">
    <div>
      Icons made by
      <a href="https://www.flaticon.com/authors/prosymbols" title="Prosymbols"
        >Prosymbols</a
      >
      from
      <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a>
    </div>

    <a href="https://icons8.com/icon/5824/pencil">Pencil icon by Icons8</a>
    <br />
    <a href="https://icons8.com/icon/83213/share">Share icon by Icons8</a>
    <br />
    <a href="https://icons8.com/icon/82535/settings">Settings icon by Icons8</a>
    <br />
    <a href="https://icons8.com/icon/85096/add">Add icon by Icons8</a>
    <br />
  </div>
</template>

<script lang="ts">
import Icon from "../components/Icon.vue";
import Save from "../components/Save.vue";
import Profile from "../components/Profile.vue";
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

export default {
  components: {
    Icon,
    Save,
    Profile,
  },
  data() {
    return {
      icons,
      saves,
      inprofile,
    };
  },
};
</script>
