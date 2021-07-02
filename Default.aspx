<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ArtGalleryWebsite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="">
        <asp:Label ID="lblCounter" runat="server" />
        <asp:Button ID="btnIncrement" Text="Click me" runat="server" OnClick="btn_Click" />
    </div>

</asp:Content>
