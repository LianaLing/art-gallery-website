<%@ Page Title="Site Map" Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="ArtGalleryWebsite.SiteMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="font-garamond flex flex-col justify-center">
            <div class="text-accent text-4xl text-center font-bold">
                Art Gallery Website Sitemap</div>
            <asp:SiteMapPath ID="SiteMapPath1" runat="server">
            </asp:SiteMapPath>
        </div>
        <asp:Menu ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1" OnMenuItemClick="Menu1_MenuItemClick">
        </asp:Menu>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
    </form>
</body>
</html>
