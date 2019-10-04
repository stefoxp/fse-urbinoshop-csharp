using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//richiama i namespaces definiti in App_Code
using Urbinoshop.Utente;

public partial class reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //svuota la casella messaggi
        lblMessaggi.Text = "";

        //si tratta di un tentativo di registrazione
        //if (Request["reg"] == "ok")
        if(Page.IsPostBack)
        {
            //esegue la routine
            int int_registra = registraUtente();
            
            switch(int_registra)
            {
                case 0:
                    //msg
                    lblMessaggi.Text = "Registrazione utente effettuata.";
                    break;
                case 1:
                    //msg
                    lblMessaggi.Text = "Dati obbligatori mancanti.";
                    break;
                case 2:
                    //msg
                    lblMessaggi.Text = "Controllo password fallito: le 2 password immesse non coincidono.";
                    break;
                case -1:
                    //msg
                    lblMessaggi.Text = "Registrazione fallita: si &egrave; verificato un errore: ";
                    //Exception err = new Exception();
                    //lblMessaggi.Text += err.Message;
                    break;
                default:
                    //msg
                    lblMessaggi.Text = "Non so";
                    break;
            };
        };
    }//fine Page_Load


    /// <summary>
    /// Richiama gli oggetti che si occupano della registrazione utente
    /// </summary>
    /// <returns>
    ///         int 0 in caso di registrazione effettuata,
    ///         1 in caso di dati mancanti
    ///         2 in caso di verifica pw errata
    ///         -1 in caso di errore
    /// </returns>
    private int registraUtente()
    {
        //dichiarazioni
        UtenteGestione objUser;
	    UtenteDettagli objDettagli;
        int int_risultato = -1;
    
        //recupera i valori passati dalla pagina precedente
        int int_utente_id = 0,
            int_ruolo_id = 0;
       /* string str_user_name = Request["txtUser"],
                str_pw = Request["txtPw"],
                str_pw2 = Request["txtPw2"],
                str_nome = Request["txtNome"],
                str_indirizzo = Request["txtIndirizzo"],
                str_telefono = Request["txtTelefono"],
                str_email = Request["txtEmail"];
        */
        string str_user_name = txtUser.Value,
                str_pw = txtPw.Value,
                str_pw2 = txtPw2.Value,
                str_nome = txtNome.Value,
                str_indirizzo = txtIndirizzo.Value,
                str_telefono = txtTelefono.Value,
                str_email = txtEmail.Value;

         //verifica la presenza dei dati obbligatori
         if ((str_user_name == "") || (str_pw == "") || 
                (str_nome == "") || (str_indirizzo == "") || 
                (str_telefono == "") || (str_email == ""))
        {
                //mancano dati richiesti: restituisce 1
                int_risultato = 1;
        } else {
            //verifica la pw
            if (str_pw != str_pw2)
                //controllo NON superato: restituisce 2
                int_risultato = 2;
        };
        
        // e istanzia l'oggetto Dettagli
        objDettagli = new UtenteDettagli(int_utente_id, 
                                        str_user_name,
                                        str_pw,
                                        int_ruolo_id,
                                        str_nome,
                                        str_indirizzo,
                                        str_telefono,
                                        str_email);     
        

           
    
        //istanzia l'oggetto per il trattamento degli utenti
        objUser = new UtenteGestione();
    
        //controlla che lo UserName sia libero e aggiunge l'utente
        if (objUser.controllaUtente(objDettagli) == 0)
            int_risultato = 0;
    
        return int_risultato;
    }//fine metodo registraUtente
}//fine classe
