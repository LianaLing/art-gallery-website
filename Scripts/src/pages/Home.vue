<template>
  <div class="flex mx-auto w-full max-w-7xl py-8 justify-around">
    <div
      class="flex flex-col mx-4"
      v-for="arts in arts2D"
      :key="arts.toString()"
    >
      <ArtCard
        v-for="art in arts"
        :key="art.id"
        :art="art"
        :favourites="favs"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { getStateFromBackend, sliceIntoChunks } from "../utils/helper";
import { ArtResponse, FavResponse } from "../types/api";
import ArtCard from "../components/ArtCard.vue";
import { Field } from "vee-validate";

// Getting the data from code-behind
const arts = getStateFromBackend<ArtResponse[]>("arts");
const favsState = getStateFromBackend<FavResponse[]>("favs");

// const favNames = [...new Set(favsState.map((f) => f.name)];
// const favIds = [...new Set(favsState.map((f) => f.id))];
// const favs = [
//   ...new Set(
//     favsState.map((f) => {
//       return {
//         name: f.name,
//         id: f.id,
//       };
//     })
//   ),
// ];

const favState = favsState.map((f) => {
  return { name: f.name, id: f.id };
});

const favs = [] as { name: string; id: number }[];

favState.map((f) =>
  favs.filter((a) => a.name === f.name && a.id === f.id).length > 0
    ? null
    : favs.push(f)
);

// favState.filter(
//   (f, index, array) =>
//     array.findIndex((a) => a.name === f.name && a.id === f.id) === index
// );

// console.log(favs);

// Slice the data from 1D array to 2D array
// eg. [1, 2, 3, 4, 5, 6] -> [[1, 2, 3], [4, 5, 6]]
const arts2D = sliceIntoChunks<ArtResponse>(arts, 3);

export default defineComponent({
  components: { ArtCard },
  data() {
    return { arts2D, favs };
  },
});
</script>
