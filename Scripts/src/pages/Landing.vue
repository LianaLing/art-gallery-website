<template>
  <!-- Slogan -->
  <div class="flex-auto mt-20">
    <h1 class="text-center text-8xl font-bold font-garamond" :class="colour">
      BEAUTY
    </h1>
    <br />
    <h1 class="text-center text-5xl font-bold font-garamond">
      is in the eye of the beholder.
    </h1>
  </div>

  <div class="relative">
    <!-- Image presentation -->
    <div
      id="homeGallery1"
      class="
        grid grid-cols-1
        sm:grid-cols-3
        md:grid-cols-5
        lg:grid-cols-7
        gap-4
        mt-35
      "
    >
      <div class="mt-5">
        <img
          class="rounded-xl"
          src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg"
          alt="Portrait of Madame X - Wikipedia"
        />
      </div>
      <div class="hidden sm:block sm:mt-20">
        <img
          class="rounded-xl"
          src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg"
          alt="Portrait of Madame X - Wikipedia"
        />
      </div>
      <div class="hidden sm:block sm:mt-5 md:mt-40">
        <img
          class="rounded-xl"
          src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg"
          alt="Portrait of Madame X - Wikipedia"
        />
      </div>
      <div class="hidden md:block md:mt-20 lg:mt-60">
        <img
          class="rounded-xl"
          src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg"
          alt="Portrait of Madame X - Wikipedia"
        />
      </div>
      <div class="hidden md:block md:mt-5 lg:mt-40">
        <img
          class="rounded-xl"
          src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg"
          alt="Portrait of Madame X - Wikipedia"
        />
      </div>
      <div class="hidden lg:block lg:mt-20">
        <img
          class="rounded-xl"
          src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg"
          alt="Portrait of Madame X - Wikipedia"
        />
      </div>
      <div class="hidden lg:block lg:mt-5">
        <img
          class="rounded-xl"
          src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg"
          alt="Portrait of Madame X - Wikipedia"
        />
      </div>
    </div>

    <!-- Gradient-to-transparent div towards the edge with a floating down-arrow button -->
    <div
      class="absolute -bottom-1.5 bg-gradient-to-t from-white h-3/6 w-full"
    ></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  methods: {
    changeColour(action: boolean) {
      if (action) {
        this.colour = "text-accent";
      } else {
        this.colour = "text-black";
      }
    },
  },
  data() {
    return {
      colour: "text-accent",
    };
  },
});
</script>