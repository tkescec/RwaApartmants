<%@ Page Title="Apartmani" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Apartments.aspx.cs" Inherits="Administrator.Apartments" %>

<%@ Register Src="~/Controls/Pagination.ascx" TagPrefix="uc" TagName="Pagination" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Apartmani</h2>
            </div>
        </div>
    </div>

    <div class="row">

        <!-- Listings -->
        <div class="col-lg-12 col-md-12">
            <div class="dashboard-list-box margin-top-0">

                <!-- Booking Requests Filters  -->
                <div class="booking-requests-filter">

                    <!-- Filter by status -->
                    <div class="sort-by" style="margin-right: 10px">
                        <div class="sort-by-select">
                            <asp:DropDownList ID="filterByStatus" runat="server" CssClass="chosen-select-no-single" AutoPostBack="true" AppendDataBoundItems="true">
                                <asp:ListItem Text="Svi statusi" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Filter by city -->
                    <div class="sort-by" style="margin-right: 10px">
                        <div class="sort-by-select">
                            <asp:DropDownList ID="filterByCity" runat="server" CssClass="chosen-select-no-single" AutoPostBack="true" AppendDataBoundItems="true">
                                <asp:ListItem Text="Svi gradovi" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Sort by -->
                    <div class="sort-by">
                        <div class="sort-by-select">
                            <asp:DropDownList ID="sortBy" runat="server" CssClass="chosen-select-no-single" AutoPostBack="true" AppendDataBoundItems="true">
                                <Items>
                                    <asp:ListItem Text="Sortiraj po" Value="" />
                                    <asp:ListItem Text="Cijeni (uzlazno)" Value="price_asc" />
                                    <asp:ListItem Text="Cijeni (silazno)" Value="price_desc" />
                                    <asp:ListItem Text="Broju soba (uzlazno)" Value="rooms_asc" />
                                    <asp:ListItem Text="Broju soba (silazno)" Value="rooms_desc" />
                                    <asp:ListItem Text="Broju mjesta (uzlazno)" Value="cap_asc" />
                                    <asp:ListItem Text="Broju mjesta (silazno)" Value="cap_desc" />
                                </Items>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <!-- Reply to review popup -->
                <%--<div id="small-dialog" class="zoom-anim-dialog mfp-hide">
                    <div class="small-dialog-header">
                        <h3>Send Message</h3>
                    </div>
                    <div class="message-reply margin-top-0">
                        <textarea cols="40" rows="3" placeholder="Your Message to Kathy"></textarea>
                        <button class="button">Send</button>
                    </div>
                </div>--%>

                <h4>Svi Apartmani</h4>
                <ul>
                    <asp:Repeater ID="rptApartments" runat="server">
                        <ItemTemplate>
                            <asp:Label ID="lblApartmentId" runat="server" Text='<%# ((DAL.Models.Apartment)Container.DataItem).ApartmentID %>' Visible = "false" />

                            <li class='<%# (((DAL.Models.Apartment)Container.DataItem).Status == "Slobodno") 
                                    ? "approved-booking" 
                                    : (((DAL.Models.Apartment)Container.DataItem).Status == "Zauzeto") ? "canceled-booking" : "pending-booking" %>'>
                                <div class="list-box-listing bookings apartments">
                                    <div class="list-box-listing-img">
                                        <asp:Image ID="ApartmentFeaturedImage" runat="server" ImageUrl='<%# ((DAL.Models.Apartment)Container.DataItem).Picture %>' />
                                    </div>
                                    <div class="list-box-listing-content">
                                        <div class="inner">
                                            <h3><%# ((DAL.Models.Apartment)Container.DataItem).Name %>
                                                <span class="booking-status"><%# ((DAL.Models.Apartment)Container.DataItem).Status %></span>
                                            </h3>

                                            <div class="inner-booking-list">
                                                <h5>Broj soba:</h5>
                                                <ul class="booking-list">
                                                    <li class="highlighted"><%# ((DAL.Models.Apartment)Container.DataItem).TotalRooms %></li>
                                                </ul>
                                            </div>

                                            <div class="inner-booking-list">
                                                <h5>Broj osoba:</h5>
                                                <ul class="booking-list">
                                                    <li class="highlighted">
                                                        Odrasli: <%# ((DAL.Models.Apartment)Container.DataItem).MaxAdults %> - 
                                                        Djeca: <%# ((DAL.Models.Apartment)Container.DataItem).MaxChildren %>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="inner-booking-list">
                                                <h5>Cijena:</h5>
                                                <ul class="booking-list">
                                                    <li class="highlighted">€<%# ((DAL.Models.Apartment)Container.DataItem).Price.ToString("N2") %></li>
                                                </ul>
                                            </div>

                                            <div class="inner-booking-list">
                                                <h5>Vlasnik:</h5>
                                                <ul class="booking-list">
                                                    <li><%# ((DAL.Models.Apartment)Container.DataItem).Owner %></li>
                                                    <li><%# ((DAL.Models.Apartment)Container.DataItem).Address %></li>
                                                    <li><%# ((DAL.Models.Apartment)Container.DataItem).City %></li>
                                                </ul>
                                            </div>

                                           <%-- <a href="#small-dialog" class="rate-review popup-with-zoom-anim"><i class="sl sl-icon-envelope-open"></i>Send Message</a>--%>

                                        </div>
                                    </div>
                                </div>
                                <div class="buttons-to-right">
                                    <a href='<%# "/BookApartment.aspx?ApartmentId=" + ((DAL.Models.Apartment)Container.DataItem).ApartmentID %>' class="button gray approve"><i class="sl sl-icon-call-in"></i> Rezerviraj</a>
                                    <asp:LinkButton ID="DeleteApartment" CssClass="button gray reject" runat="server" OnClick="DeleteApartment_Click" OnClientClick="return confirm('Da li si siguran da želiš obrisati apartman?');"><i class="sl sl-icon-close"></i> Izbriši</asp:LinkButton>
                                </div>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblEmptyData" runat="server" CssClass="empty-repeater" Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' Text="Nema pronađenih stavki!" />
                        </FooterTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>

    <uc:Pagination runat="server" ID="Pagination" />
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="Script" runat="server">
    <script>
        (function () {
            $('.dashboard-nav').find('li.active').parent().parent().addClass('active');
        })();
    </script>
</asp:Content>
