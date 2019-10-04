using System;

//namespaces aggiuntivi
using System.Data.OleDb;

namespace Urbinoshop
{
    namespace Carrello
    {
        /// <summary>
        /// Gestione di un record del carrello
        /// </summary>
        public class CarrelloGestione
        {
            private OleDb objOle;
            private OleDbDataReader objRst;
            private System.Collections.ArrayList errore = new System.Collections.ArrayList();

            /// <summary>
            /// Costruttore base
            /// </summary>
            public CarrelloGestione()
            {
                //gestione errori
                try
                {
                    //recupera il path del db dalla var di applicazione
                    string strPath = System.Configuration.ConfigurationManager.AppSettings["dbPath"];
                    string strProvider = "Microsoft.Jet.OLEDB.4.0;";

                    //istanzia l'oggetto che gestisce la connessione al db
                    this.objOle = new OleDb(strPath, strProvider);
                }
                catch (Exception err)
                {
                    //aggiunge l'errore alla collection
                    this.errore.Add(err);
                }

            }//fine costruttore CarrelloGestione

            /// <summary>
            /// Costruttore overload
            /// utile se si dispone di un oggetto OleDb già istanziato
            /// </summary>
            public CarrelloGestione(OleDb objConn)
            {
                //gestione errori
                try
                {

                    //istanzia l'oggetto che gestisce la connessione al db
                    this.objOle = objConn;
                }
                catch (Exception err)
                {
                    //aggiunge l'errore alla collection
                    this.errore.Add(err);
                }
            }//fine costruttore CarrelloGestione overload

            /// <summary>
            /// quando l'istanza della classe viene rilasciata: DISTRUTTORE DI CLASSE
            /// </summary>
            ~CarrelloGestione()
            {
                //svuota la collection
                this.errore.Clear();
            }//fine distruttore

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
            }//fine metodo visualizzaErrori

            /// <summary>
            /// Aggiunge un prodotto al carrello dell'utente
            /// </summary>
            /// <param name="objDettagli"></param>
            /// <returns> 
            /// 1 in caso di corretta esecuzione
            /// -1 in caso di errore
            ///</returns>
            public int prodottoAggiungi(CarrelloDettaglio objDettagli)
            {
                //inizializza il valore da restituire
                int intID = 1;

                //gestione errori
                try
                {
                    //sorgente dati (query parametrica: passa i 3 parametri nell'ordine richiesto)
                    string strSQL = "spAggiungiCarrello " + objDettagli.IDutente + ", "
                                                            + objDettagli.IDprodotto + ", "
                                                            + objDettagli.Quantita;

                    //esegue la query di comando
                    this.objOle.eseguiSql(strSQL);

                }
                catch (Exception err)
                {
                    //valore da restituire
                    intID = -1;

                    //aggiunge l'errore alla collection
                    this.errore.Add(err);
                };

                return intID;
            }//fine metodo prodottoAggiungi

            /// <summary>
            /// modifica i dettagli relativi ad un prodotto del carrello
            /// </summary>
            /// <param name="objDettagli"></param>
            /// <returns> 0 in caso di corretta esecuzione
            ///        -1 in caso di errore
            ///</returns>
            public int prodottoModifica(CarrelloDettaglio objDettagli)
            {
                //inizializza il valore da restituire
                int intID = 0;

                //gestione errori
                try
                {
                    //sorgente dati (query parametrica: passa i 3 parametri nell'ordine richiesto)
                    string strSQL = "spAggiornaCarrello " + objDettagli.Quantita + ", "
                                                                + objDettagli.IDutente + ", "
                                                                + objDettagli.IDprodotto;

                    //esegue la query di comando
                    this.objOle.eseguiSql(strSQL);

                }
                catch (Exception err)
                {
                    //valore da restituire
                    intID = -1;

                    //aggiunge l'errore alla collection
                    this.errore.Add(err);
                };

                return intID;
            }//fine metodo prodottoModifica

            /// <summary>
            /// elimina un prodotto del carrello
            /// </summary>
            /// <param name="objDettagli"></param>
            /// <returns> 0 in caso di corretta esecuzione
            ///        -1 in caso di errore
            ///</returns>
            public int prodottoElimina(CarrelloDettaglio objDettagli)
            {
                //inizializza il valore da restituire
                int intID = 0;

                //gestione errori
                try
                {
                    //sorgente dati (query parametrica: passa i 3 parametri nell'ordine richiesto)
                    string strSQL = "spEliminaCarrello " + objDettagli.IDutente + ", "
                                                         + objDettagli.IDprodotto;

                    //esegue la query di comando
                    this.objOle.eseguiSql(strSQL);

                }
                catch (Exception err)
                {
                    //valore da restituire
                    intID = -1;

                    //aggiunge l'errore alla collection
                    this.errore.Add(err);
                };

                return intID;
            }//fine metodo prodottoElimina


        }//fine classe CarrelloGestione

    }//fine namespace Carrello
}//fine namespace Urbinoshop