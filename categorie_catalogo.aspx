<%@ Page Title="" Language="C#" MasterPageFile="~/Principale.master" AutoEventWireup="true"
    CodeFile="categorie_catalogo.aspx.cs" Inherits="categorie_catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="Server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <asp:Repeater ID="repCatalogo" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="2">
        </HeaderTemplate>
        <ItemTemplate>
                <!-- intestazione -->
                <tr class="sfondo">
                    <td colspan="2">
                        <%# DataBinder.Eval(Container.DataItem, "Nome") %>
                    </td>
                </tr>
                <!-- corpo -->
                <tr>
                    <td width="100" align="center" valign="top">
                        <img src="<%# DataBinder.Eval(Container.DataItem, "Foto") %>" id="imgFoto" width="100"
                            height="100" border="0" alt="Foto di <%# DataBinder.Eval(Container.DataItem, "Nome") %>" />
                    </td>
                    <td valign="top">
                        <p>
                            <%# DataBinder.Eval(Container.DataItem, "Descrizione")%></p>
                        <p>
                            <a href="prodotti_catalogo.aspx?IDcategoria=<%# DataBinder.Eval(Container.DataItem, "IDcategoria") %>&nomeCategoria=<%# DataBinder.Eval(Container.DataItem, "Nome") %>">
                                Visualizza Prodotti &gt;&gt;</a></p>
                    </td>
                </tr>
            
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
