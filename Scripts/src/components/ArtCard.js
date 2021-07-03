import { Popover, PopoverButton, PopoverPanel } from "@headlessui/vue";

// Define the transition
const transition = `transition ease-in-out duration-200`;

// Define a new component
export const ArtCard = {
    components: { Popover, PopoverButton, PopoverPanel },
    props: ["id", "imageSrc", "title", "price", "author"],
    template: `
          <div class="relative group filter drop-shadow-none w-[300px]">
              <img :alt="title" class="w-[300px] h-full object-cover rounded-3xl backdrop-filter backdrop-brightness-50" :src="imageSrc" />
              <div class="absolute top-0 w-full h-full ${transition} rounded-3xl backdrop-filter backdrop-brightness-100 group-hover:backdrop-brightness-75 group-hover:cursor-[zoom-in]"></div>
              <button class="absolute px-4 py-2 font-bold ${transition} rounded-full opacity-0 bg-red hover:bg-red-hover text-light top-4 right-4 group-hover:opacity-100">Save</button>
              <p class="absolute bottom-4 left-4 ${transition} opacity-0 group-hover:opacity-100 text-dark font-semibold bg-light rounded-full px-4 py-2">
                RM {{ price.toFixed(2) }}
              </p>
              <Popover class="absolute bottom-4 right-4 opacity-0 group-hover:opacity-100 ${transition}">
                 <PopoverButton class="rounded-full bg-light p-2 focus:outline-none hover:bg-light-hover ${transition}">
                    <svg class="w-6 h-6 text-dark" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.684 13.342C8.886 12.938 9 12.482 9 12c0-.482-.114-.938-.316-1.342m0 2.684a3 3 0 110-2.684m0 2.684l6.632 3.316m-6.632-6l6.632-3.316m0 0a3 3 0 105.367-2.684 3 3 0 00-5.367 2.684zm0 9.316a3 3 0 105.368 2.684 3 3 0 00-5.368-2.684z"></path></svg>
                 </PopoverButton>

                 <PopoverPanel class="absolute z-10 bottom-12 right-0 w-[300px] shadow-xl">
                   <div class="bg-white rounded-3xl p-4">
                     <h3 class="font-bold w-full text-center text-lg">Share this artwork</h3>
                     <div class="mt-4 flex justify-around items-start">
                       <button onclick="(() => alert('Share {{ id }} WhatsApp'))()">
                         <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 48 48" width="48" height="48" version="1.1"><g id="surface1"><path d="M 48 24 C 48 37.253906 37.253906 48 24 48 C 10.746094 48 0 37.253906 0 24 C 0 10.746094 10.746094 0 24 0 C 37.253906 0 48 10.746094 48 24 Z M 48 24 " style="fill: rgb(37, 211, 102); stroke: transparent;"></path><path d="M 33.851562 13.910156 C 28.871094 9.039062 21.070312 8.566406 15.539062 12.800781 C 10.011719 17.039062 8.4375 24.695312 11.851562 30.769531 L 9.871094 38 L 17.28125 36 C 19.332031 37.148438 21.644531 37.757812 24 37.769531 C 31.730469 37.769531 38 31.503906 38 23.769531 C 38.019531 20.058594 36.542969 16.496094 33.910156 13.878906 M 24 35.410156 C 21.921875 35.410156 19.878906 34.851562 18.089844 33.789062 L 17.671875 33.539062 L 13.269531 34.699219 L 14.441406 30.410156 L 14.128906 30 C 11.304688 25.523438 11.863281 19.707031 15.484375 15.847656 C 19.109375 11.992188 24.886719 11.074219 29.527344 13.621094 C 34.167969 16.164062 36.5 21.523438 35.199219 26.652344 C 33.898438 31.785156 29.292969 35.386719 24 35.410156 M 30.371094 26.710938 C 30.019531 26.539062 28.300781 25.710938 27.980469 25.578125 C 27.660156 25.449219 27.429688 25.398438 27.199219 25.75 C 26.96875 26.101562 26.300781 26.890625 26.089844 27.121094 C 25.878906 27.351562 25.679688 27.378906 25.328125 27.210938 C 24.304688 26.792969 23.359375 26.203125 22.53125 25.46875 C 21.765625 24.765625 21.109375 23.953125 20.578125 23.058594 C 20.378906 22.710938 20.578125 22.519531 20.738281 22.339844 C 20.898438 22.160156 21.089844 21.929688 21.261719 21.730469 C 21.40625 21.554688 21.523438 21.359375 21.609375 21.148438 C 21.71875 20.960938 21.71875 20.730469 21.609375 20.539062 C 21.519531 20.359375 20.820312 18.640625 20.53125 17.941406 C 20.238281 17.238281 19.960938 17.351562 19.75 17.339844 L 19 17.339844 C 18.640625 17.351562 18.304688 17.511719 18.070312 17.78125 C 17.269531 18.53125 16.820312 19.589844 16.839844 20.691406 C 16.957031 22.007812 17.453125 23.261719 18.269531 24.300781 C 19.773438 26.554688 21.84375 28.375 24.269531 29.578125 C 24.921875 29.859375 25.589844 30.105469 26.269531 30.308594 C 26.980469 30.527344 27.734375 30.578125 28.46875 30.449219 C 29.453125 30.25 30.308594 29.648438 30.828125 28.789062 C 31.058594 28.269531 31.128906 27.691406 31.03125 27.128906 C 30.941406 26.988281 30.710938 26.898438 30.359375 26.71875 " style="fill: rgb(255, 255, 255); stroke: transparent;"></path></g></svg>
                         <span class="text-xs mt-1">WhatsApp</span>
                       </button>
                       <button class="flex flex-col items-center" onclick="(() => alert('Share to Facebook'))()">
                         <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 48 48" width="48" height="48"><title>f_logo_RGB-Blue_1024</title><g id="surface1"><path d="M 48 24 C 48 10.746094 37.253906 0 24 0 C 10.746094 0 0 10.746094 0 24 C 0 35.980469 8.777344 45.90625 20.25 47.707031 L 20.25 30.9375 L 14.15625 30.9375 L 14.15625 24 L 20.25 24 L 20.25 18.710938 C 20.25 12.699219 23.832031 9.375 29.316406 9.375 C 31.941406 9.375 34.6875 9.84375 34.6875 9.84375 L 34.6875 15.75 L 31.660156 15.75 C 28.679688 15.75 27.75 17.601562 27.75 19.5 L 27.75 24 L 34.40625 24 L 33.34375 30.9375 L 27.75 30.9375 L 27.75 47.707031 C 39.222656 45.90625 48 35.980469 48 24 Z M 48 24 " style="fill: rgb(24, 119, 242); stroke: transparent;"></path><path d="M 33.34375 30.9375 L 34.40625 24 L 27.75 24 L 27.75 19.5 C 27.75 17.601562 28.679688 15.75 31.660156 15.75 L 34.6875 15.75 L 34.6875 9.84375 C 34.6875 9.84375 31.941406 9.375 29.316406 9.375 C 23.832031 9.375 20.25 12.699219 20.25 18.710938 L 20.25 24 L 14.15625 24 L 14.15625 30.9375 L 20.25 30.9375 L 20.25 47.707031 C 22.734375 48.097656 25.265625 48.097656 27.75 47.707031 L 27.75 30.9375 Z M 33.34375 30.9375 " style="fill: rgb(255, 255, 255); stroke: transparent;"></path></g></svg>
                         <span class="text-xs mt-1">Facebook</span>
                       </button>
                       <button class="flex flex-col items-center" onclick="(() => alert('Share to Email'))()">
                         <div class="bg-light-hover rounded-full p-3 w-[48px] h-[48px] flex justify-center items-center">
                           <svg fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path d="M2.003 5.884L10 9.882l7.997-3.998A2 2 0 0016 4H4a2 2 0 00-1.997 1.884z"></path><path d="M18 8.118l-8 4-8-4V14a2 2 0 002 2h12a2 2 0 002-2V8.118z"></path></svg>
                         </div>
                         <span class="text-xs mt-1">Email</span>
                       </button>
                       <button class="flex flex-col items-center" onclick="(() => alert('Copy Link'))()">
                         <div class="bg-light-hover rounded-full p-3 w-[48px] h-[48px] flex justify-center items-center">
                           <svg fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M12.586 4.586a2 2 0 112.828 2.828l-3 3a2 2 0 01-2.828 0 1 1 0 00-1.414 1.414 4 4 0 005.656 0l3-3a4 4 0 00-5.656-5.656l-1.5 1.5a1 1 0 101.414 1.414l1.5-1.5zm-5 5a2 2 0 012.828 0 1 1 0 101.414-1.414 4 4 0 00-5.656 0l-3 3a4 4 0 105.656 5.656l1.5-1.5a1 1 0 10-1.414-1.414l-1.5 1.5a2 2 0 11-2.828-2.828l3-3z" clip-rule="evenodd"></path></svg>
                         </div>
                         <span class="text-xs mt-1">Copy Link</span>
                       </button>
                     </div>
                   </div>
                 </PopoverPanel>
               </Popover>
          </div>
          <div class="w-[300px] mb-8">
            <p class="px-2 mt-1 font-bold line-clamp-2">{{ title }}</p>
            <div class="flex items-center px-2 mt-1">
              <img :src="author.avatarUrl" class="w-[24px] w-[24px] rounded-full" />
              <span class="ml-1 text-sm">{{ author.name }}</span>
            </div>
          </div>
    `,
    data() {
      return {};
    },
};
