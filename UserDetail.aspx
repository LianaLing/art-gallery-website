<%@ Page Title="User Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="UserDetail.aspx.cs"
    Inherits="ArtGalleryWebsite.UserDetail" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div id="app">
        </div>

        <script src="Scripts/dist/UserDetailPage.dist.js"></script>

        <div class="px-[10em] py-10">
            <div class="flex font-garamond text-xl justify-center">
                <asp:Panel runat="server" ID="UserDetailForm" CssClass="px-5 w-1/2">
                <!-- User Details -->
                    <p class="font-bold">Edit User Details</p>
                    <div class="grid grid-cols-2 grid-auto-rows gap-2">
                    <label for="txtFullName" class="p-2"> Full Name </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqFullName"
                        ErrorMessage="* Required"
                        ControlToValidate="txtFullName"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input block rounded-full block w-full col-span-2"
                        ID="txtFullName"
                        placeholder="John M. Doe"
                    />
                    <asp:RegularExpressionValidator runat="server" ID="RegexFullName"
                        ErrorMessage="Please enter alphabets and spaces only."
                        ControlToValidate="txtFullName"
                        ValidationExpression="[a-zA-Z]+([\s][a-zA-Z]+)*"
                        CssClass="text-red text-sm col-span-2"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RegularExpressionValidator>

                    <label for="txtDOB" class="p-2"> Date Of Birth </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqDOB"
                        ErrorMessage="* Required"
                        ControlToValidate="txtDOB"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <asp:Calendar
                        runat="server"
                        CssClass=""
                        ID="calDOB"
                        OnSelectionChanged="calDOB_changed"
                        OnDayRender="calDOB_dayRender"
                    />
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full h-[42px] block place-self-stretch self-end"
                        ID="txtDOB"
                        placeholder="DD/MM/YYYY"
                    />
                    <asp:CompareValidator
                        runat="server"
                        ID="CompareDOB"
                        ErrorMessage="Please enter a valid date."
                        ControlToValidate="txtDOB"
                        Type="Date"
                        Operator="LessThanEqual"
                        CssClass="text-red text-sm col-span-2"
                        ValidationGroup="VGUserDetail"
                    />

                    <label for="txtAddrL1" class="p-2"> Default Address Line 1 </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqAddrL1"
                        ErrorMessage="* Required"
                        ControlToValidate="txtAddrL1"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full block w-full col-span-2"
                        ID="txtAddrL1"
                        placeholder="542 W. 15th Street"
                    />
                    <asp:RegularExpressionValidator runat="server" ID="RangeAddrL1"
                        ControlToValidate="txtAddrL1"
                        ErrorMessage="Please enter less than 256 characters."
                        ValidationExpression="^.{1,255}$"
                        CssClass="text-red text-sm col-span-2"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RegularExpressionValidator>
                    <label for="txtAddrL2" class="p-2"> Default Address Line 2 </label>
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full block w-full col-span-2"
                        ID="txtAddrL2"
                        placeholder="Apartment 1, Suite 2"
                    />
                    <asp:RegularExpressionValidator runat="server" ID="RangeAddrL2"
                        ControlToValidate="txtAddrL2"
                        ErrorMessage="Please enter less than 256 characters."
                        ValidationExpression="^.{1,255}$"
                        CssClass="text-red text-sm"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="grid grid-cols-4 grid-auto-rows row-auto gap-x-2">
                    <label for="txtAddrCity" class="p-2"> City </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqAddrCity"
                        ErrorMessage="* Required"
                        ControlToValidate="txtAddrCity"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <label for="txtAddrPC" class="p-2"> Postal Code </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqAddrPC"
                        ErrorMessage="* Required"
                        ControlToValidate="txtAddrPC"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full col-start-1 col-end-3"
                        ID="txtAddrCity"
                        placeholder="Shah Alam"
                    />
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full col-start-3 col-end-5"
                        ID="txtAddrPC"
                        placeholder="40000"
                    />
                    <asp:RegularExpressionValidator runat="server" ID="RangeAddrCity"
                        ControlToValidate="txtAddrCity"
                        ErrorMessage="Please enter a valid city."
                        ValidationExpression="[a-zA-Z ]{2,255}"
                        ValidationGroup="VGUserDetail"
                        CssClass="text-red text-sm col-start-1 col-end-3"
                        >
                        </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator runat="server" ID="RangeAddrPC"
                        ControlToValidate="txtAddrPC"
                        ErrorMessage="Please enter a valid postal code."
                        ValidationExpression="^([0-9A-Za-z]{5}|[0-9A-Za-z]{9}|(([0-9a-zA-Z]{5}-){1}[0-9a-zA-Z]{4}))$"
                        ValidationGroup="VGUserDetail"
                        CssClass="text-red text-sm col-start-3 col-end-5"
                        >
                        </asp:RegularExpressionValidator>
                    <label for="txtAddrState" class="p-2"> State </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqAddrState"
                        ErrorMessage="* Required"
                        ControlToValidate="txtAddrState"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <label for="txtAddrCountry" class="p-2"> Country </label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqAddrCountry"
                        ErrorMessage="* Required"
                        ControlToValidate="txtAddrCountry"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full col-start-1 col-end-3"
                        ID="txtAddrState"
                        placeholder="Selangor"
                    />
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full col-start-3 col-end-5"
                        ID="txtAddrCountry"
                        placeholder="Malaysia"
                    />
                    <asp:RegularExpressionValidator runat="server" ID="RangeAddrState"
                        ControlToValidate="txtAddrState"
                        ErrorMessage="Please enter a valid state."
                        ValidationExpression="[a-zA-Z ]{2,255}"
                        ValidationGroup="VGUserDetail col-start-1 col-end-3"
                        CssClass="text-red text-sm"
                        >
                        </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator runat="server" ID="RangeAddrCountry"
                        ControlToValidate="txtAddrCountry"
                        ErrorMessage="Please enter a valid country."
                        ValidationExpression="[a-zA-Z ]{2,255}"
                        ValidationGroup="VGUserDetail"
                        CssClass="text-red text-sm col-start-3 col-end-5"
                        >
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="grid grid-cols-2 grid-auto-rows grid-rows-auto">
                    <label for="txtPhone" class="p-2">Phone</label>
                    <asp:RequiredFieldValidator runat="server" ID="ReqPhone"
                        ErrorMessage="* Required"
                        ControlToValidate="txtPhone"
                        CssClass="text-red text-sm text-right"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RequiredFieldValidator>
                    <asp:TextBox
                        runat="server"
                        CssClass="form-input rounded-full block w-full col-span-2"
                        id="txtPhone"
                        placeholder="+6012-3456 789"
                    />
                    <asp:RegularExpressionValidator runat="server" ID="RegexPhone"
                        ErrorMessage="Please enter a valid phone number."
                        ControlToValidate="txtPhone"
                        ValidationExpression="^(00|\+)[1-9]{1}([0-9-][\s]*){9,20}$"
                        CssClass="text-red text-sm col-span-2"
                        ValidationGroup="VGUserDetail"
                        >
                        </asp:RegularExpressionValidator>
                    </div>
                </asp:Panel>

            <!-- Edit Account Details -->
                <asp:Panel runat="server" ID="AccountDetailForm" CssClass="px-5 w-1/2">
                    <p class="font-bold">Edit Account Details</p>
                    <div class="grid grid-cols-2 grid-auto-rows gap-2">
                        <!-- Avatar URL -->
                        <!-- Username -->
                        <label for="txtUsername" class="p-2"> Username </label>
                        <asp:RequiredFieldValidator runat="server" ID="ReqUsername"
                            ErrorMessage="* Required"
                            ControlToValidate="txtUsername"
                            CssClass="text-red text-sm text-right"
                            ValidationGroup="VGAccountDetail"
                            >
                            </asp:RequiredFieldValidator>
                        <asp:TextBox
                            runat="server"
                            CssClass="form-input rounded-full block w-full col-span-2"
                            ID="txtUsername"
                            placeholder="johndoe"
                        />
                        <asp:CompareValidator runat="server" ID="CompareUsername"
                            ErrorMessage="Please try again because this username is taken."
                            ControlToValidate="txtUsername"
                            Type="String"
                            CssClass="text-red text-sm col-span-2"
                            Operator="Equal"
                            ValidationGroup="VGUserDetail"
                            />
                        </div>
                        <!-- Password -->
                        <!-- Email -->
                        <label for="txtEmail" class="p-2"> Email</label>
                        <asp:RequiredFieldValidator runat="server" ID="ReqEmail"
                            ErrorMessage="* Required"
                            ControlToValidate="txtEmail"
                            CssClass="text-red text-sm text-right"
                            ValidationGroup="VGUserDetail"
                            >
                            </asp:RequiredFieldValidator>
                        <asp:TextBox
                            runat="server"
                            CssClass="form-input rounded-full block w-full col-span-2"
                            ID="txtEmail"
                            placeholder="john@example.com"
                        />
                        <asp:RegularExpressionValidator runat="server" ID="RegexEmail"
                            ErrorMessage="Please enter a valid email."
                            ControlToValidate="txtEmail"
                            ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"
                            CssClass="text-red text-sm col-span-2"
                            ValidationGroup="VGAccountDetail"
                            >
                            </asp:RegularExpressionValidator>
                    </asp:Panel>
                </div> <!-- Forms Div -->

                <div class="flex justify-center gap-36 py-10">
                    <!-- Cancel Button -->
                    <asp:Button
                        runat="server"
                        ID="btnCancel"
                        OnClick="btnCancel_click"
                        CssClass="
                            bg-light
                            text-xl
                            font-bold
                            font-garamond
                            justify-center
                            px-5
                            py-2
                            rounded-full
                            hover:bg-light-hover
                            "
                        Text="Cancel Changes"
                    />
                    <!-- Submit Button -->
                    <asp:Button
                        runat="server"
                        ID="btnSubmit"
                        OnClick="btnSubmit_click"
                        CssClass="
                            bg-accent
                            text-white
                            text-xl
                            font-bold
                            font-garamond
                            justify-center
                            px-5
                            py-2
                            rounded-full
                            hover:bg-accent-hover
                            "
                        Text="Apply Changes"
                    />
                </div> <!-- Buttons Div -->
        </div> <!-- Main Div -->

    </asp:Content>