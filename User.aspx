<%@ Page Title="User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs"
    Inherits="ArtGalleryWebsite.User" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Button ID="btnSaveArtDetailPage" runat="server" OnClick="btnSaveArtDetailPage_click" CssClass="hidden"/>
        <div id="app">
        </div>

        <script src="Scripts/dist/UserPage.dist.js"></script>

    </asp:Content>