namespace SimpleLogger.Enums
{
    /// <summary>
    /// Gibt den Schweregrad eines Log-Eintrags an.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug-Informationen, hauptsächlich für Entwickler.
        /// </summary>
        Debug,

        /// <summary>
        /// Allgemeine Informationen über den normalen Ablauf des Programms.
        /// </summary>
        Info,

        /// <summary>
        /// Warnung über ein potenzielles Problem, das untersucht werden sollte.
        /// </summary>
        Warn,

        /// <summary>
        /// Ein schwerwiegender Fehler, der das Programm oder einen Teil davon beeinträchtigen könnte.
        /// </summary>
        Error
    }
}
