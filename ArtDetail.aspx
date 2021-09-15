<%@ Page Title="Art Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArtDetail.aspx.cs"
    Inherits="ArtGalleryWebsite.ArtDetail" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <%-- Hidden buttons to receive and process simulated clicks from frontend --%>
        <asp:Button ID="btnSaveStar" runat="server" OnClick="btnSaveStar_click" CssClass="hidden"/>
        <asp:Button ID="btnRemoveStar" runat="server" OnClick="btnRemoveStar_click" CssClass="hidden"/>
        <asp:Button ID="btnLikeHandler" runat="server" OnClick="btnLikeHandler_click" CssClass="hidden"/>
        <asp:Button ID="btnAddToCart" runat="server" OnClick="btnAddToCart_click" CssClass="hidden"/>
        <asp:Button ID="btnCartPage" runat="server" OnClick="btnCartPage_click" CssClass="hidden" PostBackUrl="~/Cart.aspx"/>

        <%--<asp:Label ID="ErrorLabel" runat="server" />--%>
        <uc:ErrorLabel ID="lblErr" runat="server" />

        <%-- Frontend code --%>
        <div id="app">
        </div>

        <%-- User Control --%>
        <div class="text-sm font-garamond bg-accent text-white text-center p-10 bottom-0 mt-40">
            <p class="text-lg">Credits to:</p>
            <uc:References ID="ArtDetailRef1" runat="server" CreditRef="https://icons8.com/icon/99996/left-arrow" Alt="Left Arrow Icon" Author="Icons8" />
            <br/>
            <uc:References ID="ArtDetailRef2" runat="server" CreditRef="https://icons8.com/icon/hmAU0m6F6i0C/star" Alt="Animated Star Icon" Author = "Icon8"/>
            <br/>
            <uc:References ID="ArtDetailRef3" runat="server" CreditRef="https://icons8.com/icon/FupVmEePjs1T/share-rounded" Alt="Share Rounded Icon" Author = "Icon8"/>
            <br/>
            <uc:References ID="ArtDetailRef4" runat="server" CreditRef="https://icons8.com/icon/33481/facebook-like" Alt="Facebook Like Icon" Author = "Icon8"/>
            <br/>
            <uc:References ID="ArtDetailRef5" runat="server" CreditRef="https://icons8.com/icon/33479/facebook-like" Alt="Facebook Like Icon Filled" Author = "Icon8"/>
        </div>

        <script src="Scripts/dist/ArtDetailPage.dist.js"></script>

    </asp:Content>