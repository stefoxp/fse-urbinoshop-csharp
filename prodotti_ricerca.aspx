<%@ Page Title="" Language="C#" MasterPageFile="~/Principale.master" AutoEventWireup="true" CodeFile="prodotti_ricerca.aspx.cs" Inherits="prodotti_ricerca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" Runat="Server">
<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
<table width="90%" border="0" cellspacing="0" cellpadding="1">
    <tr> 
      <td> 
        <table width="100%" border="0" cellspacing="0" cellpadding="4">
          <tr class="sfondo">
            <td colspan="2">
                <b>Ricerca prodotto </b>
            </td>
          </tr>
          <tr> 
            <td align="right">
                Prodotto:
            </td>
            <td>
                <asp:TextBox ID="txtChiave" runat="server" size="80"></asp:TextBox>
            </td>
          </tr>
          <tr> 
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="butInvia" runat="server" Text="Avvia ricerca" 
                    onclick="butInvia_click" />
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
    <asp:Repeater ID="repRisultati" runat="server" Visible="False">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="2">
        </HeaderTemplate>
        <ItemTemplate>
                <!-- intestazione -->
                <tr class="sfondo">
                    <td colspan="2">
                        <%# DataBinder.Eval(Container.DataItem, "Prodotto") %> - <%# DataBinder.Eval(Container.DataItem, "Prezzo") %>
                    </td>
                </tr>
                <!-- corpo -->
                <tr>
                    <td width="100" align="center" valign="top">
                        <img src="<%# DataBinder.Eval(Container.DataItem, "Foto") %>" name="image" width="100"
                            height="100" border="0" alt="Foto di <%# DataBinder.Eval(Container.DataItem, "Prodotto") %>">
                    </td>
                    <td valign="top">
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

