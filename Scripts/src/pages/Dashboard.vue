<template>
  <div class="font-garamond flex justify-end">
    <button
      class="
        bg-accent
        rounded-full
        text-light
        py-2
        px-4
        font-medium
        hover:bg-accent-hover
        group-hover:opacity-100
      "
      @click="
        (e) => {
          e.preventDefault();
          setDialogOpen(true);
        }
      "
    >
      Add New Art
    </button>
  </div>

  <Dialog
    :open="dialogOpen"
    @close="setDialogOpen(false)"
    class="font-garamond"
  >
    <DialogOverlay class="bg-black opacity-50 inset-0 fixed" />

    <div
      class="
        py-8
        bg-white
        flex flex-col
        rounded-2xl
        transform
        top-[50%]
        left-[50%]
        shadow-2xl
        w-[95%]
        translate-x-[-50%] translate-y-[-50%]
        fixed
        items-center
        sm:w-[484px]
      "
    >
      <div class="flex items-center">
        <h2 class="text-2xl font-bold">Add a new art</h2>
        <img
          src="https://api.iconify.design/emojione:framed-picture.svg"
          class="w-6 h-6 ml-2"
        />
      </div>

      <button
        class="absolute top-4 right-4 p-3 hover:bg-light-hover rounded-full"
        @click="
          (e) => {
            e.preventDefault();
            setDialogOpen(false);
          }
        "
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          xmlns:xlink="http://www.w3.org/1999/xlink"
          aria-hidden="true"
          role="img"
          width="1em"
          height="1em"
          preserveAspectRatio="xMidYMid meet"
          viewBox="0 0 32 32"
          class="w-6 h-6"
        >
          <path
            d="M24 9.4L22.6 8L16 14.6L9.4 8L8 9.4l6.6 6.6L8 22.6L9.4 24l6.6-6.6l6.6 6.6l1.4-1.4l-6.6-6.6L24 9.4z"
            fill="currentColor"
          />
        </svg>
      </button>

      <Form
        @submit="handleSubmit"
        :validation-schema="validationSchema"
        v-slot="{ errors, isSubmitting }"
        class="flex flex-col mt-4 w-full px-24"
      >
        <div class="mb-4">
          <img
            id="uploaded_image"
            class="rounded-lg mx-auto object-cover h-[50px] w-[50px] hidden"
          />
          <label
            id="upload_image_button"
            class="file-upload-input cursor-pointer w-[108px] mx-auto"
          >
            <svg
              class="h-6 w-6"
              xmlns="http://www.w3.org/2000/svg"
              xmlns:xlink="http://www.w3.org/1999/xlink"
              aria-hidden="true"
              focusable="false"
              width="1em"
              height="1em"
              style="
                -ms-transform: rotate(360deg);
                -webkit-transform: rotate(360deg);
                transform: rotate(360deg);
              "
              preserveAspectRatio="xMidYMid meet"
              viewBox="0 0 20 20"
            >
              <path
                d="M8 12h4V6h3l-5-5l-5 5h3v6zm11.338 1.532c-.21-.224-1.611-1.723-2.011-2.114A1.503 1.503 0 0 0 16.285 11h-1.757l3.064 2.994h-3.544a.274.274 0 0 0-.24.133L12.992 16H7.008l-.816-1.873a.276.276 0 0 0-.24-.133H2.408L5.471 11H3.715c-.397 0-.776.159-1.042.418c-.4.392-1.801 1.891-2.011 2.114c-.489.521-.758.936-.63 1.449l.561 3.074c.128.514.691.936 1.252.936h16.312c.561 0 1.124-.422 1.252-.936l.561-3.074c.126-.513-.142-.928-.632-1.449z"
                fill="currentColor"
              />
            </svg>
            <span class="font-medium text-sm">Upload</span>
            <input type="file" class="hidden" @change="uploadImageHandler" />
          </label>
          <input id="upload_image_url" name="url" class="hidden" />
        </div>

        <Field
          name="description"
          placeholder="Description"
          :class="[
            errors.description ? 'border-red' : 'border-light-hover',
            inputClass,
          ]"
        />
        <ErrorMessage name="description" class="mt-2 text-xs text-red" />

        <Field
          name="price"
          placeholder="Price"
          type="number"
          min="0"
          max="9999999"
          step="1"
          :class="[
            errors.price ? 'border-red' : 'border-light-hover',
            inputClass,
          ]"
        />
        <ErrorMessage name="price" class="mt-2 text-xs text-red" />

        <Field
          name="stock"
          placeholder="Stock"
          type="number"
          min="0"
          max="9999999"
          step="1"
          :class="[
            errors.stock ? 'border-red' : 'border-light-hover',
            inputClass,
          ]"
        />
        <ErrorMessage name="stock" class="mt-2 text-xs text-red" />

        <Field
          name="style"
          placeholder="Style"
          :class="[
            errors.style ? 'border-red' : 'border-light-hover',
            inputClass,
          ]"
        />
        <ErrorMessage name="style" class="mt-2 text-xs text-red" />

        <button
          id="submit_art_button"
          class="
            bg-accent
            font-medium
            rounded-3xl
            mt-6
            text-white
            py-2
            px-4
            hover:bg-accent-hover
            focus:outline-none
          "
          type="submit"
        >
          <Spinner v-if="isSubmitting" :size="'medium'" :class="'mx-auto'" />
          <span v-else>Submit</span>
        </button>
      </Form>
    </div>
  </Dialog>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { Dialog, DialogOverlay } from "@headlessui/vue";
