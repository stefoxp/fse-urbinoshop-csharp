using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//namespaces aggiuntivi
using System.Data.OleDb;
using Urbinoshop;
using Urbinoshop.Carrello;

public partial class utente_carrello : System.Web.UI.Page
{
    //ID utente
    private int utente_id = 0;

    private string totaleQuantita;
    private string totaleSpesa;

    private System.Collections.ArrayList errore = new System.Collections.ArrayList();

    //recupera il path del db dalla var di applicazione
    private static string strPath = System.Configuration.ConfigurationManager.AppSettings["dbPath"];
    private const string strProvider = "Microsoft.Jet.OLEDB.4.0;";
    //istanzia l'oggetto che gestisce la connessione al db
    private OleDb objOle = new OleDb(strPath, strProvider);

    /// <summary>
    /// Gestisce il primo accesso alla pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //recupera l'ID utente
        utente_id = (int)Session["IDutente"];
        //recupera l'eventuale azione
        string strAzione = Request["azione"];

        //verifica la presenza di un id utente valido
        if(!(utente_id > 0))
            //redireziona l'utente sulla pagina di login
            Response.Redirect("utente_login.aspx");

        //controlla che non sia stato inviato un comando
        if (!Page.IsPostBack)
        {
            //verifica se si tratta di una richiesta di modifica del carrello
            if (string.IsNullOrEmpty(strAzione))
            {
                //associa
                gridAssocia(-1);

                //this.lblTest.Text = "No Post 1";
            }
            else
            {
                //this.lblTest.Text = "No Post 2";

                //recupera gli altri valori necessari
                string strQta = Request["qta"];
                string prodottoCodice = Request["codiceProdotto"];

                //esegue l'azione desiderata
                int risultato = this.carrelloAggiorna(strAzione,
                                    Convert.ToInt32(prodottoCodice),
                                    Convert.ToInt32(strQta));

                //riassocia la grid per visualizzare il cambio di stato
                gridAssocia(-1);

                //verifica: DA RIMUOVERE
                this.lblMsg.Text += "<br/>Risultato: " + risultato;
            };
            
        }
        else
        {
            //this.lblTest.Text = "Post";
        };
    }//fine Page_Load

    /// <summary>
    /// Riassocia la GridView
    /// </summary>
    /// <param name="intIndice">
    /// Indice della riga da modificare
    /// -1 per uscire dalla modalità di modifica
    /// </param>
    protected void gridAssocia(int intIndice = -1)
    {
        //recupera il path del db dalla var di applicazione
        //string strPath = System.Configuration.ConfigurationManager.AppSettings["dbPath"];
        //string strProvider = "Microsoft.Jet.OLEDB.4.0;";
        //istanzia l'oggetto che gestisce la connessione al db
        //OleDb objOle = new OleDb(strPath, strProvider);
        OleDbDataReader objRst;
        OleDbDataReader objRstTot;

        //compone la stringa SQL
        string strSQL = "spVisualizzaCarrello " + utente_id + "";

        //gestione errori
        try
        {
            //recupera i dati da db
            objRst = objOle.apriRst(strSQL);

            //verifica la presenza di record
            if (!objRst.HasRows)
            {
                //compone il messaggio
                string str_errore = "Il carrello è vuoto.<br />";
                //genera un'eccezione
                throw new Exception(str_errore);
            };

            //assegna la sorgente dati: è necessario poi un DataBind
            gvCarrello.DataSource = objRst;

            //compone la stringa SQL
            strSQL = "spTotaliCarrello " + utente_id + "";

            //recupera i dati da db
            objRstTot = objOle.apriRst(strSQL);

            //verifica la presenza di record
            if (!objRstTot.HasRows)
            {
                //compone il messaggio
                string str_errore = "Errore su calcolo totali<br />";
                
                //genera un'eccezione
                throw new Exception(str_errore);
            }
            else
            {
                //legge il record
                objRstTot.Read();

                //recupera i valori
                this.setTotaleQuantita(objRstTot["TotaleQuantità"].ToString());
                this.setTotaleSpesa(objRstTot["TotaleSpesa"].ToString());
            };

            //imposta l'indice del record da modificare o esce dalla modalità di modifica (-1)
            //N.B. va impostato prima di DataBind
            gvCarrello.EditIndex = intIndice;

            //applica i dati (N.B. va fatto alla fine per poter recuperare altri dati)
            gvCarrello.DataBind();

            objRstTot.Close();
            objRst.Close();
        }
        catch (Exception err)
        {
            //aggiunge l'errore alla collection
            this.errore.Add(err);
        }
        finally
        {
            //visualizza eventuali errori
            this.lblMsg.Text = visualizzaErrori();
        };
    }
  
    /// <summary>
    /// Gestisce l'evento Annulla del gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void recordAnnulla(object sender, GridViewCancelEditEventArgs e)
    {      
        //riassocia la grid per visualizzare il cambio di stato
        gridAssocia(-1);

        //verifica: da rimuovere
        //this.lblMsg.Text = "Annulla";
    }

    /// <summary>
    /// Gestisce l'evento Editing del gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void recordModifica(object sender, GridViewEditEventArgs e)
    {
        //riassocia la grid per visualizzare il cambio di stato
        gridAssocia(e.NewEditIndex);

        //verifica: da rimuovere
        //this.lblMsg.Text = "Modifica";
    }//fine metodo modificaRecord

    /// <summary>
    /// Gestisce l'evento Update del gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void recordSalva(object sender, GridViewUpdateEventArgs e)
    {
        //recupera i dati necessari alla modifica
        string codiceProdotto = e.Keys["IDprodotto"].ToString();
        string qta = e.NewValues["Quantità"].ToString();
        // e.Keys["Quantità"].ToString(); Non va bene perchè non varia su modifica 

        //richiama il metodo per il salvataggio
        int risultato = this.carrelloAggiorna("Salva", 
                                                Convert.ToInt32(codiceProdotto), 
                                                Convert.ToInt32(qta));

        //riassocia la grid per visualizzare i dati aggiornati
        gridAssocia(-1);

        //verifica: DA RIMUOVERE
        //this.lblMsg.Text += "<br/>Risultato: " + risultato;
    }//fine recordSalva

    /// <summary>
    /// Gestisce l'evento Delete del gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void recordElimina(object sender, GridViewDeleteEventArgs e)
    {
        //recupera i dati necessari per l'eliminazione
        string codiceProdotto = e.Keys["IDprodotto"].ToString();

        //richiama il metodo per il salvataggio
        int risultato = this.carrelloAggiorna("Elimina",
                                                Convert.ToInt32(codiceProdotto));

        //riassocia la grid per visualizzare il cambio di stato
        gridAssocia(-1);

        //verifica: DA RIMUOVERE
        //this.lblMsg.Text = "Elimina codice prodotto: " + codiceProdotto;
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

    /// <summary>
    /// Gestisce i comandi di aggiornamento db
    /// </summary>
    /// <param name="azione">
    /// "Aggiungi"           
    /// "Salva"
    /// "Elimina"
    /// </param>
    /// <param name="prodotto_id">Codice identificativo prodotto</param>
    /// <param name="prodotto_qta">Quantità prodotto</param>
    /// <returns>0 in caso di corretta esecuzione
    ///        -1 in caso di errore</returns>
  private int carrelloAggiorna(string azione, int prodotto_id, int prodotto_qta = 1)
  {
    //dichiarazioni
    string strCmd = azione;
    int intRisultato = 0;

    //istanzia l'oggetto per la mem dei dettagli
    CarrelloDettaglio objDettagliCarrello = new CarrelloDettaglio();
    //utilizza l'istanza già creata per gestire la connessione Oledb
    CarrelloGestione objCarrello = new CarrelloGestione(objOle);

    //gestione errori
    try
    {
        //recupera i valori passati dalla pag chiamante
        objDettagliCarrello.IDutente = this.utente_id;
        objDettagliCarrello.IDprodotto = prodotto_id;
        objDettagliCarrello.Quantita = prodotto_qta;
    
        //verifica la presenza dei valori richiesti
        if (objDettagliCarrello.verificaDati() != 0)
        {
            //genera un'eccezione
            throw new Exception("I dati forniti non sono sufficienti per procedere con la modifica del carrello.");
        };
    
        switch (strCmd)
        {
            case "Aggiungi":           
                //aggiunge un prodotto al carrello
                intRisultato = objCarrello.prodottoAggiungi(objDettagliCarrello);
                break;
            case "Salva":
                //modifica un prodtto del carrello
                intRisultato = objCarrello.prodottoModifica(objDettagliCarrello);
                break;
            case "Elimina":
                //elimina un prodotto del carrello
                intRisultato = objCarrello.prodottoElimina(objDettagliCarrello);
                break;
            default:
                //nulla
                break;
        };
    }
    catch (Exception err)
    {
        //aggiunge l'errore alla collection
        this.errore.Add(err);

        //imposta il valore da restituire
        intRisultato = -1;
    }
    finally
    {
        //visualizza eventuali errori
        this.lblMsg.Text = visualizzaErrori();
    };

    return intRisultato;
    
  }//fine metodo carrelloAggiorna

    /// <summary>
    /// Calcoli i parziali di riga
    /// </summary>
    /// <returns>Decimal</returns>
    public Decimal calcolaParziale(string prezzoProdotto, string quantitaProdotto)
    {
        Decimal dec_risultato = 0;

        Decimal dec_prezzoProdotto = Convert.ToDecimal(prezzoProdotto);
        Int16 int_quantitaProdotto = Convert.ToInt16(quantitaProdotto);
        dec_risultato = (Decimal)(dec_prezzoProdotto * int_quantitaProdotto);

        return dec_risultato;
    }

    /// <summary>
    /// Imposta la proprietà di classe
    /// </summary>
    /// <param name="totQta">string</param>
    private void setTotaleQuantita(string totQta)
    {
        this.totaleQuantita = totQta;
    }

    /// <summary>
    /// Restituisce la proprietà di classe
    /// </summary>
    /// <returns></returns>
    public string getTotaleQuantita()
    {
        return this.totaleQuantita;
    }

    /// <summary>
    /// Imposta la proprietà di classe
    /// </summary>
    /// <param name="totSpesa">string</param>
    private void setTotaleSpesa(string totSpesa)
    {
        this.totaleSpesa = totSpesa;
    }//fine setTotaleSpesa

    /// <summary>
    /// Restituisce la proprietà di classe
    /// </summary>
    /// <returns></returns>
    public string getTotaleSpesa()
    {
        return this.totaleSpesa;
    }//fine getTotaleSpesa
}//fine classe