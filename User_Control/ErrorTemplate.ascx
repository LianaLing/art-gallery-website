<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorTemplate.ascx.cs" Inherits="ArtGalleryWebsite.User_Control.ErrorTemplate" %>

        <asp:Panel runat="server" ID="ErrMes" CssClass="font-garamond font-bold text-center">       
            <asp:Label runat="server" ID="lblHttpCode" CssClass="text-[24em] block"/>
            <asp:Label runat="server" ID="lblErrBody" CssClass="text-4xl block"/>
        </asp:Panel>
        <div class="flex justify-center p-20">
        <asp:Button runat="server" ID="btnErrToHome" PostBackUrl="~/Home.aspx" CssClass="rounded-full px-5 py-2 bg-accent hover:bg-accent-hover text-white font-garamond font-bold text-xl" Text="Return to Home Page"/>
        </div>