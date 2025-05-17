using System.Collections.Concurrent;
using System.Text;
using SimpleLogger.Configuration;
using SimpleLogger.Enums;
using SimpleLogger.Models;

namespace SimpleLogger.Logger
{
    /// <summary>
    /// Ein asynchroner Datei-Logger, der Logeinträge in einer Hintergrundaufgabe verarbeitet.
    /// </summary>
    public class FileLoggerAsync : FileLogger, IDisposable
    {
        private ConcurrentQueue<string> _logQueue;
        private CancellationTokenSource _cts;
        private Task _logTask;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="FileLoggerAsync"/> Klasse mit optionaler Konfiguration.
        /// </summary>
        /// <param name="config">Die Konfiguration für den Logger (optional).</param>
        public FileLoggerAsync(LoggerConfig? config = null) : base(config)
        {
            Init();
        }

        /// <summary>
        /// Initialisiert die interne Logik und startet die Hintergrundverarbeitung der Logwarteschlange.
        /// </summary>
        public override void Init()
        {
            base.Init();

            _logQueue = new ConcurrentQueue<string>();
            _cts = new CancellationTokenSource();
            _logTask = Task.Run(() => ProcessLogQueue(_cts.Token));
        }

        /// <summary>
        /// Fügt eine Nachricht der Log-Warteschlange hinzu.
        /// </summary>
        /// <param name="message">Die zu protokollierende Nachricht.</param>
        /// <param name="level">Die Schwere des Logeintrags (Standard: Info).</param>
        /// <param name="category">Die Kategorie des Logeintrags (Standard: None).</param>
        /// <param name="customCategory">Eine benutzerdefinierte Kategorie (optional).</param>
        public override void Log(string message, LogLevel level = LogLevel.Info, LogCategory category = LogCategory.None, string? customCategory = null)
        {
            _logQueue.Enqueue(CreateLogEntry(new LogEntry(message, level, category, customCategory)));
        }

        /// <summary>
        /// Fügt eine Ausnahme der Log-Warteschlange hinzu.
        /// </summary>
        /// <param name="ex">Die zu protokollierende Ausnahme.</param>
        /// <param name="level">Die Schwere des Logeintrags (Standard: Error).</param>
        /// <param name="category">Die Kategorie des Logeintrags (Standard: None).</param>
        /// <param name="customCategory">Eine benutzerdefinierte Kategorie (optional).</param>
        public override void Log(Exception ex, LogLevel level = LogLevel.Error, LogCategory category = LogCategory.None, string? customCategory = null)
        {
            _logQueue.Enqueue(CreateLogEntry(new LogEntry(ex.ToString(), level, category, customCategory)));
        }

        /// <summary>
        /// Verarbeitet die Log-Warteschlange asynchron und schreibt die Einträge in die Logdatei.
        /// </summary>
        /// <param name="token">Das Abbruch-Token zur Steuerung der Verarbeitung.</param>
        private async Task ProcessLogQueue(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_logQueue.TryDequeue(out string entry))
                {
                    try
                    {
                        File.AppendAllText(LogFilePath, entry + Environment.NewLine, Encoding.UTF8);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Logger-Fehler (Async): {ex.Message}");
                    }
                }
                else
                {
                    await Task.Delay(50); // entlastet CPU bei Leerlauf
                }
            }
        }

        /// <summary>
        /// Beendet die Hintergrundverarbeitung und gibt verwendete Ressourcen frei.
        /// </summary>
        public void Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _logTask.Wait(); // Aufräumen
                _cts.Dispose();
            }
        }
    }
}
