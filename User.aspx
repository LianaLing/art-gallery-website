<%@ Page Title="User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs"
    Inherits="ArtGalleryWebsite.User" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Button ID="btnSaveArtDetailPage" runat="server" OnClick="btnSaveArtDetailPage_click" CssClass="hidden"/>
        <div id="app">
        </div>

        <script src="Scripts/dist/UserPage.dist.js"></script>
        <%-- User Control --%>
        <div class="text-sm font-garamond w-screen bg-accent text-white text-center p-10 bottom-0 -mx-6">
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