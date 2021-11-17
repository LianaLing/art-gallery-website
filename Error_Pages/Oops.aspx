<%@ Page Title="Oops" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Oops.aspx.cs"
    Inherits="ArtGalleryWebsite.Error_Pages.Oops" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div id="app">
        </div>

        <uc:ErrorTemplate runat="server" ID="OopsErr" HttpCode="404" ErrBody="Oops! The page you have requested does not exist." />
        <script src="Scripts/dist/OopsPage.dist.js"></script>
    </asp:Content>