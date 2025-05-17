using SimpleLogger.Models;

namespace SimpleLogger.Core
{
    /// <summary>
    /// Definiert Methoden zur Erstellung von Logeinträgen.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Erstellt einen Logeintrag auf Basis eines <see cref="LogEntry"/>-Objekts.
        /// </summary>
        /// <param name="logEntry">Das Logeintragsobjekt, das protokolliert werden soll.</param>
        /// <returns>Ein String, der den erzeugten Logeintrag repräsentiert.</returns>
        string CreateLogEntry(LogEntry logEntry);


        //Aktuell nich benutzt. Ggf. zukünftig
        //
        //string CreateExeptionLogEntry(Exception ex, LogLevel level = LogLevel.Error, LogCategory category = LogCategory.None, string? customCategory = null);
    }
}
