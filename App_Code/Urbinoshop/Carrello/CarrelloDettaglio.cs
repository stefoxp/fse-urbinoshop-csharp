namespace Urbinoshop.Carrello
{
    /// <summary>
    /// Memorizza i dati di un record del carrello
    /// </summary>
    /// <todo>
    /// da aggiungere una gestione errori
    /// </todo>
    /// <see cref="Esercizi.Sito.CarrelloDettagli.cs"/>
    public class CarrelloDettaglio
    {
        public long IDutente;
        public long IDprodotto;
        public int Quantita;

        /// <summary>
        /// Verifica la presenza di tutti i dati
        /// </summary>
        /// <returns>
        /// int 0 in caso di presenza di tutti i dati
        /// 1 in caso manchino dei dati
        ///</returns>
        /// 
        public int verificaDati()
        {
            //inizializza il valore da restituire
            int intRisultato = 0;
            /*
            if (userID == 0 || !isNumeric(userID)) || 
                (IDprodotto == 0 || !isNumeric(IDprodotto)) || 
                (Quantita == 0 Or ! isNumeric(Quantita)) 
            {
                //restituisce un codice di errore
                int_risultato = 1;
            };//fine if
        */
            return intRisultato;
        }//fine metodo verificaDati
    }//fine classe CarrelloDettaglio
}//fine namespace Urbinoshop.Carrello