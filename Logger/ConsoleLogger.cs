using SimpleLogger.Configuration;
using SimpleLogger.Core;
using SimpleLogger.Enums;
using SimpleLogger.Models;

namespace SimpleLogger.Logger
{
    /// <summary>
    /// Schreibt Logeinträge auf die Standardausgabe (<c>Console</c>).
    /// Optional kann jeder Eintrag farbig ausgegeben werden.
    /// </summary>
    public class ConsoleLogger : LoggerBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ConsoleLogger"/> Klasse mit optionaler Konfiguration.
        /// </summary>
        /// <param name="config">Die Konfiguration für den Logger (optional).</param>
        public ConsoleLogger(LoggerConfig? config = null) : base(config) { }

        /// <summary>
        /// Protokolliert eine Textnachricht.
        /// </summary>
        /// <param name="message">Die zu protokollierende Nachricht.</param>
        /// <param name="level">Der Schweregrad des Logeintrags (Standard: <see cref="LogLevel.Info"/>).</param>
        /// <param name="category">Eine vordefinierte Kategorie für den Logeintrag.</param>
        /// <param name="customCategory">Eine frei wählbare, zusätzliche Kategorie.</param>
        public void Log(string message, LogLevel level = LogLevel.Info, LogCategory category = LogCategory.None, string? customCategory = null)
        {
            WriteLog(CreateLogEntry(new LogEntry(message, level, category, customCategory)), level);
        }

        /// <summary>
        /// Protokolliert eine Ausnahme (<see cref="Exception"/>).
        /// </summary>
        /// <param name="ex">Die auszugebende Ausnahme.</param>
        /// <param name="level">Der Schweregrad des Logeintrags (Standard: <see cref="LogLevel.Error"/>).</param>
        /// <param name="category">Eine vordefinierte Kategorie für den Logeintrag.</param>
        /// <param name="customCategory">Eine frei wählbare, zusätzliche Kategorie.</param>
        public void Log(Exception ex, LogLevel level = LogLevel.Error, LogCategory category = LogCategory.None, string? customCategory = null)
        {
            WriteLog(CreateLogEntry(new LogEntry(ex.ToString(), level, category, customCategory)), level);
        }

        /// <summary>
        /// Gibt den formatierten Logeintrag auf der Konsole aus.
        /// </summary>
        /// <param name="logEntry">Der fertig formatierte Text des Logeintrags.</param>
        /// <param name="level">Der Schweregrad, anhand dessen bei farbiger Ausgabe die <see cref="Console.ForegroundColor"/> gesetzt wird. </param>
        private void WriteLog(string logEntry, LogLevel level)
        {
            if (logEntry == null) return;

            try
            {
                if (Config.LogToConsoleColourfull)
                {
                    Console.ForegroundColor = GetColor(level);
                    Console.WriteLine(logEntry);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(logEntry);
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Logger-Fehler: {ex.Message}");
            }
        }

        /// <summary>
        /// Ermittelt die Konsolenfarbe, die dem angegebenen <see cref="LogLevel"/> zugeordnet ist.
        /// </summary>
        /// <param name="level">Der Schweregrad des Logeintrags.</param>
        /// <returns>Die zu verwendende <see cref="ConsoleColor"/>.</returns>
        private ConsoleColor GetColor(LogLevel level) => level switch
        {
            LogLevel.Info => ConsoleColor.Gray,
            LogLevel.Warn => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Debug => ConsoleColor.Cyan,
            _ => ConsoleColor.White
        };
    }
}
