<%@ Page Title="" Language="C#" MasterPageFile="~/Principale.master" AutoEventWireup="true" CodeFile="utente_carrello.aspx.cs" Inherits="utente_carrello" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" Runat="Server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>  
    
      <asp:GridView ID="gvCarrello" runat="server"
                    DataKeyNames="IDprodotto"
                    OnRowUpdating="recordSalva"
                    OnRowEditing="recordModifica"
                    OnRowDeleting="recordElimina"
                    OnRowCancelingEdit="recordAnnulla"
                    Width="100%" 
                    AutoGenerateColumns="false"
                    emptydatatext="Il carrello è vuoto"
                    BorderWidth="0"
                    GridLines="None"
                    cellspacing="2" 
                    ShowFooter="True">
        
        <HeaderStyle BackColor="Navy" ForeColor="LightYellow" />
        <FooterStyle BackColor="Navy" ForeColor="LightYellow" HorizontalAlign="Center" />

         <columns>
            <asp:TemplateField headertext=""
                                ItemStyle-BackColor="LightGray"
                                ItemStyle-HorizontalAlign="Center">
                <itemtemplate>
                    <input type="hidden" id="codiceProdotto" 
                            value="<%# Eval("IDprodotto") %>"
                             />
                    <img src="<%# Eval("Foto") %>"
                         id="imgFoto" width="30" height="30"
                         alt="Foto di <%# Eval("Nome") %>"
                         title="Foto di <%# Eval("Nome") %>" />
                 </itemtemplate>
            </asp:TemplateField>
             <asp:TemplateField headertext="Prodotto"
                                ItemStyle-BackColor="LightGray">
                <itemtemplate>
                    <a href="prodotti_dettaglio.aspx?IDprodotto=<%# Eval("IDprodotto") %>"><%# Eval("Nome") %></a>
                    <br />
                    <%# Eval("Descrizione") %>
            </itemtemplate>
            </asp:TemplateField>
             
             <asp:boundfield datafield="Quantità" 
                                headertext="Quantit&agrave;"
                                ItemStyle-BackColor="LightGray"
                                ItemStyle-HorizontalAlign="Center" />
            
            <asp:TemplateField headertext="Prezzo"
                                ItemStyle-BackColor="LightGray"
                                ItemStyle-HorizontalAlign="Center">
                <itemtemplate>
                    <asp:Label ID="lblPrezzo" runat="server" Text=""><%# Eval("Prezzo") %></asp:Label>
                </itemtemplate>  
            </asp:TemplateField>
            <asp:TemplateField headertext="Parziale spesa"
                                ItemStyle-BackColor="LightGray"
                                ItemStyle-HorizontalAlign="Center">
                <itemtemplate>
                    <asp:Label ID="lblParziale" runat="server" Text="">
                            <%# calcolaParziale(Eval("Prezzo").ToString(), Eval("Quantità").ToString()) %>
                    </asp:Label>
                </itemtemplate>
                <FooterTemplate>
                         <asp:Label runat="server" ID="lblTotaleSpesa" ><%# this.getTotaleSpesa() %> </asp:Label>         
                  </FooterTemplate> 
            </asp:TemplateField>

            <asp:CommandField HeaderText="Azione"
                                ItemStyle-BackColor="LightGray"
                                ItemStyle-HorizontalAlign="Center"
                                ShowDeleteButton="True" 
                                ShowEditButton="True"
                                ButtonType="Button" />
        </columns>
    </asp:GridView>
    <!--
    <asp:Repeater ID="repCarrello" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="2">
                <tr class="sfondo">
		            <td colspan="2">
		                Prodotto
                    </td>
		            <td align="center">
			            Quantit&agrave;
		            </td>
		            <td align="right">
		                Prezzo unitario
		            </td>
                    <td align="right">
		                Parziale spesa
		            </td>
                    <td align="center" class="titolo">Azione</td>
                </tr> 
	            <tr> 
                    <td align="center" colspan="6">
                        <hr />
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
  <table width="100%" border="0" cellspacing="0" cellpadding="3">
    <tr bgcolor="#DCDCDC"> 
        <td width="5%" align="center" bgcolor="#DCDCDC">
            <input type="hidden" id="codiceProdotto" value="<%# Eval("IDprodotto") %>" />
            <img src="<%# Eval("Foto") %>"
                  id="imgFoto" width="30" height="30"
                  alt="Foto di <%# Eval("Nome") %>"
                  title="Foto di <%# Eval("Nome") %>" />
        </td>
        <td width="40%">
             <a href="prodotti_dettaglio.aspx?IDprodotto=<%# Eval("IDprodotto") %>"><%# Eval("Nome") %></a>
             <br />
             <asp:Label ID="lblDescrizione" runat="server" Text=""><%# Eval("Descrizione") %></asp:Label>
        </td>
        <td width="10%" align="center">
            <asp:TextBox ID="txtQuantita"
                        runat="server" 
                        value="" 
                        size="5" maxlength="4"></asp:TextBox>
        </td>
        <td width="10%" align="right"><asp:Label ID="lblPrezzo" runat="server" Text=""><%# Eval("Prezzo") %></asp:Label></td>
        <td width="15%" align="right">
            <asp:Label ID="lblParziale" runat="server" Text="">
                <%# calcolaParziale(Eval("Prezzo").ToString(), Eval("Quantità").ToString()) %>
            </asp:Label>
        </td>
        <td width="20%" align="center">
	        <input runat="server" type="submit" id="cmdS" value="Salva" />
		    &nbsp;
            <input runat="server" type="submit" id="cmdE" value="Elimina" />
        </td>
    </tr>
    </table>
</ItemTemplate>
       <FooterTemplate>
                 <tr>
		            <td colspan="2">&nbsp;</td>
		            <td align="center">
                        <asp:Label ID="lblQtaTotale" runat="server" Text="<%# getTotaleQuantita() %>"></asp:Label>
                    </td>
		            <td>&nbsp;</td>
                    <td align="right">
		                <asp:Label ID="lblSpesaTotale" runat="server" Text="<%# getTotaleSpesa() %>"></asp:Label>
		            </td>
                    <td>&nbsp;</td>
                </tr> 
          </table>
        </FooterTemplate>
    </asp:Repeater>
    -->
</asp:Content>

