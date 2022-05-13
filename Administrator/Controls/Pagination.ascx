<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagination.ascx.cs" Inherits="Administrator.Controls.Pagination" %>

<!-- Pagination -->
<div class="clearfix"></div>
<div class="row">
    <div class="col-md-12">
        <!-- Pagination -->
        <div class="pagination-container margin-top-20 margin-bottom-40">
            <nav class="pagination">
                <ul>
                    <asp:Repeater ID="rptPagination" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="lnkPage" OnClick="BtnPage_Click" CommandName="Page" CommandArgument="<%# Container.DataItem %>" runat="server"><%# Container.DataItem %>  
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>
    </div>
</div>
<!-- Pagination / End -->
