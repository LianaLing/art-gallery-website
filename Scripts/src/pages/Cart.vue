<template>
  <div class="flex font-garamond lg:mx-[350px] lg:my-[30px]">
    <!-- Cart -->
    <div class="lg:w-3/5">
      <div class="px-5">
        <div class="py-5">
          <p class="font-bold text-4xl">Your Cart</p>
        </div>
        <!-- Item Header -->
        <div class="px-5 py-2 bg-accent text-white">
          <p class="font-bold text-2xl">Item(s)</p>
        </div>
        <!-- Item Detail -->
        <div v-for="art in arts" :key="art.id" class="py-5">
          <!-- Image -->
          <img
            class="object-cover inline-block w-[8.5em] h-[8.5em]"
            :src="art.url"
            :alt="art.description"
          />
          <!-- Description -->
          <div class="inline-block px-5 w-auto">
            <p class="font-bold text-xl">{{ art.description }}</p>
            <p>By {{ art.author.name }}</p>
            <p class="float-right">RM{{ art.price }}</p>
          </div>
        </div>
      </div>
    </div>
    <!-- Receipt -->
    <div class="px-5 w-auto lg:w-2/5 sticky">
      <p class="py-5 text-2xl font-bold">Order Summary</p>
      <div class="text-xl">
        <div class="flex">
          <p class="w-4/6">Subtotal</p>
          <!-- Testing -->
          <p class="w-1/6 text-gray-500">RM</p>
          <p class="w-1/6 text-right">{{ arts[0].price }}</p>
        </div>
        <div class="flex">
          <p class="w-4/6">Shipping</p>
          <p class="w-1/6 text-gray-500">RM</p>
          <!-- Testing -->
          <p class="w-1/6 text-right">10.00</p>
        </div>
        <div class="my-5 border-t-2 border-b-2 border-gray-200 py-5 flex">
          <p class="w-4/6 font-bold">Order Total</p>
          <p class="w-1/6 text-gray-500">RM</p>
          <!-- Testing -->
          <p class="w-1/6">{{ arts[0].price }}</p>
        </div>
      </div>
      <!-- Payment -->
      <div class="text-xl">
        <p class="font-bold">Payment</p>
        <div class="py-5">
          <input
            class="w-1/12 checked:border-accent"
            type="radio"
            id="paymenttype"
            name="paymenttype"
            value="Visa/Master/Amex"
          />
          <label for="Visa/Master/Amex" class="w-11/12">
            Visa/Master/Amex
          </label>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { getStateFromBackend } from "../utils/helper";
import * as API from "../types/api";

const arts = getStateFromBackend<API.ArtResponse[]>("arts");

export default defineComponent({
  data() {
    return {
      arts,
    };
  },
});
</script>