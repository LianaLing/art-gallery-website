<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="ArtGalleryWebsite.Home" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <%-- Hidden buttons to receive and process simulated clicks from frontend --%>
        <asp:button ID="btnArtDetailPage" runat="server" OnClick="btnArtDetailPage_click" CssClass="hidden" />
        <asp:Button ID="btnSaveArt" runat="server" OnClick="btnSaveArt_click" CssClass="hidden" />
        <asp:Button ID="btnRemoveArt" runat="server" OnClick="btnRemoveArt_click" CssClass="hidden" />

        <%-- Frontend code --%>
        <div id="app">
        </div>

        <script src="Scripts/dist/HomePage.dist.js"></script>
    </asp:Content>