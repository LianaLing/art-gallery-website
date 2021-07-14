<template>
  <div class="flex justify-around w-full py-8 mx-auto max-w-7xl">
    <div
      class="flex flex-col mx-4"
      v-for="arts in arts2D"
      :key="arts.toString()"
    >
      <ArtCard v-for="art in arts" :key="art.id" :art="art" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Art } from "../types/model";
import { getStateFromBackend, sliceIntoChunks } from "../utils/helper";
import ArtCard from "../components/ArtCard.vue";

// Getting the data from code-behind
const arts = getStateFromBackend<Art[]>("state");
// Slice the data from 1D array to 2D array
// eg. [1, 2, 3, 4, 5, 6] -> [[1, 2, 3], [4, 5, 6]]
const arts2D = sliceIntoChunks<Art>(arts, 3);

export default defineComponent({
  components: { ArtCard },
  data() {
    return { arts2D };
  },
});
</script>
