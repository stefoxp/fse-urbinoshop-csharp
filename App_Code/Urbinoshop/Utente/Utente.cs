using System;
using System.Data.OleDb;
using System.Configuration;
//using System.Diagnostics;

namespace Urbinoshop.Utente {

    /// <summary>
    /// Descrizione di riepilogo per la classe 
    /// </summary>
    /// <todo>
    /// Aggiungi una proprietà che punti ad un oggetto UtenteDettagli
    /// da utilizzare invece del parametro nei metodi controllaUtente
    /// ed aggiungiUtente
    /// </todo>
    public class UtenteGestione
    {
        //proprietà di classe
        //private OleDbConnection objConn;
        //private string str_messaggio;
        private OleDb objOle;
        private System.Collections.ArrayList errore = new System.Collections.ArrayList();
        

        /// <summary>
        /// Costruttore
        /// </summary>
        public UtenteGestione()
        {
            //gestione errori
            try
            {
                //recupera il path del db dalla var di applicazione
                string strPath =  ConfigurationManager.AppSettings["dbPath"];
                string strProvider = "Microsoft.Jet.OLEDB.4.0;";

                //istanzia l'oggetto che gestisce la connessione al db
                this.objOle = new OleDb(strPath, strProvider);

            }
            catch (Exception err)
            { 
                //aggiunge l'errore alla collection
                this.errore.Add(err);
            }
        }

