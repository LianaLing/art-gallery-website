<template>
  <div class="flex font-garamond lg:mx-[350px] lg:my-[30px]">
    <!-- Cart -->
    <div class="lg:w-3/5">
      <div class="px-5">
        <div class="py-5">
          <p class="font-bold text-4xl">Your Cart</p>
        </div>
        <!-- Item Header -->
        <button
          class="
            px-5
            py-2
            bg-accent
            text-white
            rounded-full
            flex
            w-full
            hover:bg-accent-hover
          "
          @click="showItemsHandler($event, !showItems)"
        >
          <p class="font-bold text-2xl">Items</p>
          <p
            class="
              w-0
              h-0
              border-l-[5px]
              border-r-[5px]
              border-t-[10px]
              border-r-transparent
              border-l-transparent
              border-t-white
              ml-auto
              my-auto
            "
          />
        </button>
        <!-- Item Detail -->
        <div
          v-for="art in arts"
          :key="art.id"
          class="py-5 flex w-full mt-2"
          :class="showItemsClass"
        >
          <!-- Image -->
          <div class="w-[8.5em] mr-5">
            <img
              class="object-cover w-[8.5em] h-[8.5em] rounded-3xl"
              :src="art.url"
              :alt="art.description"
            />
          </div>
          <!-- Description -->
          <div class="flex flex-col w-[75%]">
            <p class="font-bold text-xl">{{ art.description }}</p>
            <p>By {{ art.author.name }}</p>
            <p class="mt-auto ml-auto">RM {{ art.price.toFixed(2) }}</p>
          </div>
        </div>
        <!-- Shipping and Billing -->
        <button
          class="
            px-5
            py-2
            bg-accent
            text-white
            rounded-full
            flex
            w-full
            my-5
            hover:bg-accent-hover
          "
          @click="showShipBillHandler($event, !showShipBill)"
        >
          <p class="font-bold text-2xl">Shipping and Billing</p>
          <p
            class="
              w-0
              h-0
              border-l-[5px]
              border-r-[5px]
              border-t-[10px]
              border-r-transparent
              border-l-transparent
              border-t-white
              ml-auto
              my-auto
            "
          />
        </button>
        <div class="px-5 text-xl" :class="showShipBillClass">
          <!-- Shipping Address -->
          <div>
            <p class="font-bold">Shipping Address</p>
            <label for="fname" class="p-2 block"> Full Name</label>
            <input
              class="form-input rounded-full block w-full"
              type="text"
              id="fname"
              name="firstname"
              placeholder="John M. Doe"
            />
            <label for="email" class="p-2 block"> Email</label>
            <input
              class="form-input rounded-full block w-full"
              type="text"
              id="email"
              name="email"
              placeholder="john@example.com"
            />
            <label for="adr" class="p-2 block"> Address</label>
            <input
              class="form-input rounded-full block w-full"
              type="text"
              id="shipadr"
              name="shipaddress"
              placeholder="542 W. 15th Street"
            />
            <label for="phone" class="p-2 block">Phone</label>
            <input
              class="form-input rounded-full block w-full"
              type="text"
              id="phone"
              name="phone"
              placeholder="012-3456 789"
            />
          </div>
        </div>
      </div>
    </div>
    <!-- Receipt -->
    <div class="px-5 w-auto lg:w-2/5 self-start sticky top-0">
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
        <div class="py-5 border-b-2 border-gray-100">
          <input
            class="text-accent mr-2 form-radio solid"
            type="radio"
            id="paymenttype"
            name="paymenttype"
            value="Visa/Master/Amex"
          />
          <label for="Visa/Master/Amex" class=""> Visa/Master/Amex </label>
        </div>
        <!-- Enter Payment Info -->
        <div class="py-5 flex justify-center">
          <button
            class="
              bg-accent
              text-white
              px-5
              py-2
              rounded-full
              hover:bg-accent-hover
            "
          >
            Pay With This Method
          </button>
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

const phone = document.getElementById("phone");

export default defineComponent({
  methods: {
    showItemsHandler(e: Event, showItems: boolean) {
      e.preventDefault();
      if (showItems) {
        this.showItemsClass = "display";
      } else {
        this.showItemsClass = "hidden";
      }
      this.showItems = showItems;
    },
    showShipBillHandler(e: Event, showShipBill: boolean) {
      e.preventDefault();
      if (showShipBill) {
        this.showShipBillClass = "display";
      } else {
        this.showShipBillClass = "hidden";
      }
      this.showShipBill = showShipBill;
    },
  },
  data() {
    return {
      arts,
      showItems: true,
      showItemsClass: "display",
      showShipBill: true,
      showShipBillClass: "display",
    };
  },
});
</script>