<%@ Page Title="Landing Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ArtGalleryWebsite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Click me button
    
    <div class="">
        <p class="text-5xl">HEYEY</p>
        <asp:Label ID="lblCounter" runat="server" />
        <asp:Button ID="btnIncrement" Text="Click me" runat="server" OnClick="btn_Click" />
        <br />
        <br />
    </div>
    -->

    <!-- Slogan -->
    <div class="flex-auto mt-20">
        <h1 class="text-center text-accent text-8xl font-bold font-garamond">BEAUTY</h1>
        <br />
        <h1 class="text-center text-5xl font-bold font-garamond">is in the eye of the beholder.</h1>
    </div>

    <div class="relative">
        <!-- Image presentation -->
        <div id="homeGallery1" class="grid grid-cols-1 sm:grid-cols-3 md:grid-cols-5 lg:grid-cols-7 gap-4 mt-35">
            <div class="mt-5">
                <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden sm:block sm:mt-20">
                <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden sm:block sm:mt-5 md:mt-40">
                <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden md:block md:mt-20 lg:mt-60">
                <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden md:block md:mt-5 lg:mt-40">
                <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden lg:block lg:mt-20">
                <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden lg:block lg:mt-5">
                <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
        </div>

        <div id="homeGallery2" class="absolute top-80 grid grid-cols-1 sm:grid-cols-3 md:grid-cols-5 lg:grid-cols-7 gap-4 mt-35">
            <div class="mt-5">
                <img class="rounded-xl rotate-180" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden sm:block sm:mt-20">
                <img class="rounded-xl rotate-180" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden sm:block sm:mt-5 md:mt-40">
                <img class="rounded-xl rotate-180" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden md:block md:mt-20 lg:mt-60">
                <img class="rounded-xl rotate-180" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden md:block md:mt-5 lg:mt-40">
                <img class="rounded-xl rotate-180" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden lg:block lg:mt-20">
                <img class="rounded-xl rotate-180" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
            <div class="hidden lg:block lg:mt-5">
                <img class="rounded-xl rotate-180" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
            </div>
        </div>

        <!-- Auto crop image (not sure if needed)-->
        <script type="text/javascript">
            (function() {

            var img = document.getElementById('homeGallery').firstChild;
            img.onload = function() {
                if(img.height > img.width) {
                    img.height = '100%';
                    img.width = 'auto';
                }
            };

            }());
        </script>

        <!-- Gradient-to-transparent div towards the edge with a floating down-arrow button -->
        <div class="absolute -bottom-1.5 bg-gradient-to-t from-white h-3/6 w-full">
        </div>
    </div>

</asp:Content>
