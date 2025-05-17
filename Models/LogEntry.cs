using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimpleLogger.Enums;

namespace SimpleLogger.Models
{
    /// <summary>
    /// Repräsentiert einen einzelnen Log-Eintrag mit Zeitstempel, Nachricht, Schweregrad und Kategorie.
    /// </summary>
    public class LogEntry
    {
        private DateTime _timestamp = DateTime.Now;
        private string _message;
        private LogLevel _level;
        private LogCategory _category;
        private string _customCategory;

        /// <summary>
        /// Erstellt einen neuen <see cref="LogEntry"/> mit den angegebenen Parametern.
        /// </summary>
        /// <param name="message">Die Log-Nachricht.</param>
        /// <param name="level">Der Schweregrad des Log-Eintrags.</param>
        /// <param name="logCategory">Die Kategorie des Log-Eintrags. Wenn nicht angegeben, ist die Kategorie "None"</param>
        /// <param name="customCategory">Eine benutzerdefinierte Kategorie (optional).</param>
        public LogEntry(string message, LogLevel level, LogCategory logCategory = LogCategory.None, string? customCategory = null)
        {
            _message = message;
            _level = level;
            _category = logCategory;
            _customCategory = customCategory == null ? String.Empty : customCategory;
        }

        /// <summary>
        /// Gibt den Zeitstempel zurück, zu dem der Log-Eintrag erstellt wurde.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get => _timestamp; }

        /// <summary>
        /// Gibt die Nachricht des Log-Eintrags zurück.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get => _message; }

        /// <summary>
        /// Gibt den Schweregrad (<see cref="LogLevel"/>) des Eintrags zurück.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("level")]
        public LogLevel Level { get => _level; }

        /// <summary>
        /// Gibt die Kategorie (<see cref="LogCategory"/>) des Log-Eintrags zurück.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("category")]
        public LogCategory Category { get => _category; }

        /// <summary>
        /// Gibt die benutzerdefinierte Kategorie des Log-Eintrags zurück, falls angegeben.
        /// </summary>
        [JsonProperty("customCategory")]
        public string CustomCategory { get => _customCategory; }
    }
}
