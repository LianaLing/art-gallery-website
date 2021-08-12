<%@ Page Title="ArtDetail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArtDetail.aspx.cs"
    Inherits="ArtGalleryWebsite.ArtDetail" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Button ID="btnSaveStar" runat="server" OnClick="btnSaveStar_click" CssClass="hidden"/>
        <div id="app">
        </div>

        <script src="Scripts/dist/ArtDetailPage.dist.js"></script>

    </asp:Content>