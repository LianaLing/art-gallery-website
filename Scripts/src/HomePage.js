import { createApp } from 'vue';
import ArtCard from "./components/ArtCard";
import { sliceIntoChunks } from "./utils/Helper";

// Getting the data from code-behind
const state = JSON.parse(document.getElementById("state").value);
const arts2D = sliceIntoChunks(state, 3);

// Define the app component
export default createApp({
    components: { ArtCard },
    template: `
        <div class="flex justify-around w-full py-8 mx-auto max-w-7xl">
            <div class="flex flex-col mx-4" v-for="arts in arts2D">
                <ArtCard v-for="(art, index) in arts" :id="art.id" :title="art.title" :imageSrc="art.imgSrc" :price="art.price" :author="art.author" />
            </div>
        </div>
    `,
    data() {
        return { arts2D };
    },
}).mount('#app');
