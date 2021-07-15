<template>
  <!-- Profile Header -->
  <div class="rounded-full justify-items-center mt-10">
    <a href="/">
      <img
        class="rounded-full h-[130px] w-[130px] mx-auto block"
        src="https://lumiere-a.akamaihd.net/v1/images/character_princess_jasmine_7da1e27a.jpeg?region=0%2C0%2C450%2C450"
        alt="Jasmine profile picture"
      />
    </a>
  </div>
  <br />
  <h1 class="text-center text-4xl font-bold font-garamond">Liana Ling</h1>
  <h2 class="text-center text-lg text-gray-500 font-bold font-garamond">
    @lianaling
  </h2>
  <h2 class="text-center text-lg text-gray-500 font-bold font-garamond">
    1 following
  </h2>

  <!-- Icons -->
  <Icon class="w-full">Test</Icon>

  <!-- My saves -->
  <div class="border-b-2 w-full">
    <Save
      v-for="save in saves"
      :key="save.id"
      :art="save.arts"
      :saves="saves"
    ></Save>
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

      <div class="inline grid grid-rows-2 gap-[1px] h-[10rem]">
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
      <h4 class="inline text-sm text-gray-500 font-bold">9d</h4>
    </div>-->
  </div>

  <!-- Unorganised Saves -->
  <div class="m-14 font-garamond">
    <strong class="text-xl inline">Unorganised Saves</strong>

    <a
      class="
        px-3
        py-2
        font-bold
        bg-light
        hover:bg-light-hover
        rounded-full
        inline
        float-right
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
import { sliceIntoChunks } from "../utils/Helper";
import * as API from "../types/api";

// const iconId = JSON.parse(
//   (<HTMLInputElement>document.getElementById("iconId")).value
// );

// const icons2D = sliceIntoChunks<API.Icon>(iconId, 4);
// const arts = JSON.parse(
//   (<HTMLInputElement>document.getElementById("state")).value
// );
// Slice the data from 1D array to 2D array
// eg. [1, 2, 3, 4, 5, 6] -> [[1, 2, 3], [4, 5, 6]]

export default {
  components: {
    Icon,
    Save,
  },
  data() {
    return {
      // authors: [
      //   {
      //     id: "aut_0001",
      //     name: "Liana",
      //     avatarUrl: "/",
      //   },
      // ],
      saves: (<unknown>[
        {
          id: <unknown>"save_0001",
          title: <unknown>"My saves",
          pinNo: <unknown>12,
          updatedAt: <unknown>0,
          href: "/",
          arts: (<unknown>[
            {
              id: <unknown>"saveart_0001",
              imageSrc: <unknown>[
                "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg",
                "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg",
                "https://i.pinimg.com/564x/dc/c6/7e/dcc67ed0107da71b834615d48421efa4.jpg",
              ],
              title: <unknown>[
                "Still Life with Flowers in a Glass Vase",
                "The Weasleys",
                "A Wheatfield, with Cypresses",
              ],
            },
          ]) as API.SaveArt,
        } as API.Save,
        {
          id: <unknown>"save_0002",
          title: <unknown>"Harry Potter",
          pinNo: <unknown>12,
          updatedAt: <unknown>0,
          href: "/",
          arts: (<unknown>[
            {
              id: <unknown>"saveart_0002",
              imageSrc: <unknown>[
                "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg",
                "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg",
                "https://i.pinimg.com/564x/dc/c6/7e/dcc67ed0107da71b834615d48421efa4.jpg",
              ],
              title: <unknown>[
                "Still Life with Flowers in a Glass Vase",
                "The Weasleys",
                "A Wheatfield, with Cypresses",
              ],
            },
          ]) as API.SaveArt,
        } as API.Save,
      ]) as API.Save[],
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
