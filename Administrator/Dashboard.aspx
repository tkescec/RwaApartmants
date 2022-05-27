<%@ Page Title="Nadzorna ploča" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Administrator.Dashboard" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Nadzorna Ploča</h2>
            </div>
        </div>
    </div>

    <!-- Notice -->
    <%--<div class="row">
        <div class="col-md-12">
            <div class="notification success closeable margin-bottom-30">
                <p>Your listing <strong>Hotel Govendor</strong> has been approved!</p>
                <a class="close" href="#"></a>
            </div>
        </div>
    </div>--%>

    <!-- Content -->
    <div class="row">

        <!-- Item -->
        <div class="col-lg-3 col-md-6">
            <div class="dashboard-stat color-1">
                <div class="dashboard-stat-content">
                    <h4 id="ApartmentCount" runat="server"></h4>
                    <span>Apartmana</span></div>
                <div class="dashboard-stat-icon"><i class="im im-icon-Map2"></i></div>
            </div>
        </div>

        <!-- Item -->
        <div class="col-lg-3 col-md-6">
            <div class="dashboard-stat color-2">
                <div class="dashboard-stat-content">
                    <h4 id="ReservationCount" runat="server"></h4>
                    <span>Rezervacija</span></div>
                <div class="dashboard-stat-icon"><i class="im im-icon-Line-Chart"></i></div>
            </div>
        </div>


        <!-- Item -->
        <div class="col-lg-3 col-md-6">
            <div class="dashboard-stat color-3">
                <div class="dashboard-stat-content">
                    <h4 id="ReviewCount" runat="server"></h4>
                    <span>Recenzija</span></div>
                <div class="dashboard-stat-icon"><i class="im im-icon-Heart"></i></div>
            </div>
        </div>

        <!-- Item -->
        <div class="col-lg-3 col-md-6">
            <div class="dashboard-stat color-4">
                <div class="dashboard-stat-content">
                    <h4 id="UserCount" runat="server"></h4>
                    <span>Korisnika</span></div>
                <div class="dashboard-stat-icon"><i class="im im-icon-Add-UserStar"></i></div>
            </div>
        </div>
    </div>


    <%--<div class="row">

        <!-- Recent Activity -->
        <div class="col-lg-6 col-md-12">
            <div class="dashboard-list-box with-icons margin-top-20">
                <h4>Recent Activities</h4>
                <ul>
                    <li>
                        <i class="list-box-icon sl sl-icon-layers"></i>Your listing <strong><a href="#">Hotel Govendor</a></strong> has been approved!
							<a href="#" class="close-list-item"><i class="fa fa-close"></i></a>
                    </li>

                    <li>
                        <i class="list-box-icon sl sl-icon-star"></i>Kathy Brown left a review
                        <div class="numerical-rating" data-rating="5.0"></div>
                        on <strong><a href="#">Burger House</a></strong>
                        <a href="#" class="close-list-item"><i class="fa fa-close"></i></a>
                    </li>

                    <li>
                        <i class="list-box-icon sl sl-icon-heart"></i>Someone bookmarked your <strong><a href="#">Burger House</a></strong> listing!
							<a href="#" class="close-list-item"><i class="fa fa-close"></i></a>
                    </li>

                    <li>
                        <i class="list-box-icon sl sl-icon-star"></i>Kathy Brown left a review
                        <div class="numerical-rating" data-rating="3.0"></div>
                        on <strong><a href="#">Airport</a></strong>
                        <a href="#" class="close-list-item"><i class="fa fa-close"></i></a>
                    </li>

                    <li>
                        <i class="list-box-icon sl sl-icon-heart"></i>Someone bookmarked your <strong><a href="#">Burger House</a></strong> listing!
							<a href="#" class="close-list-item"><i class="fa fa-close"></i></a>
                    </li>

                    <li>
                        <i class="list-box-icon sl sl-icon-star"></i>John Doe left a review
                        <div class="numerical-rating" data-rating="4.0"></div>
                        on <strong><a href="#">Burger House</a></strong>
                        <a href="#" class="close-list-item"><i class="fa fa-close"></i></a>
                    </li>

                    <li>
                        <i class="list-box-icon sl sl-icon-star"></i>Jack Perry left a review
                        <div class="numerical-rating" data-rating="2.5"></div>
                        on <strong><a href="#">Tom's Restaurant</a></strong>
                        <a href="#" class="close-list-item"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
            </div>
        </div>

        <!-- Invoices -->
        <div class="col-lg-6 col-md-12">
            <div class="dashboard-list-box invoices with-icons margin-top-20">
                <h4>Invoices</h4>
                <ul>

                    <li><i class="list-box-icon sl sl-icon-doc"></i>
                        <strong>Professional Plan</strong>
                        <ul>
                            <li class="unpaid">Unpaid</li>
                            <li>Order: #00124</li>
                            <li>Date: 20/07/2019</li>
                        </ul>
                        <div class="buttons-to-right">
                            <a href="dashboard-invoice.html" class="button gray">View Invoice</a>
                        </div>
                    </li>

                    <li><i class="list-box-icon sl sl-icon-doc"></i>
                        <strong>Extended Plan</strong>
                        <ul>
                            <li class="paid">Paid</li>
                            <li>Order: #00108</li>
                            <li>Date: 14/07/2019</li>
                        </ul>
                        <div class="buttons-to-right">
                            <a href="dashboard-invoice.html" class="button gray">View Invoice</a>
                        </div>
                    </li>

                    <li><i class="list-box-icon sl sl-icon-doc"></i>
                        <strong>Extended Plan</strong>
                        <ul>
                            <li class="paid">Paid</li>
                            <li>Order: #00097</li>
                            <li>Date: 10/07/2019</li>
                        </ul>
                        <div class="buttons-to-right">
                            <a href="dashboard-invoice.html" class="button gray">View Invoice</a>
                        </div>
                    </li>

                    <li><i class="list-box-icon sl sl-icon-doc"></i>
                        <strong>Basic Plan</strong>
                        <ul>
                            <li class="paid">Paid</li>
                            <li>Order: #00091</li>
                            <li>Date: 30/06/2019</li>
                        </ul>
                        <div class="buttons-to-right">
                            <a href="dashboard-invoice.html" class="button gray">View Invoice</a>
                        </div>
                    </li>

                </ul>
            </div>
        </div>
    </div>--%>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="Script" runat="server">
</asp:Content>