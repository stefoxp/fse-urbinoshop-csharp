using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//namespaces aggiuntivi
using System.Data.OleDb;
using Urbinoshop;

public partial class prodotti_ricerca : System.Web.UI.Page
{
    //dichiara ed inizializza le proprietà di classe
    private string str_chiave = "";
    //recupera il path del db dalla var di applicazione
    private string strPath = System.Configuration.ConfigurationManager.AppSettings["dbPath"];
    private string strProvider = "Microsoft.Jet.OLEDB.4.0;";
    private System.Collections.ArrayList errore = new System.Collections.ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Gestisce l'evento click del buttone di invio
    /// </summary>
    /// <param name="sender">Button di invio dati form</param>
    /// <param name="e"></param>
    protected void butInvia_click(object sender, EventArgs e)
    {
        //memorizza il valore immesso dall'utente
        str_chiave = txtChiave.Text;

        lblMsg.Text = str_chiave;

        //istanzia l'oggetto che gestisce la connessione al db
        OleDb objOle = new OleDb(strPath, strProvider);
        OleDbDataReader objRst;

        //compone la stringa SQL
        string strSQL = "spProdottoRicerca " + "'%" + str_chiave + "%'";

        //gestione errori
        try
        {
            //recupera i dati da db
            objRst = objOle.apriRst(strSQL);

            //verifica la presenza di record
            if (!objRst.HasRows)
            {
                //compone il messaggio
                string str_errore = "<strong>Spiacenti:</strong> ";
                str_errore += "Nessun prodotto corrisponde ai parametri inseriti.<br />";
                //genera un'eccezione
                throw new Exception(str_errore);
            };

            //assegna la sorgente dati
            repRisultati.DataSource = objRst;

            //applica i dati
            repRisultati.DataBind();

            //visualizza il repeater con i dati
            repRisultati.Visible = true;
        }
        catch (Exception err)
        {
            //aggiunge l'errore alla collection
            this.errore.Add(err);

            //nasconde il repeater con i dati
            repRisultati.Visible = false;
        }
        finally
        {
            //visualizza eventuali errori
            this.lblMsg.Text = visualizzaErrori();
        };    
    }

    /// <summary>
    /// Raccoglie i messaggi di errore in una stringa
    /// </summary>
    /// <returns>Una stringa che elenca i messaggi di errore</returns>
    public string visualizzaErrori()
    {
        string str = "";

        foreach (Exception e in this.errore)
        {
            str += "Messaggio: " + e.Message + "<br/>";
        }

        return str;
    }

    
}