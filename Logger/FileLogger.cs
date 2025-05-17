using System.Text;
using SimpleLogger.Configuration;
using SimpleLogger.Core;
using SimpleLogger.Enums;
using SimpleLogger.Models;

namespace SimpleLogger.Logger
{
    /// <summary>
    /// Implementiert einen Logger, der Logeinträge in eine Datei schreibt.
    /// Unterstützt Text- oder JSON-Format sowie Rolling Logs.
    /// </summary>
    public class FileLogger : LoggerBase
    {
        private readonly object lockObj = new object();
        private string _logFilePath;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="FileLogger"/> Klasse mit optionaler Konfiguration.
        /// </summary>
        /// <param name="config">Die Konfiguration für den Logger. Wenn null, wird eine Standardkonfiguration verwendet.</param>
        public FileLogger(LoggerConfig? config = null) : base(config)
        {
            Init();
        }

        /// <summary>
        /// Gibt den vollständigen Pfad zur Logdatei zurück.
        /// </summary>
        public string LogFilePath { get => _logFilePath; }

        /// <summary>
        /// Initialisiert den Logger und erstellt das Logverzeichnis sowie den Logdateipfad basierend auf der Konfiguration.
        /// </summary>
        public virtual void Init()
        {
            Directory.CreateDirectory(Config.LogDirectory);

            string logFileEnding = Config.LogFileFormatJson == true ? ".json" : ".txt";

            if (Config.EnableRollingLog)
            {
                string date = DateTime.Now.ToString("dd-MM-yyyy");
                _logFilePath = Path.Combine(Config.LogDirectory, $"log_{date}{logFileEnding}");
            }
            else
            {
                _logFilePath = Path.Combine(Config.LogDirectory, Config.LogFileName + logFileEnding);

                if (Config.OverwriteOnStart && File.Exists(_logFilePath))
                {
                    File.WriteAllText(_logFilePath, "", Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// Schreibt eine Textnachricht in das Log.
        /// </summary>
        /// <param name="message">Die zu protokollierende Nachricht.</param>
        /// <param name="level">Die Schwere des Logeintrags (Standard: Info).</param>
        /// <param name="category">Die vordefinierte Logkategorie (Standard: None).</param>
        /// <param name="customCategory">Eine benutzerdefinierte Kategorie, falls erforderlich.</param>
        public virtual void Log(string message, LogLevel level = LogLevel.Info, LogCategory category = LogCategory.None, string? customCategory = null)
        {
            WriteLog(CreateLogEntry(new LogEntry(message, level, category, customCategory)));
        }

        /// <summary>
        /// Protokolliert eine Ausnahme (Exception) als Fehlereintrag.
        /// </summary>
        /// <param name="ex">Die zu protokollierende Ausnahme.</param>
        /// <param name="level">Die Schwere des Logeintrags (Standard: Error).</param>
        /// <param name="category">Die vordefinierte Logkategorie (Standard: None).</param>
        /// <param name="customCategory">Eine benutzerdefinierte Kategorie, falls erforderlich.</param>
        public virtual void Log(Exception ex, LogLevel level = LogLevel.Error, LogCategory category = LogCategory.None, string? customCategory = null)
        {
            WriteLog(CreateLogEntry(new LogEntry(ex.ToString(), level, category, customCategory)));
        }

        /// <summary>
        /// Schreibt einen formatierten Logeintrag in die Logdatei.
        /// </summary>
        /// <param name="logEntry">Der zu schreibende Logeintrag im bereits formatierten Textformat.</param>
        public virtual void WriteLog(string logEntry)
        {
            if (logEntry == String.Empty) return;

            lock (lockObj)
            {
                try
                {
                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Logger-Fehler: {ex.Message}");
                }
            }
        }
    }
}
