<%@ Page Title="Rezervacije" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="Administrator.Bookings" %>

<%@ Register Src="~/Controls/Pagination.ascx" TagPrefix="uc" TagName="Pagination" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Rezervacije</h2>
            </div>
        </div>
    </div>

    <div class="row">
        <!-- Listings -->
        <div class="col-lg-12 col-md-12">
            <div class="dashboard-list-box margin-top-0">
                <h4>Sve rezervacije</h4>
                <ul>
                    <asp:Repeater ID="rptReservations" runat="server">
                        <ItemTemplate>
                            <asp:Label ID="lblReservationId" runat="server" Text='<%# ((DAL.Models.Reservation)Container.DataItem).ReservationID %>' Visible="false" />
                            <li class='<%# (((DAL.Models.Reservation)Container.DataItem).UserType == "Gost") ? "pending-booking" : "approved-booking" %>'>
                                <div class="list-box-listing bookings apartments">
                                    <div class="list-box-listing-img">
                                        <asp:Image ID="TagImage" runat="server" ImageUrl="<%# ((DAL.Models.Reservation)Container.DataItem).Picture %>" />
                                    </div>
                                    <div class="list-box-listing-content">
                                        <div class="inner">
                                            <h3><%# ((DAL.Models.Reservation)Container.DataItem).UserName %>
                                                <span class="booking-status"><%# ((DAL.Models.Reservation)Container.DataItem).UserType %></span>
                                            </h3>

                                            <div class="inner-booking-list">
                                                <h5>Apartman:</h5>
                                                <ul class="booking-list">
                                                    <li class="highlighted"><%# ((DAL.Models.Reservation)Container.DataItem).Apartment %></li>
                                                </ul>
                                            </div>

                                            <div class="inner-booking-list">
                                                <h5>Detalji:</h5>
                                                <ul class="booking-list">
                                                    <li class="highlighted"><%# ((DAL.Models.Reservation)Container.DataItem).Details %></li>
                                                </ul>
                                            </div>

                                            <div class="inner-booking-list">
                                                <h5>Info korisnika:</h5>
                                                <ul class="booking-list">
                                                    <li><%# ((DAL.Models.Reservation)Container.DataItem).UserAddress %></li>
                                                    <li><%# ((DAL.Models.Reservation)Container.DataItem).UserEmail %></li>
                                                    <li><%# ((DAL.Models.Reservation)Container.DataItem).UserPhone %></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--<div class="buttons-to-right">
                                    <asp:LinkButton ID="DeleteApartment" CssClass="button gray reject" runat="server" OnClick="DeleteTag_Click" OnClientClick="return confirm('Da li si siguran da želiš obrisati tag?');"><i class="sl sl-icon-close"></i> Izbriši</asp:LinkButton>
                                </div>--%>

                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>

    <uc:Pagination runat="server" ID="Pagination" />

</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="Script" runat="server">
</asp:Content>


