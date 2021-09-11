<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs"
    Inherits="ArtGalleryWebsite.Cart" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <%-- Hidden buttons to receive and process simulated clicks from frontend --%>

        <%-- Frontend code --%>
        <div id="app">
        </div>

        <script src="Scripts/dist/CartPage.dist.js"></script>
    </asp:Content>
