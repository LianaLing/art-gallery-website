<%@ Page Title="InternalErr" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Internal.aspx.cs"
    Inherits="ArtGalleryWebsite.Error_Pages.Internal" %>

  <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <uc:ErrorTemplate runat="server" ID="InternalErr" HttpCode="500" ErrBody="Something's wrong with our server... We'll be back soon!" />
  </asp:Content>