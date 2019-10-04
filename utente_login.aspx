<%@ Page Title="UrbinoShop - Login" Language="C#" AutoEventWireup="true" 
        CodeFile="utente_login.aspx.cs" Inherits="login"
        MasterPageFile="~/Principale.master" %>

<asp:Content ID="contIntestazione" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="contCorpo" ContentPlaceHolderID="Corpo" Runat="Server">
    <table>
    <tr>
        <td>
            <p>Se non sei registrato puoi <a href="utente_reg.aspx">registrarti</a> ora: &egrave; una procedura semplice e veloce. </p>
            <h4>La procedura di login vi permetterà di procedere all'acquisto
            dei prodotti desiderati.</h4>
        </td>
    </tr>
    <tr>
        <td>
            <p>
                <asp:Label id="lblMessaggi" runat="server" text="Utente disconnesso!"></asp:Label>
            </p>
        </td>
    </tr>
    <tr>
    <td>
            <table>
                <tr>
                    <td>UserName:</td>
                    <td>
                        <input id="txtUserName" type="text" runat="server" />
                    </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <input id="txtPassword" type="text" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <input type="button" id="butLogin" runat="server" value="Invia" onserverclick="invia" />
                </td>
            </tr>
            </table>
    </td>
    </tr>
</table>
</asp:Content>
