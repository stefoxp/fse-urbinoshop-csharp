using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//namespaces aggiuntivi
using System.Data.OleDb;
using Urbinoshop;

public partial class prodotti_dettaglio : System.Web.UI.Page
{
    private string codiceProdotto = "";
    private string nomeProdotto = "";
    private string descrizioneProdotto = "";
    private string fotoProdotto = "";
    private string prezzoProdotto = "";
    private string disponibilitaProdotto = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //recupera il path del db dalla var di applicazione
        string strPath = System.Configuration.ConfigurationManager.AppSettings["dbPath"];
        string strProvider = "Microsoft.Jet.OLEDB.4.0;";

        //istanzia l'oggetto che gestisce la connessione al db
        OleDb objOle = new OleDb(strPath, strProvider);
        OleDbDataReader objRst;

        //recupera il codice e il nome della categoria di appartenenza
        codiceProdotto = Request["IDprodotto"];

        //compone la stringa SQL
        string strSQL = "spProdottoDettagli " + codiceProdotto;

        //gestione errori
        try
        {
            //recupera i dati da db
            objRst = objOle.apriRst(strSQL);
            //assegna la sorgente dati
            //repCatalogo.DataSource = objRst;

            //applica i dati
            //repCatalogo.DataBind();

            while (objRst.Read())
            {
                //string codiceProdotto = objRst.["IDutente"].ToString();
                nomeProdotto = objRst["Nome"].ToString();
                descrizioneProdotto = objRst["Descrizione"].ToString();
                fotoProdotto = objRst["Foto"].ToString();
                prezzoProdotto = objRst["Prezzo"].ToString();
                disponibilitaProdotto = objRst["Disponibilità"].ToString();
            };
            //associa i dati
            //DataBind();

            lblNome.Text = nomeProdotto;
            lblDescrizione.Text = descrizioneProdotto;
            lblDisponibilita.Text = disponibilitaProdotto + " disponibilità";
            lblPrezzo.Text = prezzoProdotto + " euro";
            //setta le proprietà dell'immagine
            imgFoto.ImageUrl = fotoProdotto;
            imgFoto.AlternateText = imgFoto.ToolTip = "Foto di " + nomeProdotto;
            //setta le proprietà del link
            lkCarrello.NavigateUrl = "utente_carrello.aspx?qta=1&azione=Aggiungi&codiceProdotto=" + codiceProdotto;
            lkCarrello.ToolTip = "Aggiungi il prodotto al carrello";
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