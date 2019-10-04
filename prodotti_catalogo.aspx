<%@ Page Title="" Language="C#" MasterPageFile="~/Principale.master" AutoEventWireup="true" CodeFile="prodotti_catalogo.aspx.cs" Inherits="prodotti_catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" Runat="Server">
<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <asp:Repeater ID="repCatalogo" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="2">
        </HeaderTemplate>
        <ItemTemplate>
                <!-- intestazione -->
                <tr class="sfondo">
                    <td colspan="2">
                        <%# DataBinder.Eval(Container.DataItem, "Nome") %> - <%# DataBinder.Eval(Container.DataItem, "Prezzo") %>
                    </td>
                </tr>
                <!-- corpo -->
                <tr>
                    <td width="100" align="center" valign="top">
                        <img src="<%# DataBinder.Eval(Container.DataItem, "Foto") %>" id="imgFoto" width="100"
                            height="100" border="0" alt="Foto di <%# DataBinder.Eval(Container.DataItem, "Nome") %>" />
                    </td>
                    <td valign="top">
                        <p><%# DataBinder.Eval(Container.DataItem, "Descrizione")%></p>
                        <p>
                            <a href="prodotti_dettaglio.aspx?IDprodotto=<%# DataBinder.Eval(Container.DataItem, "IDprodotto") %>">
                                Dettagli &gt;&gt;</a>
                         </p>
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
          </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

