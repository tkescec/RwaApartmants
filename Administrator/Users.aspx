<%@ Page Title="Korisnici" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Administrator.Users" %>

<%@ Register Src="~/Controls/Pagination.ascx" TagPrefix="uc" TagName="Pagination" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Korisnici</h2>
            </div>
        </div>
    </div>

    <div class="row">
        <!-- Listings -->
        <div class="col-lg-12 col-md-12">
            <div class="dashboard-list-box margin-top-0">
                <h4>Svi korisnici</h4>

                <ul>
                    <asp:Repeater ID="rptUsers" runat="server">
                        <ItemTemplate>
                            <li class=<%# (!(bool)((DAL.Models.User)Container.DataItem).EmailConfirmed || ((DAL.Models.User)Container.DataItem).DeletedAt != null) ? "canceled-booking" : "approved-booking" %>>
                                <div class="list-box-listing bookings">
                                    <div class="list-box-listing-img">
                                        <img src="http://www.gravatar.com/avatar/00000000000000000000000000000000?d=mm&s=120" alt=""></div>
                                    <div class="list-box-listing-content">
                                        <div class="inner">
                                            <h3><%# ((DAL.Models.User)Container.DataItem).Username %> <span class="booking-status"><%# (!(bool)Eval(nameof(DAL.Models.User.EmailConfirmed)) || Eval(nameof(DAL.Models.User.DeletedAt)) != null) ? "Neaktivan" : "Aktivan" %></span></h3>

                                            <div class="inner-booking-list">
                                                <h5>Korisnička uloga:</h5>
                                                <ul class="booking-list">
                                                    <li class="highlighted"><%# ((DAL.Models.User)Container.DataItem).Role.ToString() != "" ? ((DAL.Models.User)Container.DataItem).Role : "Nema uloge" %></li>
                                                </ul>
                                            </div>

                                            <%--<div class="inner-booking-list">
                                                <h5>Kontakt:</h5>
                                                <ul class="booking-list">
                                                    <li>kathy@example.com</li>
                                                    <li>123-456-789</li>
                                                </ul>
                                            </div>--%>

                                            <%--<a href="#small-dialog" class="rate-review popup-with-zoom-anim"><i class="sl sl-icon-envelope-open"></i>Send Message</a>--%>

                                        </div>
                                    </div>
                                </div>
                                <%--<div class="buttons-to-right">
                                    <a href="#" class="button gray reject"><i class="sl sl-icon-close"></i>Izbriši</a>
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
