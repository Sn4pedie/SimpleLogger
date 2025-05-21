# SimpleLogger

SimpleLogger ist eine flexible und einfach nutzbare Logging-Bibliothek für .NET-Anwendungen. Sie unterstützt das Schreiben von Logeinträgen in Dateien (Text oder JSON), die Konsolenausgabe sowie asynchrones Logging mit Warteschlangenverarbeitung. Die Konfiguration ist flexibel und unterstützt u.a. Rolling Logs und verschiedene Log-Level.

## Features

- Logging in Dateien (Text & NDJSON)
- Asynchrones Logging (Hintergrundverarbeitung)
- Rolling Logfiles (tägliche Dateien)
- Log-Level & Kategorien
- Konfiguration über Code
- Konsolen-Logger enthalten
- Einfache Erweiterbarkeit

## Installation

Füge das Projekt SimpleLogger zu deiner Solution hinzu oder kopiere die relevanten Dateien in dein Projekt.

> **Voraussetzungen:**  
> .NET Core oder .NET Framework, je nach Projektstruktur.

## Schnellstart

### Beispiel: Datei-Logger

```csharp
using SimpleLogger.Logger;
using SimpleLogger.Configuration;

var config = new LoggerConfig
{
    LogDirectory = "Logs",
    LogFileName = "app",
    LogFileFormatJson = false,
    MinimumLogLevel = LogLevel.Info,
    EnableRollingLog = true,
    OverwriteOnStart = false
};

var logger = new FileLogger(config);

logger.Log("Anwendung gestartet.");
logger.Log(new Exception("Ein Fehler!"));
```

### Beispiel: Asynchroner Datei-Logger

```csharp
using SimpleLogger.Logger;

var asyncLogger = new FileLoggerAsync();
asyncLogger.Log("Dies ist ein asynchroner Log-Eintrag.");
```

## Konfiguration

Die wichtigsten Einstellungen findest du in `LoggerConfig`:

- **LogDirectory**: Speicherort der Logdateien
- **LogFileName**: Basisname der Logdateien
- **LogFileFormatJson**: Logeinträge als NDJSON speichern (`true`) oder als Text (`false`)
- **MinimumLogLevel**: Minimales Log-Level (z.B. Info, Warn, Error)
- **EnableRollingLog**: Neue Logdatei pro Tag
- **OverwriteOnStart**: Überschreibt bestehende Logdatei beim Start

## Logger-Typen

- **FileLogger**: Schreibt synchron in eine Datei
- **FileLoggerAsync**: Schreibt asynchron (empfohlen für hohe Log-Frequenz)
- **ConsoleLogger**: Gibt Logs auf der Konsole aus

## Lizenz

Dieses Projekt steht unter der MIT-Lizenz.

## Beitrag

Pull Requests sind willkommen! Bitte öffne ein Issue für Vorschläge oder Fehler.

---

*Autor: Sn4pedie*
