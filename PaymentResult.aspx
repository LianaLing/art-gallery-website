<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentResult.aspx.cs" Inherits="ArtGalleryWebsite.PaymentResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if (error != null) { %>
        <div class="flex flex-col justify-center items-center py-24 font-garamond">
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" class="iconify iconify--emojione" width="128" height="128" preserveAspectRatio="xMidYMid meet" viewBox="0 0 64 64"><path d="M62 52c0 5.5-4.5 10-10 10H12C6.5 62 2 57.5 2 52V12C2 6.5 6.5 2 12 2h40c5.5 0 10 4.5 10 10v40z" fill="#ff5a79"></path><path fill="#fff" d="M50 21.2L42.8 14L32 24.8L21.2 14L14 21.2L24.8 32L14 42.8l7.2 7.2L32 39.2L42.8 50l7.2-7.2L39.2 32z"></path></svg>
            <h2 class="text-3xl mt-4 font-bold text-red-500"><%= error %></h2>
            <button onclick="returnToHome(event);" class="py-2 px-4 mt-4 rounded-3xl bg-accent hover:bg-accent-hover text-white">Return to Home</button>
        </div>
    <% } %>
    <% if (order!= null && payment != null) { %> 
        <div class="flex flex-col justify-center items-center py-24 font-garamond">
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" class="iconify iconify--emojione" width="128" height="128" preserveAspectRatio="xMidYMid meet" viewBox="0 0 64 64"><circle cx="32" cy="32" r="30" fill="#4bd37b"></circle><path fill="#fff" d="M46 14L25 35.6l-7-7.2l-7 7.2L25 50l28-28.8z"></path></svg>
            <h2 class="text-3xl mt-4 font-bold">Payment <%= order.Status %></h2>
            <p class="text-sm mt-2">RM <%= (payment.Amount + payment.Tax).ToString("0.00") %> paid at <%= order.UpdatedAt.ToLocalTime() %></p>
            <button onclick="returnToHome(event);" class="py-2 px-4 mt-4 rounded-3xl bg-accent hover:bg-accent-hover text-white">Return to Home</button>
        </div>
    <% } %>

    <script>
        const returnToHome = (event) => {
            event.preventDefault();
            window.location.href = "Home.aspx";
        };
    </script>
</asp:Content>
