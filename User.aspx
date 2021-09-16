<%@ Page Title="User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="User.aspx.cs"
    Inherits="ArtGalleryWebsite.User" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Button ID="btnSaveArtDetailPage" runat="server" OnClick="btnSaveArtDetailPage_click" CssClass="hidden"/>
        <asp:Button ID="btnUserDetailPage" runat="server" OnClick="btnUserDetailPage_click"
        PostBackUrl="~/UserDetail.aspx"
        CssClass="hidden"/>
        <asp:Button ID="btnFavDetailPage" runat="server" OnClick="btnFavDetailPage_click" CssClass="hidden"/>
        <asp:Button ID="btnShowCreateFav" runat="server" OnClick="btnShowCreateFav_click" CssClass="hidden"/>
        <asp:Button ID="btnShowPH" runat="server" OnClick="btnShowPH_click" CssClass="hidden"/>

        <div id="app">
        </div>

        <script src="Scripts/dist/UserPage.dist.js"></script>

        <!-- Create New Fav -->
        <asp:Panel runat="server" ID="CreateFav" CssClass="absolute z-10 font-garamond">
            <div
                class="bg-white
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
                        p-10
                ">
                <p class="text-center text-3xl font-bold pb-5">Create New Fav</p>
                <div class="grid grid-col-2 gap-2 w-full">
                    <label for="txtFavName" class="p-2"> Name </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqFavName"
                        ErrorMessage="* Required"
                        ControlToValidate="txtFavName"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGFav"
                        >
                        </asp:RequiredFieldValidator>
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full block w-full col-span-2"
                        ID="txtFavName"
                        placeholder="Light Academia"
                    />
                    <asp:RegularExpressionValidator runat="server" ID="RegexFavName"
                        ControlToValidate="txtFavName"
                        ErrorMessage="Please enter less than 50 characters."
                        ValidationExpression="^.{1,50}$"
                        CssClass="text-red text-sm col-span-2"
                        ValidationGroup="VGFav"
                        >
                        </asp:RegularExpressionValidator>
                    </div> <!-- Input Div -->

                <div class="flex justify-center gap-10">
                    <!-- Cancel Button -->
                    <asp:Button
                        runat="server"
                        ID="btnCancelFav"
                        OnClick="btnCancelFav_click"
                        CssClass="
                            bg-light
                            text-lg
                            font-bold
                            font-garamond
                            justify-center
                            px-5
                            py-2
                            rounded-full
                            hover:bg-light-hover
                            "
                        Text="Cancel Create"
                    />

                    <!-- Create Button -->
                    <asp:Button
                        runat="server"
                        ID="btnCreateFav"
                        OnClick="btnCreateFav_click"
                        CssClass="
                            bg-accent
                            text-white
                            text-lg
                            font-bold
                            font-garamond
                            justify-center
                            px-5
                            py-2
                            rounded-full
                            hover:bg-accent-hover
                            "
                        Text="Create Fav"
                    />
                </div> <!-- Button Div -->
            </div> <!-- Modal Div -->
        </asp:Panel>

        <!-- Purchase History -->
        <asp:Panel runat="server" ID="PurchaseHistory" CssClass="absolute z-10 font-garamond]">
            <div
                class="bg-white
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
                        p-10
                        h-[30em]
                        overflow-auto
                        font-garamond
                ">
                <p class="text-center text-3xl font-bold pb-5">Purchase History</p>
                <asp:Panel runat="server" ID="PHItems" CssClass="">
                    <% foreach (var ph in PHis) { %>
                        <% foreach (var art in ph.Arts) { %>
                        <div class="py-5 flex w-full mt-2">
                        <!-- Image -->
                        <div class="w-[8.5em] mr-5">
                            <img
                            class="object-cover w-[8.5em] h-[8.5em] rounded-3xl"
                            src="<%= art.Url %>"
                            alt="<%= art.Description %>"
                            />
                        </div>
                        <!-- Description -->
                        <div class="flex flex-col w-[75%]">
                            <p class="font-bold text-xl"><%= art.Description %></p>
                            <p>By <%= art.Author.Name %></p>
                            <%--<p class="text-gray-500">Purchased on 12/09/2021 00:00:00</p>--%>
                            <p class="text-gray-500">Purchased on <%= ph.UpdatedAt.ToLocalTime() %></p>
                            <p class="mt-auto ml-auto">RM <%= art.Price.ToString("0.00") %></p>
                        </div>
                        </div>
                        <% } %>
                    <% } %>
                </asp:Panel>

                <div class="flex justify-center gap-10">
                    <!-- Cancel Button -->
                    <asp:Button
                        runat="server"
                        ID="btnClosePH"
                        OnClick="btnClosePH_click"
                        CssClass="
                            bg-light
                            text-lg
                            font-bold
                            font-garamond
                            justify-center
                            px-5
                            py-2
                            rounded-full
                            hover:bg-light-hover
                            cursor-pointer
                            "
                        Text="Close"
                    />
                </div> <!-- Button Div -->
            </div> <!-- Modal Div -->
        </asp:Panel>

        <%-- User Control --%>
        <div class="text-sm font-garamond  bg-accent text-white text-center p-10 bottom-0">
            <p class="text-lg">Credits to:</p>
            <uc:References ID="UserRef1" runat="server" CreditRef="https://icons8.com/icon/5824/pencil" Alt="Edit Icon" Author="Icons8" />
            <br/>
            <uc:References ID="UserRef2" runat="server" CreditRef="https://icons8.com/icon/83213/share" Alt="Share Icon" Author = "Icon8"/>
            <br/>
            <uc:References ID="UserRef3" runat="server" CreditRef="https://icons8.com/icon/82535/settings" Alt="Settings Icon" Author = "Icon8"/>
            <br/>
            <uc:References ID="UserRef4" runat="server" CreditRef="https://icons8.com/icon/85096/add" Alt="Add Icon" Author = "Icon8"/>
        </div>
    </asp:Content>