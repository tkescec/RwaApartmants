<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Administrator.Default" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - Listeo Apartmants</title>

    <webopt:BundleReference runat="server" Path="~/Content/css" />

</head>
<body>
    <div class="admin-login-page">
        <div class="login-cover">
            <div class="login-cover-img"></div>
            <div class="login-cover-bg"></div>
        </div>
        <form id="loginForm" runat="server" style="min-width: 100%">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">
                        <!-- PANEL PORUKA -->
                        <asp:Panel ID="PanelIspis" runat="server" Visible="False">
                            <div class='notification error closeable'>
                                <asp:Label ID="lblErrorLogin" meta:resourcekey="lblErrorLogin" runat="server" Text="Neispravno korisničko ime i/ili zaporka"></asp:Label>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <!-- // -->
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">
                        <asp:Panel ID="PanelForma" runat="server" Visible="True">
                            <!-- FORM -->
                            <fieldset>
                                <legend class="legend-title margin-bottom-15" runat="server" meta:resourcekey="legendLogin">Prijava Administratora</legend>
                                <div class="margin-bottom-15">
                                    <asp:Label ID="lblUsername" meta:resourcekey="lblUsername" class="form-label" runat="server" Text="Korisničko ime"></asp:Label>
                                    <asp:TextBox ID="txtUsername" class="input-text margin-bottom-0" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" meta:resourcekey="rfvUsername" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ForeColor="#FF9393">Niste upisali korisničko ime</asp:RequiredFieldValidator>
                                </div>
                                <div class="margin-bottom-15">
                                    <asp:Label ID="lblPassword" meta:resourcekey="lblPassword" class="form-label" runat="server" Text="Zaporka"></asp:Label>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" class="input-text margin-bottom-0" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" meta:resourcekey="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="#FF9393">Niste upisali zaporku</asp:RequiredFieldValidator>
                                </div>
                                <asp:Button ID="btnLogin" meta:resourcekey="btnLogin" class="btn btn-primary" runat="server" Text="Prijavi se" OnClick="btnLogin_Click" />
                            </fieldset>
                            <!-- // -->
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