        /// <summary>
        /// quando l'istanza della classe viene rilasciata: DISTRUTTORE DI CLASSE
        /// </summary>
        ~UtenteGestione() {
            //svuota la collection
            this.errore.Clear();
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

        /// <summary>
        /// Gestisce il login
        /// </summary>
        /// <param name="strUser">Nome utente</param>
        /// <param name="strPw">Password</param>
        /// <returns>
        /// ID utente registrato o
        /// 0 in caso di login fallito
        /// </returns>
        public int Login(string strUser, string strPw)
        {
            //dichiarazioni
            int intID = 0;

            //compone la stringa SQL
            string strSQL = "spLoginUtente '" + strUser + "', '" + strPw + "'";

            //gestione errori
            try
            {
                //recupera i dati da db
                OleDbDataReader objRst = objOle.apriRst(strSQL);

                while (objRst.Read())
                {
                    //memorizza il codice utente
                    intID = (int)objRst["userID"];
                };


            }
            catch (Exception err)
            {
                //valore da restituire in caso di errore
                intID = -1;

                //aggiunge l'errore alla collection
                this.errore.Add(err);
            };

            //restituire un valore
            return intID;
        }

        /// <summary>
        /// controlla lo UserName del nuovo utente e lo aggiunge al db
        /// </summary>
        /// <param name="objDettagliUtente">istanza della classe UtenteDettagli</param>
        /// <returns>
        /// 1 se l'aggiunta va a buon fine
        /// 0 in caso di UserName già presente
        /// -1 in caso di errore
        /// </returns>
        public int controllaUtente(UtenteDettagli objDettagliUtente) {

            //inizializza il valore di ritorno
            int intRisultato = 1;

            string strSQL = "spVerificaUtente " + objDettagliUtente.UserName;

             //gestione errori
            try
            {

                //recupera i dati da db
                OleDbDataReader objRst = objOle.apriRst(strSQL);

                //verifica la presenza di record
                if (objRst.Read())
                    // sono presenti: restituisce 0
                    intRisultato = 0;

            }
            catch (Exception err)
            {
                //valore da restituire in caso di errore
                intRisultato = -1;

                //aggiunge l'errore alla collection
                this.errore.Add(err);
            };

            //controlla che lo UserName sia libero
            if (intRisultato == 1)
                //esegue il metodo per la registrazione
                this.aggiungiUtente(objDettagliUtente);

            return intRisultato;
        }//fine metodo controllaUtente
        
        /// <summary>
        /// aggiunge un utente a tblUtente
        /// </summary>
        /// <param name="objDettagliUtente">istanza della classe UtenteDettagli</param>
        /// <returns>
        /// 1 se l'aggiunta va a buon fine
        /// -1 in caso di errore
        /// </returns>
        private int aggiungiUtente(UtenteDettagli objDettagliUtente)
        {
            //valore da restituire
            int intRisultato = 1;

            string strSQL = "spAddUtente '" + objDettagliUtente.UserName + "', '" 
                                            + objDettagliUtente.Password + "', '" 
                                            + objDettagliUtente.Nome + "', '" 
                                            + objDettagliUtente.Indirizzo + "', '" 
                                            + objDettagliUtente.Telefono + "', '" 
                                            + objDettagliUtente.Email + "'";
            try
            {
               /*
                //istanzia gli oggetti per il rst
                OleDbCommand objCmd = new OleDbCommand(strSQL, this.objConn);
                //fondamentale: imposta il tipo su StoredProcedure
                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                objCmd.ExecuteNonQuery();
                */
                objOle.eseguiSql(strSQL);
            }
            catch (Exception err)
            {
                //valore da restituire in caso di errore
                intRisultato = -1;

                //aggiunge l'errore alla collection
                this.errore.Add(err);
            };

            return intRisultato;
        
        }//fine metodo aggiungiUtente

        /// <summary>
        /// Permette il cambio della propria password
        /// per l'utente autenticato
        /// </summary>
        /// <param name="userID">Utente ID</param>
        /// <param name="pw">Nuova valore da assegnare alla password</param>
        /// <returns>
        /// 1 se va a buon fine
        /// -1 in caso di errore
        /// </returns>
        int cambiaPassword(int userID, string pw)
        {
            //inizializza il valore di ritorno
            int intRisultato = 1;

            /*
            * corpo del metodo da implementare
            */

            //restituisce il valore di ritorno
            return intRisultato;
        }//fine metodo cambiaPassword
    
     /*   public int loginAdmin(string strUser, string strPw)
            '-----------------------------------------------------------------------------
            ' Metodo LoginAdmin
            ' Scopo:        login per gli amministratori
            ' Argomenti:    accetta un'istanza della classe DettagliUtente
            ' Data:         06/02/2005
            ' Stato:        funziona con Access 2000
            '-----------------------------------------------------------------------------

            'dichiarazioni
            Dim intID
            Dim objRst
        
            'inizializza il valore da restituire
            intID = 0
        
            'gestione errori
            On Error Resume Next

            'istanzia gli oggetti per il rst
            objRst = Server.CreateObject("ADODB.Recordset")

            With objRst
                'connessione da utilizzare
                .ActiveConnection = objConn
                'sorgente dati (query parametrica: passa i 2 parametri nell'ordine)
                .Source = "spLoginAdmin " & strUser & ", " & strPw
                'apre il rst
                .Open()
                'verifica la presenza di record
                If Not (.BOF Or .EOF) Then
                    'si sposta sul primo record
                    .MoveFirst()
                    'verifica il ruolo ricoperto dall'utente
                    If .Fields("Ruolo").value = "amministratore" Then
                        'memorizza il codice utente
                        intID = .Fields("userID").Value
                    End If
                End If
            End With

            'restituire un valore
            LoginAdmin = intID
        
            'rilascia le istanze di tutti gli oggetti utilizzati
            objRst = Nothing
        
            'verifica la presenza di un errore
            If Err.Number <> 0 Then
                'visualizza un messaggio informativo
                Response.Write("<b>Errore nella funzione di login - classe Utente (amministrazione):</b> " _
                                & Err.Description & "<br />")
            End If
        End Function
      * 
      */
       
    /// <summary>
    /// Modifica i dati di registrazione dell'utente
    /// Note: lo userID non può essere modificato
    /// ma serve per identificare l'utente su db;
    /// lo UserName non può essere modificato
    /// </summary>
    /// <param name="userID">ID unico utente</param>
    /// <param name="pw">Password</param>
    /// <param name="nome">Nome</param>
    /// <param name="indirizzo">Indirizzo</param>
    /// <param name="telefono">Telefono</param>
    /// <param name="email">Email</param>
    /// <returns>
    /// 1 se va a buon fine
    /// -1 in caso di errore
    /// </returns>
    int modificaUtente(int userID,
                        string pw,
                        string nome,
                        string indirizzo,
                        string telefono,
                        string email)   
    {
        //inizializza il valore di ritorno
        int intRisultato = 1;

        /*
        * corpo del metodo da implementare
        */

        //restituisce il valore di ritorno
        return intRisultato;
    }//fine metodo eliminaUtente

        /*
        Public Function Modifica(ByVal objDettagliUtente)
            '-----------------------------------------------------------------------------
            ' Metodo Modifica
            ' Scopo:        modifica i dati di un utente
            ' Argomenti:    accetta un'istanza della classe DettagliUtente
            ' Data:         13/02/2005
            ' Stato:        funziona con Access 2000
            '-----------------------------------------------------------------------------
            Dim objRst
            Dim strSql

            'gestione errori
            On Error Resume Next
        
            objRst = Server.CreateObject("ADODB.Recordset")

            With objDettagliUtente
                strSql = "spEditUtente " & .UserName & ", " _
                                        & .Password & ", " _
                                        & .IDruolo & ", " _
                                        & .userID '& ", '" _
                '& .Nome & "', '" _
                '& .Indirizzo & "', '" _
                '& .Telefono & "', '" _
                '& .Email & "'"
            End With

            With objRst
                'connessione da utilizzare
                .ActiveConnection = objConn
                'sorgente dati (tabella, query o stringa SQL)
                .Source = strSql
                'blocco dati ottimistico:
                'li blocca solo al momento della chiamata di Update
                .LockType = adLockOptimistic
                'apre il rst
                .Open()
            End With
        
            'rilascia le istanze di tutti gli oggetti utilizzati
            objRst = Nothing
        
            'verifica la presenza di un errore
            If Err.Number <> 0 Then
                'visualizza un messaggio informativo
                Response.Write("<b>Errore durante il salvataggio delle modifiche all'utente - classe Utente:</b> " _
                                & Err.Description & "<br />")
            End If
        End Function
        */


    /// <summary>
    /// Rimuove o disabilita l'utente indicato dal database
    /// </summary>
    /// <param name="userID">UtenteID</param>
    /// <returns>
    /// 1 se la rimozione va a buon fine
    /// -1 in caso di errore
    /// </returns>
    int eliminaUtente(int userID)
    {
        //inizializza il valore di ritorno
        int intRisultato = 1;

        /*
        * corpo del metodo da implementare
        */

        //restituisce il valore di ritorno
        return intRisultato;
    }//fine metodo eliminaUtente

      
      /* 
        Public Function Elimina(ByVal objDettagliUtente)
            '-----------------------------------------------------------------------------
            ' Metodo Elimina
            ' Scopo:        elimina l'utente
            ' Argomenti:    accetta un'istanza della classe DettagliUtente
            ' Data:         13/02/2005
            ' Stato:        funziona con Access 2000
            ' N.B.:         manca il controllo sull'utilizzo del codiceUtente
            '               se l'utente ha un carrello viene restituito 
            '               solamente un errore generico SQL
            '-----------------------------------------------------------------------------
            Dim objRst
            Dim strSql

            'gestione errori
            On Error Resume Next
        
            objRst = Server.CreateObject("ADODB.Recordset")

            strSql = "spEliminaUtente " & objDettagliUtente.userID

            With objRst
                'connessione da utilizzare
                .ActiveConnection = objConn
                'sorgente dati (tabella, query o stringa SQL)
                .Source = strSql
                'blocco dati ottimistico:
                'li blocca solo al momento della chiamata di Update
                .LockType = adLockOptimistic
                'apre il rst
                .Open()
            End With
        
            'rilascia le istanze di tutti gli oggetti utilizzati
            objRst = Nothing
        
            'verifica la presenza di un errore
            If Err.Number <> 0 Then
                'visualizza un messaggio informativo
                Response.Write("<b>Errore durante l'eliminazione dell'utente - classe Utente:</b> " _
                                & Err.Description & "<br />")
            End If
        }
         * */
    } // fine classe
} //fine namespace Urbinoshop.Utente