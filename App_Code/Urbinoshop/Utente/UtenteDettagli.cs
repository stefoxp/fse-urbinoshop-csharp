namespace Urbinoshop.Utente
{
    /// <summary>
    /// Descrizione di riepilogo per la classe UtenteDettagli
    /// </summary>
    /// /// <todo>
    /// Le proprietà possono essere rese tutte private
    /// Da aggiungere un costruttore che chiama tutti i metodi set
    /// da aggiungere i metodi set con controllo del parametro
    /// da aggiungere i metodi get pubblici
    /// da aggiungere una gestione errori
    /// </todo>
    public class UtenteDettagli
    {
        /* proprietà pubbliche */
        public int IdUtente;
        public string UserName;
        public string Password;
        public int IdRuolo;
        public string Nome;
        public string Indirizzo;
        public string Telefono;
        public string Email;

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="utente_id">ID utente</param>
        /// <param name="name">Nome utente</param>
        /// <param name="pw">Password</param>
        /// <param name="ruolo_id">ID Ruolo</param>
        /// <param name="nome">Nome per esteso</param>
        /// <param name="indirizzo">Indirizzo</param>
        /// <param name="tel">Telefono</param>
        /// <param name="mail">Indirizzo e-mail</param>
        public UtenteDettagli(int utente_id = 0,
                                string name = "",
                                string pw = "",
                                int ruolo_id = 0,
                                string nome = "",
                                string indirizzo = "",
                                string tel = "",
                                string mail = "")
        {
            // inizializza tutte le proprietà
            this.IdUtente = utente_id;
            this.UserName = name;
            this.Password = pw;
            this.IdRuolo = ruolo_id;
            this.Nome = nome;
            this.Indirizzo = indirizzo;
            this.Telefono = tel;
            this.Email = mail;
        }

    } // fine classe
} //fine namespace Urbinoshop.Utente