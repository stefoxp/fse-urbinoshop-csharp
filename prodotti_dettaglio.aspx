<%@ Page Title="" Language="C#" MasterPageFile="~/Principale.master" AutoEventWireup="true" CodeFile="prodotti_dettaglio.aspx.cs" Inherits="prodotti_dettaglio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" Runat="Server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
   <table width="90%" border="0" cellspacing="3" cellpadding="2"> 
      <tr valign="top" class="sfondo"> 
        <td colspan="2">
             <asp:Label id="lblNome" runat="server" Text="Nome"></asp:Label>
        </td>
      </tr>
      <tr valign="top">
        <td width="150" rowspan="6">
            <asp:Image id="imgFoto" runat="server" width="150" height="180" border="0" />
        </td>
      </tr>
      <tr> 
        <td>
            <asp:Label id="lblDescrizione" runat="server" Text="Descrizione"></asp:Label>
        </td>
      </tr>
      <tr> 
        <td>
            <asp:Label id="lblPrezzo" runat="server" Text="Prezzo"></asp:Label> &nbsp;- 
            <asp:Label id="lblDisponibilita" runat="server" Text="Disponibilità"></asp:Label> &nbsp;- 
            <asp:HyperLink id="lkCarrello" runat="server">Aggiungi al carrello</asp:HyperLink>
        </td>
      </tr>
      <tr> 
        <td align="center">
            <strong>Recensioni prodotto
            <br /> * * * * *</strong>
        </td>
      </tr>
      <tr> 
        <td>
          Testo di esempio Testo di esempio Testo di esempio Testo di esempio
          Testo di esempio Testo di esempio Testo di esempio Testo di esempio
          Testo di esempio Testo di esempio Testo di esempio Testo di esempio
          Testo di esempio Testo di esempio Testo di esempio Testo di esempio
          Testo di esempio Testo di esempio
        </td>
      </tr>
      <tr> 
        <td align="center">
            <strong>* * * * *</strong>
        </td>
      </tr>
      <tr class="sfondo"> 
        <td colspan="2">
		    <p>&nbsp;</p>
        </td>
      </tr>
</table>
</asp:Content>