import { Form, Field, ErrorMessage, configure } from "vee-validate";
import { sleep, triggerBackendControl } from "../utils/helper";
import * as Yup from "yup";
import Spinner from "../components/Spinner.vue";
import { post } from "../utils/api";
import { useSession } from "../stores/useSession";

// Configure vee-validate params (form validation)
configure({
  validateOnBlur: false, // will not validate when user clicks outside of input
  validateOnChange: true,
});

const refreshData = () =>
  triggerBackendControl(undefined, "MainContent_RefreshData");

// function to upload image to imgBB
const uploadToImgBBAndRender = (file: File) => {
  // Create a FormData
  const form = new FormData();
  form.append("image", file);

  // Construct a request using XHR
  const req = new XMLHttpRequest();

  // Handle when request is done
  req.addEventListener("loadend", () => {
    const res = JSON.parse(req.response);

    if (!res.success) {
      console.error(res);
      alert("Error uploading the image, please try again.");
    } else {
      // Grab the img element
      const img = <HTMLImageElement>document.getElementById("uploaded_image");
      const imgUrl = res.data.url;
      const imgTitle = res.data.title;

      // Remove the file upload input
      document.getElementById("upload_image_button")!.remove();

      // Render the uploaded image
      img.src = imgUrl;
      img.alt = imgTitle;
      img.classList.remove("hidden");

      // Write the url to textbox
      (<HTMLInputElement>document.getElementById("upload_image_url"))!.value =
        imgUrl;
    }
  });

  // Handle when request error out
  req.addEventListener("error", () => {
    alert("Error uploading the image, please try again.");
  });

  // Send the request asynchronously
  req.open(
    "POST",
    "https://api.imgbb.com/1/upload?key=5c9fb8fe1c97301797246733495f9770",
    true
  );
  req.send(form);
};

export default defineComponent({
  components: {
    Dialog,
    DialogOverlay,
    Form,
    Field,
    ErrorMessage,
    Spinner,
  },
  setup() {
    const dialogOpen = ref(false);
    const session = useSession();

    const setDialogOpen = (val: boolean) => (dialogOpen.value = val);

    const handleSubmit = async ({
      description,
      price,
      stock,
      style,
    }: {
      description: string;
      price: number;
      stock: number;
      style: string;
    }) => {
      const url = (<HTMLInputElement>(
        document.getElementById("upload_image_url")
      )).value;

      // Send a POST request to backend api to create an art
      const { data, error } = await post<{ success: boolean }>(
        "Api/Art.aspx/Create",
        {
          url,
          description,
          price,
          stock,
          style,
          authorId: session.value.user?.authorId,
        }
      );

      // If failed to create art
      if (error || !data) {
        console.error(error);
        alert(error?.message);
        return;
      }

      // Close the modal
      setDialogOpen(false);
      // Refreshes the data from db
      refreshData();
    };

    return { dialogOpen, setDialogOpen, handleSubmit };
  },
  data() {
    // This is the validaion schema for add form
    const validationSchema = Yup.object({
      description: Yup.string()
        .required("Description cannot be empty")
        .min(10, "Description must exceeds 10 characters"),
      price: Yup.number()
        .required("Price cannot be empty")
        .min(1, "Price cannot be 0 or negative"),
      stock: Yup.number()
        .required("Stock cannot be empty")
        .min(0, "Stock cannot be negative"),
      style: Yup.string()
        .required("Style cannot be empty")
        .min(4, "Style must exceeds 4 characters"),
    });

    const inputClass = `
      w-full
      px-4
      py-2
      mt-2
      border-2
      rounded-2xl
      focus:outline-none
      focus:border-light-hover
      focus:ring focus:ring-accent
    `;
    return { validationSchema, inputClass };
  },
  methods: {
    uploadImageHandler: (event: Event) => {
      const file = (<HTMLInputElement>event.target).files![0];
      uploadToImgBBAndRender(file);
    },
  },
});
</script>
