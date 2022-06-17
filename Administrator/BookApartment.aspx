<%@ Page Title="Rezervacija apartmana" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="BookApartment.aspx.cs" Inherits="Administrator.BookApartment" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Rezervacija apartmana</h2>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="Script" runat="server">
    <script>
        (function () {
            $('.dashboard-nav .sl-icon-home').parent().parent().addClass('active');
        })();
    </script>
</asp:Content>
