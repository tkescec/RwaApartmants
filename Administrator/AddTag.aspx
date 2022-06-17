<%@ Page Title="Novi tag" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddTag.aspx.cs" Inherits="Administrator.AddTag" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Novi Tag</h2>
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
                            <h5>Ime (obavezno) <i class="tip" data-tip-content="Upišite željeno ime taga."></i></h5>
                            <asp:TextBox ID="txtTagName" class="search-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTagName" meta:resourcekey="rfvTagName" runat="server" ControlToValidate="txtTagName" Display="Dynamic" ForeColor="#FF9393">Niste upisali ime taga.</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Ime engleski <i class="tip" data-tip-content="Upišite ime taga na engleskom jeziku radi lakšeg prevođenja."></i></h5>
                            <asp:TextBox ID="txtTagNameEng" class="search-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Tip (obavezno) <i class="tip" data-tip-content="Odaberite tip taga."></i></h5>
                            <asp:DropDownList ID="ddlTagType" runat="server" CssClass="chosen-select-no-single" AutoPostBack="false" AppendDataBoundItems="true">
                                <asp:ListItem Text="Odaberi tip" Value="" />
                            </asp:DropDownList>
							<asp:RequiredFieldValidator ID="rfvTagType" meta:resourcekey="rfvTagType" runat="server" ControlToValidate="ddlTagType" Display="Dynamic" ForeColor="#FF9393">Niste odabrali tip taga.</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <!-- Section / End -->

                <asp:LinkButton ID="btnAddNewTag" CssClass="button preview" OnClick="btnAddNewTag_Click" runat="server">Dodaj novi tag <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>

            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="Script" runat="server">
    <script>
        (function () {
            $('.dashboard-nav').find('li.active').parent().parent().addClass('active');
        })();
    </script>
</asp:Content>
