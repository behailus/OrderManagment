<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OrderManagment.Core.Entities.Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	OrderReceived
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%:ViewData["Message"]%></h2>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Order Information</legend>
            <br />
            <fieldset>
            <legend>Customer Detail</legend>
             <div class="editor-label">
                <%: Html.Label("Full Name") %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Customer.FullName) %>
            </div>
            <div class="editor-label">
                <%: Html.Label("E-mail") %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Customer.Email) %>
            </div>
            <div class="editor-label">
                <%: Html.Label("Discount Code") %>
            </div>
            <div class="editor-field">
                <%: Html.TextBox("DiscountCode")%>
            </div>
            </fieldset>
           <br />
           <fieldset>
           <legend>Order Detail</legend>
           <div class="editor-label">
                <%:Html.Label("Order number") %>
           </div>
           <div class="editor-field">
                <%:Html.TextBoxFor(model=>model.OrderNumber) %>
           </div>
           </fieldset>
            <p>
                <input type="submit" value="Order" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>
