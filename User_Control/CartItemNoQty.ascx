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
        <p class="">Qty: <%= Quantity %></p>
        <p class="mt-auto ml-auto">RM <%= (CartItem.Art.Price * Quantity).ToString("0.00") %></p>
    </div>
</div>
                