<%@ Page Title="Oops" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Oops.aspx.cs"
    Inherits="ArtGalleryWebsite.Error_Pages.Oops" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <%-- Hidden buttons to receive and process simulated clicks from frontend --%>

        <%-- Frontend code --%>
        <div id="app">
        </div>

        <asp:Panel runat="server" ID="OopsBody" CssClass="font-garamond font-bold text-center">       
            <p class="text-[25em]">
                404
            </p>
            <p class="text-4xl">
            Oops! The page you have requested does not exist.
            </p>
        </asp:Panel>
        <div class="flex justify-center p-20">
        <asp:Button runat="server" ID="btnErrToHome" PostBackUrl="~/Home.aspx" CssClass="rounded-full px-5 py-2 bg-accent hover:bg-accent-hover text-white font-garamond font-bold text-xl" Text="Return to Home Page"/>
        </div>
        <script src="Scripts/dist/OopsPage.dist.js"></script>
    </asp:Content>