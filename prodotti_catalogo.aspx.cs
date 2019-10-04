using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//namespaces aggiuntivi
using System.Data.OleDb;
using Urbinoshop;

public partial class prodotti_catalogo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //recupera il path del db dalla var di applicazione
        string strPath = System.Configuration.ConfigurationManager.AppSettings["dbPath"];
        string strProvider = "Microsoft.Jet.OLEDB.4.0;";

        //istanzia l'oggetto che gestisce la connessione al db
        OleDb objOle = new OleDb(strPath, strProvider);
        OleDbDataReader objRst;

        //recupera il codice e il nome della categoria di appartenenza
        string codiceCategoria = Request["IDcategoria"];
        string nomeCategoria = Request["nomeCategoria"];

        //compone la stringa SQL
        string strSQL = "spProdottoPerCategoria " + codiceCategoria;

        //gestione errori
        try
        {
            //recupera i dati da db
            objRst = objOle.apriRst(strSQL);
            //assegna la sorgente dati
            repCatalogo.DataSource = objRst;

            //applica i dati
            repCatalogo.DataBind();
        }
        catch (Exception err)
        {
            //aggiunge l'errore alla collection
            this.errore.Add(err);
        };

        //visualizza eventuali errori
        this.lblMsg.Text = visualizzaErrori();
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
            str += "Sorgente: " + e.Source + " Messaggio: " + e.Message + "<br/>";
        }

        return str;
    }

    private System.Collections.ArrayList errore = new System.Collections.ArrayList();
}