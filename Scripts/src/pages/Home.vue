<template>
  <div class="flex justify-around w-full py-8 mx-auto max-w-7xl">
    <div class="flex flex-col mx-4" v-for="arts in arts2D" :key="arts">
      <ArtCard v-for="art in arts" :key="art.id" :art="art" />
    </div>
  </div>
</template>

<script lang="ts">
import ArtCard from "../components/ArtCard.vue";
import { sliceIntoChunks } from "../utils/Helper";

// Getting the data from code-behind
const state = JSON.parse(
  (<HTMLInputElement>document.getElementById("state")).value
);
// Slice the data from 1D array to 2D array
// eg. [1, 2, 3, 4, 5, 6] -> [[1, 2, 3], [4, 5, 6]]
const arts2D = sliceIntoChunks(state, 3);

export default {
  components: { ArtCard },
  data() {
    return { arts2D };
  },
};
</script>
