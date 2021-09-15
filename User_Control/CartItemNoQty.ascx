<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CartItemNoQty.ascx.cs" Inherits="ArtGalleryWebsite.User_Control.CartItemNoQty" %>

                    <div class="py-5 flex w-full mt-2">
                        <!-- Image -->
                        <div class="w-[8.5em] mr-5">
                            <img
                                ID="ciImg"
                                class="object-cover w-[8.5em] h-[8.5em] rounded-3xl"
                                src="<%= CartItem.Art.Url %>"
                                alt="<%= CartItem.Art.Description %>" />
                        </div>
                        <!-- Description -->
                        <div class="flex flex-col w-[75%]">
                            <p class="font-bold text-xl"><%= CartItem.Art.Description %></p>
                            <p>By <%= CartItem.Art.Author.Name %></p>
                            <div class="flex">
                                <asp:Button runat="server" ID="btnQtyDecr" CssClass="
                                  bg-light
                                  font-bold
                                  px-4
                                  py-1
                                  rounded-full
                                  hover:bg-light-hover" OnClick="btnQtyDecr_click" Text="-" />
                                <p class="">Qty: <%= Quantity %></p>
                                <asp:Button runat="server" ID="btnQtyIncr" CssClass="
                                  bg-light
                                  font-bold
                                  px-4
                                  py-1
                                  rounded-full
                                  hover:bg-light-hover" OnClick="btnQtyIncr_click" Text="+" />
                                </div>
                            <p class="mt-auto ml-auto">RM <%= CartItem.Art.Price * Quantity %></p>
                        </div>
                    </div>
                