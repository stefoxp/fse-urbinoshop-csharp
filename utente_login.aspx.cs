using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//richiama i namespaces definiti in App_Code
using Urbinoshop.Utente;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //svuota la casella messaggi
        lblMessaggi.Text = "";

        //se il form non è stato inviato
        if (!Page.IsPostBack)
        {
            //comando ricevuto
            string str_cmd = Request["log"];

            //verifica la presenza del valore di controllo
            switch (str_cmd)
            {
                case "out":
                    //termina la sessione corrente svuotandola
                    Session.Abandon();
                    lblMessaggi.Text += "Logout effettuato";
                    break;
                default:
                    //recupera l'ID utente
                    int utente_id = (int)Session["IDutente"];

                    //verifica lo stato di log
                    if (utente_id != 0)
                    {
                        //l'utente è già loggato: lo informa
                        lblMessaggi.Text += "<br/><strong>Utente connesso</strong>: " + Session["nomeUtente"];
                    }
                    else
                    {
                        //l'utente non è loggato: lo informa
                        lblMessaggi.Text += "<br/><strong>Utente disconnesso</strong>";
                    };
                    break;
            };
        };
    }

    /// <summary>
    /// Gestore evento ServerClick del pulsante Invia
    /// </summary>
    /// <param name="sender">Identifica l'oggetto che ha scatenato l'evento</param>
    /// <param name="e">Lista di argomenti</param>
    protected void invia(object sender, EventArgs e)
    {
        //esegue la routine per il login
        eseguiLogin();
    }

    /// <summary>
    /// Gestisce il login
    /// </summary>
    private void eseguiLogin() {
        
        //dichiarazioni
        int intID = 0;

        UtenteGestione objUser;
        //recupera i valori dal form
        string strUser = txtUserName.Value,
                strPw = txtPassword.Value;

        //svuota la casella messaggi
        lblMessaggi.Text = "";
       
        //verifica la presenza dei dati obbligatori
        //UserName
        if (!(strUser == "")) {
           //Password
            if (!(strPw == "")) {
                objUser = new UtenteGestione();
            
                //recupera il valore prodotto dal metodo login
                intID = objUser.Login(strUser, strPw);

                //verifica che sia maggiore di 0
                if (intID > 0) {
                    //variabile di sessione utilizzata per l'autenticazione dell'utente
                    Session["IDutente"] = intID;
                    //memorizza il nome dell'utente
                    Session["nomeUtente"] = strUser;
                    //conferma per l'utente
                    lblMessaggi.Text += "<br/>Login riuscito.";
                    lblMessaggi.Text += "<br/><h3>Benvenuto " + strUser + "</h3>";
                    //allunga il timeout per la sessione
                    Session.Timeout = 60;
                } else {
                    //messaggio informativo
                    lblMessaggi.Text += "<br/>ID utente = " + intID + " errore: " + objUser.visualizzaErrori();
                
                    lblMessaggi.Text += "<br/>Accesso negato<br />";
                    lblMessaggi.Text += "<br/>UserName o Password errati<br />";
                };
            } else {
                //informa l'utente
                lblMessaggi.Text += "<br/>Password mancante !";
            };
        } else {
            //informa l'utente
            lblMessaggi.Text += "<br/>UserName mancante !";
        };
    }//fine metodo eseguiLogin
}//fine classe

