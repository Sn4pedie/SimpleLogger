using System.Text;
using Newtonsoft.Json;
using SimpleLogger.Configuration;
using SimpleLogger.Enums;
using SimpleLogger.Models;

namespace SimpleLogger.Core
{
    /// <summary>
    /// Basisimplementierung. Logeinträge werden je nach Konfiguration als Plain Text oder JSON formatiert.
    /// </summary>
    public class LoggerBase : ILogger
    {
        private LoggerConfig _config;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="LoggerBase"/> Klasse mit optionaler Konfiguration.
        /// </summary>
        /// <param name="config">Die Konfiguration des Loggers. Wenn <c>null</c>, wird eine Standardkonfiguration verwendet.</param>
        public LoggerBase(LoggerConfig? config = null)
        {
            _config = config ?? new LoggerConfig();
        }

        /// <summary>
        /// Ruft die Logger-Konfiguration ab.
        /// </summary>
        public LoggerConfig Config { get => _config; }

        /// <summary>
        /// Erstellt einen formatierten Logeintrag auf Basis des angegebenen <see cref="LogEntry"/>.
        /// </summary>
        /// <param name="logEntry">Der Logeintrag, der verarbeitet werden soll.</param>
        /// <returns>
        /// Der formatierte Logtext oder ein leerer String, wenn das Log-Level unter dem Minimum liegt.
        /// </returns>
        public string CreateLogEntry(LogEntry logEntry)
        {
            if (logEntry.Level < Config.MinimumLogLevel)
                return String.Empty;

            if (Config.LogFileFormatJson)
            {
                return JsonConvert.SerializeObject(logEntry, Formatting.Indented);
            }
            else
            {
                string categoryString = logEntry.Category != LogCategory.None ? logEntry.Category.ToString() : logEntry.CustomCategory;
                string timeStamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                string levelText = logEntry.Level.ToString().PadLeft(5);
                return $"[{timeStamp}] [{levelText}] [{categoryString}] {logEntry.Message}";
            }
        }

        //Aktuell nich benutzt. Ggf. zukünftig
        /*
        public string CreateExeptionLogEntry(Exception ex, LogLevel level = LogLevel.Error, LogCategory category = LogCategory.None, string? customCategory = null)
        {
            if (ex == null) return String.Empty;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Exception:");
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);

            Exception? inner = ex.InnerException;

            while (inner != null)
            {
                sb.AppendLine("Inner Exception:");
                sb.AppendLine(inner.Message);
                sb.AppendLine(inner.StackTrace);
                inner = inner.InnerException;
            }

            return CreateLogEntry(new LogEntry(sb.ToString(), level, category, customCategory));
        }
        */
    }
}
