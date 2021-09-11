<%@ Page Title="ArtDetail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArtDetail.aspx.cs"
    Inherits="ArtGalleryWebsite.ArtDetail" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <%-- Hidden buttons to receive and process simulated clicks from frontend --%>
        <asp:Button ID="btnSaveStar" runat="server" OnClick="btnSaveStar_click" CssClass="hidden"/>
        <asp:Button ID="btnRemoveStar" runat="server" OnClick="btnRemoveStar_click" CssClass="hidden"/>
        <asp:Button ID="btnCartPage" runat="server" OnClick="btnCartPage_click" CssClass="hidden" PostBackUrl="~/Cart.aspx"/>
        
        <%-- Frontend code --%>
        <div id="app">
        </div>

        <script src="Scripts/dist/ArtDetailPage.dist.js"></script>

    </asp:Content>