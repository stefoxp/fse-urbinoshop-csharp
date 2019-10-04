<%@ Page Title="UrbinoShop - Registrazione utente" Language="C#" AutoEventWireup="true" 
        CodeFile="utente_reg.aspx.cs" Inherits="reg"
        MasterPageFile="~/Principale.master" %>

 <asp:Content ID="contIntestazione" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="contCorpo" ContentPlaceHolderID="Corpo" Runat="Server">
  <table width="90%" border="0" cellpadding="8">
    <tr class="sfondo"> 
      <td colspan="2">
        <strong>Registrazione</strong>
      </td>
      <td>
        <asp:Label id="lblMessaggi" runat="server" text="Registrati!"></asp:Label>
      </td>
    </tr>
    <tr valign="top"> 
      <td align="right">User Name*</td>
      <td colspan="2">
          <input type="text" id="txtUser" runat="server" />
          <br />        
          <font size="-1">* Campi obbligatori</font>
      </td>
    </tr>
    <tr valign="top"> 
      <td align="right">Password*</td>
      <td colspan="2"> 
          <input type="password" id="txtPw" runat="server" />
      </td>
    </tr>
    <tr valign="top"> 
      <td align="right">Conferma Password*</td>
      <td colspan="2">
	        <input type="password" id="txtPw2" runat="server" />
      </td>
    </tr>
    <tr valign="top">
      <td align="right">Nome e Cognome*</td>
      <td colspan="2">
        <input type="text" id="txtNome" size="50" runat="server" />
      </td>
    </tr>
    <tr valign="top">
      <td align="right">Indirizzo*</td>
      <td colspan="2">
        <input type="text" id="txtIndirizzo" size="50" runat="server" />
      </td>
    </tr>
    <tr valign="top"> 
      <td align="right" >Telefono*</td>
      <td colspan="2">
        <input type="text" id="txtTelefono" runat="server" />
      </td>
    </tr>
    <tr valign="top"> 
      <td align="right">Email*</td>
      <td colspan="2">        
            <input type="text" id="txtEmail" runat="server" />
      </td>
    </tr>
    <tr valign="top">
        <td>
            <p>&nbsp; </p>
        </td>
      <td>
        <input name="cmdRegistraInvia" type="submit" value="Registra" runat="server" />
        <input name="butAnnulla" type="reset" value="Annulla" runat="server" />
        <input type="hidden" value="ok" name="reg" runat="server" />
      </td>
    </tr>
  </table>
<!-- 
</form>
-->
</asp:Content>