using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace Urbinoshop
{
    /// <summary>
    /// Descrizione di riepilogo per OleDb
    /// </summary>
    public class OleDb
    {
        //membri pubblici

        /// <summary>
        /// Costruttore
        /// </summary>
        public OleDb(string str_path, string str_provider)
        {
            ///TODO
            ///Sostituire con metodi set che controllino i dati forniti
            this.db_path = str_path;
            this.db_provider = str_provider;
            this.errore = new System.Collections.ArrayList();

            try
            {
                //istanzia la connessione
                this.Conn = new OleDbConnection("Provider=" + this.db_provider +
                                                "Data Source=" + this.db_path);
                //apre la connessione
                this.Conn.Open();
            }
            catch (Exception err)
            {
                //aggiunge l'errore alla collection
                this.errore.Add(err);
            }
        }

        /// <summary>
        /// Distruttore
        /// </summary>
        ~OleDb()
        {
            //chiude la connessione
            //this.Conn.Close();
            //svuota la collection
            this.errore.Clear();
        }

        /// <summary>
        /// accede ad una tabella e restituisce un DataReader
        /// </summary>
        /// <param name="query">Stringa SQL</param>
        /// <returns>OleDbDataReader dati da visualizzare</returns>
        public OleDbDataReader apriRst(string query)
        {
            //istanzia l'oggetto DataReader da restituire al chiamante
            OleDbCommand objCmd;
            OleDbDataReader objRst = null;

            try
            {
                //istanzia gli oggetti per il rst
                objCmd = new OleDbCommand(query, this.Conn);
                //fondamentale per query a parametro: imposta il tipo su StoredProcedure
                objCmd.CommandType = CommandType.StoredProcedure;
                //popola l'oggetto DataReader eseguendo la stored
                objRst = objCmd.ExecuteReader();
            }
            catch (Exception err)
            {
                //aggiunge l'errore alla collection
                this.errore.Add(err);
            }

            return objRst;
        }


        /// <summary>
        /// esegue una query di comando
        /// </summary>
        /// <param name="query">Stringa SQL</param>
        /// <returns>
        /// int
        /// 0 se l'esecuzione va a buon fine
        /// -1 in caso di errore
        /// </returns>
        public int eseguiSql(string sql)
        {
            //inizializza il valore di ritorno
            int int_risultato = 0;

            //istanzia l'oggetto
            OleDbCommand objCmd;

            try
            {
                //istanzia gli oggetti per il rst
                objCmd = new OleDbCommand(sql, this.Conn);
                //fondamentale: imposta il tipo su StoredProcedure
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                //valore da restituire in caso di errore
                int_risultato = -1;

                //aggiunge l'errore alla collection
                this.errore.Add(err);
            }

            return int_risultato;
        }

        /// <summary>
        /// Raccoglie i messaggi di errore in una stringa
        /// </summary>
        /// <returns>Una stringa che elenca i messaggi di errore</returns>
        public string visualizzaErrori()
        {
            string str = "";

            foreach (Exception e in this.errore)
                str += e.Message + "<br/>";

            return str;
        }


        //Membri privati
        private OleDbConnection Conn;
        private System.Collections.ArrayList errore;
        private string db_path;
        private string db_provider;
    } //fine classe OleDb
} //fine namespace