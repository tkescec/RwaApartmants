<%@ Page Title="Tagovi" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Administrator.Tags" %>

<%@ Register Src="~/Controls/Pagination.ascx" TagPrefix="uc" TagName="Pagination" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Tagovi</h2>
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
                            <asp:LinkButton ID="DeleteAllTags" runat="server" CssClass="button gray reject margin-top-5" OnClick="DeleteAllTags_Click" OnClientClick="return confirm('Da li si siguran da želiš obrisati sve ne aktive tagove?');">Izbriši sve ne pridružene tagove</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <h4>Svi tagovi</h4>
                <div class="clearfix"></div>
                <ul>
                    <asp:Repeater ID="rptTags" runat="server">
                        <ItemTemplate>
                            <asp:Label ID="lblApartmentCount" runat="server" Text='<%# ((DAL.Models.Tag)Container.DataItem).ApartmentCount %>' Visible="false" />
                            <asp:Label ID="lblTagId" runat="server" Text='<%# ((DAL.Models.Tag)Container.DataItem).TagID %>' Visible="false" />
                            <li class='<%# ((int)((DAL.Models.Tag)Container.DataItem).ApartmentCount > 0) ? "approved-booking" : "canceled-booking" %>'>
                                <div class="list-box-listing bookings tags">
                                    <div class="list-box-listing-img">
                                        <asp:Image ID="TagImage" runat="server" ImageUrl="<%# ((DAL.Models.Tag)Container.DataItem).Picture %>" />
                                    </div>
                                    <div class="list-box-listing-content">
                                        <div class="inner">
                                            <h3><%# ((DAL.Models.Tag)Container.DataItem).Name %>
                                                <span class="booking-status"><%# ((DAL.Models.Tag)Container.DataItem).ApartmentCount %></span>
                                            </h3>

                                            <div class="inner-booking-list">
                                                <h5>Tip:</h5>
                                                <ul class="booking-list">
                                                    <li class="highlighted"><%# ((DAL.Models.Tag)Container.DataItem).TypeName %></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                               <div class="buttons-to-right">
                                    <asp:LinkButton ID="DeleteApartment" CssClass="button gray reject" runat="server" OnClick="DeleteTag_Click" OnClientClick="return confirm('Da li si siguran da želiš obrisati tag?');"><i class="sl sl-icon-close"></i> Izbriši</asp:LinkButton>
                               </div>
                                
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
    <script>
        (function () {
            $('.dashboard-nav').find('li.active').parent().parent().addClass('active');
        })();
    </script>
</asp:Content>
