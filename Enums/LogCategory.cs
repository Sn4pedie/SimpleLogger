namespace SimpleLogger.Enums
{
    /// <summary>
    /// Definiert vordefinierte Kategorien für Log-Einträge zur besseren Strukturierung und Filterung.
    /// </summary>
    public enum LogCategory
    {
        /// <summary>
        /// Allgemeine Log-Nachricht, die keiner spezifischen Kategorie zugeordnet ist.
        /// </summary>
        General,

        /// <summary>
        /// Log-Nachricht im Zusammenhang mit Authentifizierung oder Autorisierung.
        /// </summary>
        Auth,

        /// <summary>
        /// Log-Nachricht, die sich auf Netzwerkoperationen bezieht.
        /// </summary>
        Netzwerk,

        /// <summary>
        /// Log-Nachricht im Kontext von Datenbankoperationen.
        /// </summary>
        Datenbank,

        /// <summary>
        /// Log-Nachricht, die sich auf Benutzeroberflächen-Ereignisse oder Fehler bezieht.
        /// </summary>
        UI,

        /// <summary>
        /// Log-Nachricht im Zusammenhang mit API-Aufrufen.
        /// </summary>
        API,

        /// <summary>
        /// Spezifische Debug-Informationen, die nur zur Entwicklungszeit relevant sind.
        /// </summary>
        Debug,

        /// <summary>
        /// Keine definierte Kategorie; kann in Verbindung mit benutzerdefinierter Kategorie verwendet werden.
        /// </summary>
        None
    }
}
