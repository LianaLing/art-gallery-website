<template>
  <div class="flex mx-auto w-full max-w-7xl py-8 justify-around">
    <div
      class="flex flex-col mx-4"
      v-for="(arts, index) in arts2D"
      :key="index"
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
import { defineComponent, ref, watch } from "vue";
import { breakpointsTailwind, useBreakpoints } from "@vueuse/core";
import { getStateFromBackend, splitIntoNArrays } from "../utils/helper";
import { ArtResponse, FavResponse } from "../types/api";
import ArtCard from "../components/ArtCard.vue";

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

export default defineComponent({
  components: { ArtCard },
  setup() {
    // Helpers to check current viewport width
    const breakpoints = useBreakpoints(breakpointsTailwind);
    const greaterThanLg = breakpoints.greater("lg");
    const sm = breakpoints.smaller("sm");

    // Define the size based on first load's viewport width
    const initialSize = greaterThanLg.value ? 3 : sm.value ? 1 : 2;

    // Define the arts array as a Ref so that it can be manipulated
    // to trigger component re-render
    const arts2D = ref<ArtResponse[][]>(
      splitIntoNArrays<ArtResponse>(arts, initialSize)
    );

    // Watch both viewport width, change the
    // size when moving through breakpoints
    watch([greaterThanLg, sm], () => {
      let size = 3;

      if (!greaterThanLg.value) size = 2;
      if (sm.value) size = 1;

      arts2D.value = splitIntoNArrays<ArtResponse>(arts, size);
    });

    return { breakpoints, arts2D, favs };
  },
});
</script>
