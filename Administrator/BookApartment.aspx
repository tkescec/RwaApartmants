<%@ Page Title="Rezervacija apartmana" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="BookApartment.aspx.cs" Inherits="Administrator.BookApartment" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2 id="titleApartment" runat="server"></h2>
            </div>
        </div>
    </div>

    <div class="row">
        <!-- Listings -->
        <div class="col-lg-12 col-md-12">
            <div class="add-listing">

                <div class="add-listing-section switcher-on">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-doc"></i>Detalji</h3>
                    </div>

                    <div class="switcher-content">
                        <div class="row with-forms">
                            <div class="col-md-12">
                                <h5>Detalji rezervacije <i class="tip" data-tip-content="Upišite dodatne podatke ili napomenu za rezervaciju."></i></h5>
                                <asp:TextBox ID="txtBookingDetails" class="search-field" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="add-listing-section margin-top-45 switcher-on">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-user-following"></i>Postojeći korisnik</h3>
                    </div>

                    <div class="switcher-content">
                        <div class="row with-forms">
                            <div class="col-md-12">
                                <h5>Korisnik <i class="tip" data-tip-content="Odaberi postojećeg korisnika ili ostavi prazno kako bi dodao gosta."></i></h5>
                                <asp:DropDownList ID="ddlBookingUsers" runat="server" CssClass="booking-users" AutoPostBack="false" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Odaberi korisnika" Value="" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="add-listing-section margin-top-45 switcher-on" id="guestUser">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-user-unfollow"></i>Gost</h3>
                    </div>
                    <div class="switcher-content">
                        <div class="row with-forms">
                            <div class="col-md-6">
                                <h5>Ime <i class="tip" data-tip-content="Upišite ime gosta."></i></h5>
                                <asp:TextBox ID="txtUserName" class="search-field" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <h5>Email <i class="tip" data-tip-content="Upišite email adresu gosta."></i></h5>
                                <asp:TextBox ID="txtUserEmail" class="search-field" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row with-forms">
                            <div class="col-md-6">
                                <h5>Telefon <i class="tip" data-tip-content="Upišite telefonski broj gosta."></i></h5>
                                <asp:TextBox ID="txtUserPhone" class="search-field" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <h5>Adresa <i class="tip" data-tip-content="Upišite adresu gosta."></i></h5>
                                <asp:TextBox ID="txtUserAddress" class="search-field" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>

                <asp:LinkButton ID="btnBookApartment" CssClass="button preview" OnClick="btnBookApartment_Click" runat="server">Rezerviraj apartman <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>

            </div>

        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="Script" runat="server">
    <script>
        (function () {
            $('.dashboard-nav .sl-icon-home').parent().parent().addClass('active');
            $('.booking-users').chosen({ disable_search_threshold: 100, width: "100%" }).change(function (e) {
                if (e.target == this) {
                    if ($(this).val() == "") {
                        $("#guestUser").addClass('switcher-on');
                    } else {
                        $("#guestUser").removeClass('switcher-on');
                        $("#guestUser .search-field").val("");
                    }
                }
            });
        })();
    </script>
</asp:Content>
