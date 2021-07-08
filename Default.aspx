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

    <div class="flex-auto mt-20">
        <h1 class="text-center text-[#E60023] text-8xl font-bold font-garamond">BEAUTY</h1>
        <br />
        <h1 class="text-center text-5xl font-bold font-garamond">is in the eye of the beholder.</h1>
    </div>

    <!-- Image presentation -->
    <div id="homeGallery" class="grid grid-cols-7 gap-4 mt-35">
        <div>
            <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
        </div>
        <div class="mt-20">
            <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
        </div>
        <div class="mt-40">
            <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
        </div>
        <div class="mt-60">
            <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
        </div>
        <div class="mt-40">
            <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
        </div>
        <div class="mt-20">
            <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
        </div>
        <div>
            <img class="rounded-xl" src="https://upload.wikimedia.org/wikipedia/commons/a/a4/Madame_X_%28Madame_Pierre_Gautreau%29%2C_John_Singer_Sargent%2C_1884_%28unfree_frame_crop%29.jpg" alt="Portrait of Madame X - Wikipedia"/>
        </div>
    </div>

    <!-- Auto crop image-->
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

</asp:Content>
