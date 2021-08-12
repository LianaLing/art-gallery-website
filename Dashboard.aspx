<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ArtGalleryWebsite.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="app"></div>

    <asp:GridView
        ID="ArtsGrid"
        runat="server"
        AutoGenerateColumns="false"
        OnRowEditing="ArtsGrid_RowEditing"
        OnRowUpdating="ArtsGrid_RowUpdating"
        OnRowCancelingEdit="ArtsGrid_RowCancelingEdit"
        CssClass="w-full font-garamond"
        HeaderStyle-CssClass="border-b bg-[#fafafa]"
        RowStyle-CssClass="border-b hover:bg-[#fafafa] font-medium transition-colors duration-200 ease-in-out"
    >
        <Columns>
            <asp:TemplateField
                HeaderText="ID"
                HeaderStyle-CssClass="text-left table-header-cell"
                ItemStyle-CssClass="w-[15px] table-cell"
            >
                <ItemTemplate>
                    <asp:Label ID="ArtId" runat="server" Text='<%# Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField
                HeaderText="Image"
                HeaderStyle-CssClass="table-header-cell"
                ItemStyle-CssClass="table-cell w-[82px]"
            >
                <ItemTemplate>
                    <img
                        src='<%# Eval("url")%>'
                        alt='<%# Eval("description")%>'
                        class="rounded-lg mx-auto object-cover h-[50px] w-[50px]"
                    />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:FileUpload ID="ArtImageUpload" runat="server" CssClass="hidden" />
                    <button id="ImageUpload" class="file-upload-input">
                        <svg class="h-6 w-6" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" focusable="false" width="1em" height="1em" style="-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);" preserveAspectRatio="xMidYMid meet" viewBox="0 0 20 20"><path d="M8 12h4V6h3l-5-5l-5 5h3v6zm11.338 1.532c-.21-.224-1.611-1.723-2.011-2.114A1.503 1.503 0 0 0 16.285 11h-1.757l3.064 2.994h-3.544a.274.274 0 0 0-.24.133L12.992 16H7.008l-.816-1.873a.276.276 0 0 0-.24-.133H2.408L5.471 11H3.715c-.397 0-.776.159-1.042.418c-.4.392-1.801 1.891-2.011 2.114c-.489.521-.758.936-.63 1.449l.561 3.074c.128.514.691.936 1.252.936h16.312c.561 0 1.124-.422 1.252-.936l.561-3.074c.126-.513-.142-.928-.632-1.449z" fill="currentColor"/></svg>
                        <span class="font-medium text-sm">Upload</span>
                    </button>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField
                HeaderText="Description"
                HeaderStyle-CssClass="table-header-cell"
                ItemStyle-CssClass="table-cell"
            >
                <ItemTemplate>
                    <asp:Label ID="ArtDescription" runat="server" Text='<%# Eval("description") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox
                        ID="ArtDescriptionEdit"
                        runat="server"
                        Text='<%# Eval("description")%>'
                        CssClass="text-input w-full"
                    />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField
                HeaderText="Price"
                HeaderStyle-CssClass="table-header-cell"
                ItemStyle-CssClass="table-cell w-[180px]"
            >
                <ItemTemplate>
                    <div class="flex flex-row justify-center items-center">
                        <asp:Label ID="ArtPrice" runat="server" Text='<%# Eval("price", "RM {0:0.00}") %>' />
                        <img src="https://api.iconify.design/noto:money-bag.svg" class="ml-1" />
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="flex flex-row justify-center items-center">
                        <p class="mr-2">RM</p>
                        <asp:TextBox
                            ID="ArtPriceEdit"
                            runat="server"
                            Text='<%# Eval("price", "{0:0.00}")%>'
                            TextMode="Number"
                            min="0"
                            max="9999999"
                            step="1"
                            CssClass="text-input"
                        />
                        <img src="https://api.iconify.design/noto:money-bag.svg" class="ml-1" />
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField
                HeaderText="Stock"
                HeaderStyle-CssClass="table-header-cell"
                ItemStyle-CssClass="table-cell w-[100px]"
            >
                <ItemTemplate>
                    <div class="flex flex-row justify-center items-center">
                        <asp:Label ID="ArtStock" runat="server" Text='<%# Eval("stock") %>' />
                        <img src="https://api.iconify.design/noto:package.svg" class="ml-1" />
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="flex flex-row justify-center items-center">
                        <asp:TextBox
                            ID="ArtStockEdit"
                            runat="server"
                            Text='<%# Eval("stock")%>'
                            TextMode="Number"
                            min="0"
                            max="9999999"
                            step="1"
                            CssClass="text-input w-[70px]"
                        />
                        <img src="https://api.iconify.design/noto:package.svg" class="ml-1" />
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField
                HeaderText="Likes"
                HeaderStyle-CssClass="table-header-cell"
                ItemStyle-CssClass="table-cell w-[100px]"
            >
                <ItemTemplate>
                    <div class="flex flex-row justify-center items-center">
                        <asp:Label ID="ArtLikes" runat="server" Text='<%# Eval("likes") %>' />
                        <img src="https://api.iconify.design/noto:heart-suit.svg" class="ml-1" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField
                HeaderText="Style"
                HeaderStyle-CssClass="table-header-cell"
                ItemStyle-CssClass="w-[160px] table-cell"
            >
                <ItemTemplate>
                    <asp:Label
                        ID="ArtStyle"
                        runat="server"
                        Text='<%# Eval("style") %>'
                        CssClass="badge-purple"
                    />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="ArtStyleEdit" runat="server" Text='<%# Eval("style")%>' CssClass="text-input" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField
                ShowEditButton="True"
                HeaderText="Controls"
                HeaderStyle-CssClass="table-header-cell"
                ItemStyle-CssClass="w-[50px] table-cell text-center edit-link"
                EditText="Edit"
            />
        </Columns>
    </asp:GridView>

    <script src="Scripts/dist/DashboardPage.dist.js"></script>
    <script>
        // Get the upload button
        const btn = document.querySelector("#ImageUpload");
        // Get the current asp:FileUpload id
        const currentFileUploadId = "MainContent_ArtsGrid_ArtImageUpload_<%= ArtsGrid.EditIndex %>";

        // If btn exists
        if (btn) {
            // Attach the file upload listener
            btn.addEventListener("click", () => {
                document.querySelector(`#${currentFileUploadId}`).click();
            });
        }
    </script>
</asp:Content>
