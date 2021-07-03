<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ArtGalleryWebsite.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="app">
    </div>

    <script src="Scripts/helper.js"></script>
    <script type="module">
        import { ArtCard } from "./Components/ArtCard.js";

        // Getting the data from code-behind
        const state = JSON.parse(document.getElementById("state").value);
        const arts2D = sliceIntoChunks(state, 3);

        // Define the app component
        const app = Vue.createApp({
            template: `
                <div class="flex justify-around w-full py-8 mx-auto max-w-7xl">
                    <div class="flex flex-col mx-4" v-for="arts in arts2D">
                        <art-card v-for="(art, index) in arts" :title="art.title" :imageSrc="art.imgSrc" :class="index !== 0 ? 'mt-8' : ''" />
                    </div>
                </div>
            `,
            data() {
                return { arts2D };
            },
        });
        app.component("art-card", ArtCard);
        app.mount('#app');
    </script>
</asp:Content>
