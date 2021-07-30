<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="ArtGalleryWebsite.Home" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div id="app">
        </div>
<%--        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtGalleryDB %>" SelectCommand="SELECT * FROM [Art]"></asp:SqlDataSource>
        <asp:DataList ID="DataList1" runat="server" DataKeyField="id" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                id:
                <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                <br />
                style:
                <asp:Label ID="styleLabel" runat="server" Text='<%# Eval("style") %>' />
                <br />
                description:
                <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("description") %>' />
                <br />
                price:
                <asp:Label ID="priceLabel" runat="server" Text='<%# Eval("price") %>' />
                <br />
                stock:
                <asp:Label ID="stockLabel" runat="server" Text='<%# Eval("stock") %>' />
                <br />
                likes:
                <asp:Label ID="likesLabel" runat="server" Text='<%# Eval("likes") %>' />
                <br />
                author_id:
                <asp:Label ID="author_idLabel" runat="server" Text='<%# Eval("author_id") %>' />
                <br />
                <br />
            </ItemTemplate>
        </asp:DataList>--%>

        <script src="Scripts/dist/HomePage.dist.js"></script>
    </asp:Content>