<%@ Page Title="Landing Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ArtGalleryWebsite._Default" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <%-- Frontend code --%>
        <div id="app"></div>

        <script src="Scripts/dist/LandingPage.dist.js"></script>
    </asp:Content>