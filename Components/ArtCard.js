// Define a new component
export const ArtCard = {
  props: ["imageSrc", "title"],
  template: `
        <div class="relative group filter drop-shadow-xl">
            <img :alt="title" class="w-[300px] h-full object-cover rounded-3xl backdrop-filter backdrop-brightness-50" :src="imageSrc" />
            <div class="absolute top-0 w-full h-full transition duration-200 ease-in-out rounded-3xl backdrop-filter backdrop-brightness-100 group-hover:backdrop-brightness-75 group-hover:cursor-[zoom-in]"></div>
            <button class="absolute px-4 py-2 font-bold transition duration-200 ease-in-out rounded-full opacity-0 bg-red hover:bg-red-hover text-light top-4 right-4 group-hover:opacity-100">Save</button>
            <p class="absolute bottom-4 left-4 transition duration-200 ease-in-out opacity-0 group-hover:opacity-100 text-white">{{ title }}</p>
        </div>
    `,
  data: () => {
    return {};
  },
};
