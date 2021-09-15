<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs"
    Inherits="ArtGalleryWebsite.Cart" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <%-- Hidden buttons to receive and process simulated clicks from frontend --%>

        <%-- Frontend code --%>
        <div id="app" class="hidden">
        </div>

        <%--  --%>
  <div class="flex font-garamond lg:mx-[350px] lg:my-[30px]">
    <!-- Cart -->
    <div class="lg:w-3/5">
      <div class="px-5">
        <div class="py-5">
          <p class="font-bold text-4xl">Your Cart</p>
        </div>
        <!-- Item Header -->
          <asp:Button
            ID="hdrItems"
            OnClick="btnShowItems_click"
            runat="server"
            CssClass="
              px-5
              py-2
              bg-accent
              text-white
              rounded-full
              flex
              w-full
              hover:bg-accent-hover
              text-2xl
              font-bold
            "
            Text="Items"
          />
          <!-- <div class="
              w-0
              h-0
              border-l-[5px]
              border-r-[5px]
              border-t-[10px]
              border-r-transparent
              border-l-transparent
              border-t-red" /> -->
        <!-- Item Detail -->
        <asp:Panel runat="server" ID="ItemsList">
          <% foreach (var art in Arts) { %>
            <div class="py-5 flex w-full mt-2">
              <!-- Image -->
              <div class="w-[8.5em] mr-5">
                <img
                  class="object-cover w-[8.5em] h-[8.5em] rounded-3xl"
                  src="<%= art.Url %>"
                  alt="<%= art.Description %>"
                />
              </div>
              <!-- Description -->
              <div class="flex flex-col w-[75%]">
                <p class="font-bold text-xl"><%= art.Description %></p>
                <p>By <%= art.Author.Name %></p>
                <p class="mt-auto ml-auto">RM <%= art.Price %></p>
              </div>
            </div>
          <% } %>
        </asp:Panel>
        <!-- Shipping and Billing -->
        <asp:Button
          ID="hdrShipBill"
          OnClick="btnShowShipBill_click"
          runat="server"
          CssClass="
            text-2xl
            font-bold
            px-5
            py-2
            bg-accent
            text-white
            rounded-full
            flex
            w-full
            my-5
            hover:bg-accent-hover
          "
          Text="Shipping and Billing"
          />
        <asp:Panel runat="server" ID="ShipBill" CssClass="px-5 text-xl">
          <!-- Shipping Address -->
          <div>
            <p class="font-bold">Shipping Address</p>
            <div class="grid grid-cols-2 grid-auto-rows">
              <label for="txtFullName" class="p-2"> Full Name </label>
              <asp:RequiredFieldValidator runat="server" ID="ReqFullName"
                  ErrorMessage="* Required"
                  ControlToValidate="txtFullName"
                  CssClass="text-red text-sm text-right"
                  ValidationGroup="VGShipBill"
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
                  ValidationGroup="VGShipBill"
                  >
                  </asp:RegularExpressionValidator>

              <label for="txtEmail" class="p-2"> Email</label>
              <asp:RequiredFieldValidator runat="server" ID="ReqEmail"
                  ErrorMessage="* Required"
                  ControlToValidate="txtEmail"
                  CssClass="text-red text-sm text-right"
                  ValidationGroup="VGShipBill"
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
                  ValidationGroup="VGShipBill"
                  >
                  </asp:RegularExpressionValidator>

              <label for="txtAddrL1" class="p-2"> Address Line 1 </label>
              <asp:RequiredFieldValidator runat="server" ID="ReqAddrL1"
                  ErrorMessage="* Required"
                  ControlToValidate="txtAddrL1"
                  CssClass="text-red text-sm text-right"
                  ValidationGroup="VGShipBill"
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
                ValidationGroup="VGShipBill"
                >
                </asp:RegularExpressionValidator>
              <label for="txtAddrL2" class="p-2"> Address Line 2 </label>
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
                ValidationGroup="VGShipBill"
                >
                </asp:RegularExpressionValidator>
              </div>

            <div class="grid grid-cols-4 grid-auto-rows row-auto gap-x-2">
              <label for="txtAddrCity" class="p-2"> City </label>
              <asp:RequiredFieldValidator runat="server" ID="ReqAddrCity"
                  ErrorMessage="* Required"
                  ControlToValidate="txtAddrCity"
                  CssClass="text-red text-sm text-right"
                  ValidationGroup="VGShipBill"
                  >
                  </asp:RequiredFieldValidator>
              <label for="txtAddrPC" class="p-2"> Postal Code </label>
              <asp:RequiredFieldValidator runat="server" ID="ReqAddrPC"
                  ErrorMessage="* Required"
                  ControlToValidate="txtAddrPC"
                  CssClass="text-red text-sm text-right"
                  ValidationGroup="VGShipBill"
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
                ValidationGroup="VGShipBill"
                CssClass="text-red text-sm col-start-1 col-end-3"
                >
                </asp:RegularExpressionValidator>
              <asp:RegularExpressionValidator runat="server" ID="RangeAddrPC"
                ControlToValidate="txtAddrPC"
                ErrorMessage="Please enter a valid postal code."
                ValidationExpression="^([0-9A-Za-z]{5}|[0-9A-Za-z]{9}|(([0-9a-zA-Z]{5}-){1}[0-9a-zA-Z]{4}))$"
                ValidationGroup="VGShipBill"
                CssClass="text-red text-sm col-start-3 col-end-5"
                >
                </asp:RegularExpressionValidator>
              <label for="txtAddrState" class="p-2"> State </label>
              <asp:RequiredFieldValidator runat="server" ID="ReqAddrState"
                  ErrorMessage="* Required"
                  ControlToValidate="txtAddrState"
                  CssClass="text-red text-sm text-right"
                  ValidationGroup="VGShipBill"
                  >
                  </asp:RequiredFieldValidator>
              <label for="txtAddrCountry" class="p-2"> Country </label>
              <asp:RequiredFieldValidator runat="server" ID="ReqAddrCountry"
                  ErrorMessage="* Required"
                  ControlToValidate="txtAddrCountry"
                  CssClass="text-red text-sm text-right"
                  ValidationGroup="VGShipBill"
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
                ValidationGroup="VGShipBill col-start-1 col-end-3"
                CssClass="text-red text-sm"
                >
                </asp:RegularExpressionValidator>
              <asp:RegularExpressionValidator runat="server" ID="RangeAddrCountry"
                ControlToValidate="txtAddrCountry"
                ErrorMessage="Please enter a valid country."
                ValidationExpression="[a-zA-Z ]{2,255}"
                ValidationGroup="VGShipBill"
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
                  ValidationGroup="VGShipBill"
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
                  ValidationGroup="VGShipBill"
                  >
                  </asp:RegularExpressionValidator>
            </div>

          </div>
        </asp:Panel>
      </div>
    </div>
    <!-- Receipt -->
    <div class="px-5 w-auto lg:w-2/5 self-start sticky top-0">
      <p class="py-5 text-2xl font-bold">Order Summary</p>
      <div class="text-xl">
        <div class="flex">
          <p class="w-4/6">Subtotal</p>
          <!-- Testing -->
          <p class="w-1/6 text-gray-500">RM</p>
          <asp:Label runat="server" ID="lblSubtotal" class="w-1/6 text-right" Text="Subtotal"/>
        </div>
        <div class="flex">
          <p class="w-4/6">Shipping</p>
          <p class="w-1/6 text-gray-500">RM</p>
          <!-- Testing -->
          <asp:Label runat="server" ID="lblShipping" class="w-1/6 text-right" Text="Shipping"/>
        </div>
        <div class="my-5 border-t-2 border-b-2 border-gray-200 py-5 flex">
          <p class="w-4/6 font-bold">Order Total</p>
          <p class="w-1/6 text-gray-500">RM</p>
          <!-- Testing -->
          <asp:Label runat="server" ID="lblTotal" class="w-1/6 text-right" Text="Total"/>
        </div>
      </div>
      <!-- Payment -->
      <div class="text-xl">
        <p class="font-bold">Payment</p>
        <div class="py-5 border-b-2 border-gray-100">
          <asp:RadioButton
            runat="server"
            CssClass="form-radio text-accent mr-2"
            Checked="True"
            ID="rdbtnCard"
            value="card"
            GroupName="PayType"
          />
          <label for="rdbtnCard" class=""> Visa/Master/Amex </label><br/>
          <asp:RadioButton
            runat="server"
            CssClass="form-radio text-accent mr-2"
            ID="rdbtnTng"
            value="tng"
            GroupName="PayType"
          />
          <label for="rdbtnTng" class=""> Touch n' Go </label><br/>
          <asp:RadioButton
            runat="server"
            CssClass="form-radio text-accent mr-2"
            ID="rdbtnGrab"
            value="grabpay"
            GroupName="PayType"
          />
          <label for="rdbtnGrab" class=""> GrabPay </label><br/>
          <asp:RadioButton
            runat="server"
            CssClass="form-radio text-accent mr-2"
            ID="rdbtnFpx"
            value="fpx"
            GroupName="PayType"
          />
          <label for="rdbtnFpx" class=""> FPX </label><br/>
        </div>
        <!-- Continue Button -->
        <div class="py-5 flex justify-center">
          <asp:Button
            runat="server"
            ID="btnContinue"
            OnClick="btnContinue_click"
            CssClass="
              bg-accent
              text-white
              font-bold
              px-5
              py-2
              rounded-full
              hover:bg-accent-hover
            "
            Text="Continue With Transaction"
          />
        <!-- Pay Button -->
          <asp:Button
            runat="server"
            ID="btnPayWith"
            OnClick="btnPayWith_click"
            CssClass="
              bg-accent
              text-white
              font-bold
              px-5
              py-2
              rounded-full
              hover:bg-accent-hover
            "
            Text="Pay Using This Method"
          />
        </div>
      </div>
      <!-- Confirmation Message -->
      <asp:Panel runat="server" ID="PaymentIndicator" CssClass="p-10 border-accent border-dashed border-2 display">
        <asp:Label runat="server" ID="lblPayConfirmHeader" class="text-xl block font-bold text-accent"/>
        <asp:Label runat="server" ID="lblPayConfirmBody" class="text-lg block whitespace-pre-wrap"/>
      </asp:Panel>
    </div>
  </div>

        <script src="Scripts/dist/CartPage.dist.js"></script>
        <script>
            function DisableHiddenValidators() {
                for (var i = 0; i < Page_Validators.length; i++) {
                  var visible = $('#' + Page_Validators[i].controltovalidate).is(':visible');
                  ValidatorEnable(Page_Validators[i], visible)
                }
                return Page_ClientValidate();
              }
            </script>
    </asp:Content>
