using SimpleLogger.Enums;

namespace SimpleLogger.Configuration
{
    /// <summary>
    /// Stellt die Konfigurationseinstellungen für den Logger bereit.
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// Das Verzeichnis, in dem die Logdateien gespeichert werden.
        /// Standardmäßig ist dies ein "Logs"-Ordner im Basisverzeichnis der Anwendung.
        /// </summary>
        public string LogDirectory { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        /// <summary>
        /// Der Basisname der Logdateien ohne Dateierweiterung.
        /// </summary>
        public string LogFileName { get; set; } = "log";

        /// <summary>
        /// Gibt an, ob die Logdateien im JSON-Format oder als Plain Text gespeichert werden sollen.
        /// </summary>
        public bool LogFileFormatJson { get; set; } = false;

        /// <summary>
        /// Gibt das minimale Log-Level an, das geloggt werden soll.
        /// Nachrichten unterhalb dieses Levels werden ignoriert.
        /// Mögliche Log-Level werden von <see cref="LogLevel"/> bereitgestellt.
        /// </summary>
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Info;

        /// <summary>
        /// Aktiviert oder deaktiviert das Rolling-Log-Verhalten.
        /// Wenn aktiviert, werden für jeden Tag neue Dateien erstellt.
        /// </summary>
        public bool EnableRollingLog { get; set; } = false;

        /// <summary>
        /// Gibt an, ob vorhandene Logdateien beim Start der Anwendung überschrieben werden sollen.
        /// </summary>
        public bool OverwriteOnStart { get; set; } = false;

        /// <summary>
        /// Gibt an, ob die Log-Ausgabe in der Konsole farbig dargestellt werden soll.
        /// </summary>
        public bool LogToConsoleColourfull { get; set; } = true;
    }
}
