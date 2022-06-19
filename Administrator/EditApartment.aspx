<%@ Page Title="Uređivanje apartmana" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="EditApartment.aspx.cs" Inherits="Administrator.EditApartment" %>

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
        <div class="col-lg-12">
            <div id="add-listing">
                <!-- Section -->
                <div class="add-listing-section">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-doc"></i>Osnovne Informacije</h3>
                    </div>

                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Vlasnik (obavezno) <i class="tip" data-tip-content="Odaberite jednog od vlasnika apartmana."></i></h5>
                            <asp:DropDownList ID="ddlOwner" runat="server" CssClass="chosen-select-no-single" AutoPostBack="false" AppendDataBoundItems="true">
                                <asp:ListItem Text="Odaberi vlasnika" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvOwner" meta:resourcekey="rfvOwner" runat="server" ControlToValidate="ddlOwner" Display="Dynamic" ForeColor="#FF9393">Niste odabrali vlasnika.</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Ime (obavezno) <i class="tip" data-tip-content="Upišite željeno ime apartmana."></i></h5>
                            <asp:TextBox ID="txtApartmentName" class="search-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApartmentName" meta:resourcekey="rfvApartmentName" runat="server" ControlToValidate="txtApartmentName" Display="Dynamic" ForeColor="#FF9393">Niste upisali ime apartmana.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <h5>Ime engleski (obavezno) <i class="tip" data-tip-content="Upišite ime apartmana na engleskom jeziku radi lakšeg prevođenja."></i></h5>
                            <asp:TextBox ID="txtApartmentNameEng" class="search-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApartmentNameEng" meta:resourcekey="rfvApartmentNameEng" runat="server" ControlToValidate="txtApartmentNameEng" Display="Dynamic" ForeColor="#FF9393">Niste upisali ime apartmana na engleskom.</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-location"></i>Lokacija</h3>
                    </div>

                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Grad (obavezno) <i class="tip" data-tip-content="Odaberite grad u kojem se nalazi apartman."></i></h5>
                            <asp:DropDownList ID="ddlApartmentCity" runat="server" CssClass="chosen-select-no-single" AutoPostBack="false" AppendDataBoundItems="true">
                                <asp:ListItem Text="Odaberi grad" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCity" meta:resourcekey="rfvCity" runat="server" ControlToValidate="ddlApartmentCity" Display="Dynamic" ForeColor="#FF9393">Niste odabrali grad.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <h5>Adresa <i class="tip" data-tip-content="Upišite adresu apartmana."></i></h5>
                            <asp:TextBox ID="txtApartmentAddress" class="search-field" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvApartmentAddress" meta:resourcekey="rfvApartmentAddress" runat="server" ControlToValidate="txtApartmentAddress" Display="Dynamic" ForeColor="#FF9393">Niste upisali ime apartmana.</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-picture"></i>Galerija slika</h3>
                    </div>
                    <!-- Dropzone -->
                    <div class="file-input-container">
                        <div class="file-input">
                            <asp:FileUpload ID="fileApartmentImages" AllowMultiple="true" runat="server" CssClass="files-input" data-multiple-caption="Broj odabranih slika ({count})" />
                            <label class="label-input text-center" for="fileApartmentImages">
                                <i class="sl sl-icon-plus"></i>
                                <br>
                                <span>Povuci i ispusti fotografije ili odaberi sa računala</span>
                                <br>
                                <span class="files-info text-danger"></span>
                            </label>
                        </div>
                    </div>
                    <div class="images-preview-container row"></div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-docs"></i>Detalji</h3>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Broj soba <i class="tip" data-tip-content="Upišite koliko apartman ima soba."></i></h5>
                            <asp:TextBox ID="txtApartmentRooms" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <h5>Broj odraslih osoba <i class="tip" data-tip-content="Upišite koliko odraslih osoba prima apartman."></i></h5>
                            <asp:TextBox ID="txtApartmentAdults" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Broj djece <i class="tip" data-tip-content="Upišite koliko djece prima apartman."></i></h5>
                            <asp:TextBox ID="txtApartmentChilds" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <h5>Udaljenost od plaže <i class="tip" data-tip-content="Upišite koliko je apartman udaljen od plaže u metrima."></i></h5>
                            <asp:TextBox ID="txtApartmentBeachDistance" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Tagovi <i class="tip" data-tip-content="Odaberite željene tagove za apartman"></i></h5>
                            <div class="checkboxes in-row margin-bottom-20">
                                <asp:CheckBoxList ID="cblApartmentTags" runat="server" AppendDataBoundItems="True" RepeatLayout="UnorderedList"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-book-open"></i>Cijena</h3>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Cijena (obavezno) <i class="tip" data-tip-content="Upišite cijenu apartmana u eurima."></i></h5>
                            <div class="fm-input pricing-price">
                                <asp:TextBox ID="txtApartmentPrice" class="search-field" runat="server" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" type="currency" data-unit="EUR"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvApartmentPrice" meta:resourcekey="rfvApartmentPrice" runat="server" ControlToValidate="txtApartmentPrice" Display="Dynamic" ForeColor="#FF9393">Niste upisali cijenu.</asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- Section / End -->
            </div>

            <asp:LinkButton ID="btnUpdateApartment" CssClass="button preview" OnClick="btnUpdateApartment_Click" runat="server">Dodaj novi apartman <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>

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
