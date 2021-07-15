<template>
  <!-- Profile Header -->
  <Profile />

  <!-- Icons -->
  <div class="w-full">
    <Icon v-for="i in icons" :key="i.id" :icon="i" />
  </div>

  <!-- My saves -->
  <div class="flex w-full border-b-2">
    <Save v-for="save in saves" :key="save.id" :save="save" />
    <!-- <div class="m-14 mr-0 grid grid-cols-2 gap-[1px] w-[22.1rem]">
      <div class="inline">
        <a href="/">
          <img
            class="inline h-[161px] w-[11rem] object-cover rounded-l-2xl"
            src="https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg"
            alt="The Weasleys"
          />
        </a>
      </div>

      <div class="grid grid-rows-2 gap-[1px] h-[10rem]">
        <a href="/">
          <img
            class="inline h-[5rem] w-[5rem] object-cover rounded-tr-2xl"
            src="https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg"
            alt="The Weasleys"
          />
        </a>
        <a href="/">
          <img
            class="inline h-[5rem] w-[5rem] object-cover rounded-br-2xl"
            src="https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg"
            alt="The Weasleys"
          />
        </a>
      </div>
    </div>
    <div class="m-14 font-garamond">
      <strong class="text-xl">My saves</strong>
      <br />
      <h3 class="inline">12 Arts&nbsp;</h3>
      <h4 class="inline text-sm font-bold text-gray-500">9d</h4>
    </div>-->
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
      // saves: [
      //   {
      //     id: "save_0001",
      //     title: "My saves",
      //     pinNo: 12,
      //     updatedAt: 0,
      //     href: "/",
      //     arts: [
      //       {
      //         id: "svart_0001",
      //         imageSrc:
      //           "https://i.pinimg.com/564x/dc/c6/7e/dcc67ed0107da71b834615d48421efa4.jpg",
      //         title: "Still Life with Flowers in a Glass Vase",
      //       },
      //       {
      //         id: "svart_0002",
      //         imageSrc:
      //           "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg",
      //         title: "The Weasleys",
      //       },
      //       {
      //         id: "svart_0003",
      //         imageSrc:
      //           "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg",
      //         title: "A Wheatfield, with Cypresses",
      //       },
      //     ],
      //   },
      //   {
      //     id: "save_0002",
      //     title: "Harry Potter",
      //     pinNo: 0,
      //     updatedAt: 0,
      //     href: "/",
      //     arts: [
      //       {
      //         id: "svart_0004",
      //         imageSrc:
      //           "https://i.pinimg.com/564x/dc/c6/7e/dcc67ed0107da71b834615d48421efa4.jpg",
      //         title: "Still Life with Flowers in a Glass Vase",
      //       },
      //       {
      //         id: "svart_0005",
      //         imageSrc:
      //           "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg",
      //         title: "The Weasleys",
      //       },
      //       {
      //         id: "svart_0006",
      //         imageSrc:
      //           "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg",
      //         title: "A Wheatfield, with Cypresses",
      //       },
      //     ],
      //   },
      // ] as API.Save[],
      arts: [
        //   id: "art_0001",
        //   imageSrc:
        //     "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg",
        //   title: "Still Life with Flowers in a Glass Vase",
        //   price: 19000.0,
        // },
        // {
        //   id: "art_0010",
        //   imageSrc:
        //     "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg",
        //   title: "The Weasleys",
        //   price: 975.0,
        // },
        // {
        //   id: "art_0011",
        //   imageSrc:
        //     "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg",
        //   title: "The Weasleys",
        //   price: 975.0,
        // },
      ],
    };
  },
};
</script>
