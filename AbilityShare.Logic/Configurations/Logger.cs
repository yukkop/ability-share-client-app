using System;
using System.IO;

namespace AbilityShare.Logic.Configurations;

public class Logger
{
    private static string _logPath = "ability-share.log";

    #region Log

    private static string buildLog(string message)
    {
        return $"[{DateTime.Now}] {message}\n";
    }

    /// <summary>
    /// Записывает сообщение в журнал по пути "{папка проекта}/lanary.log"
    /// </summary>
    /// <remarks>
    /// Используеться вопределении конфига во избежании рекурсий
    /// </remarks>
    /// <param name="message">Сообщение которое будет записанно в журнал</param>
    public static async void LogOnlyInFile(string message)
    {
        string log = buildLog(message);

        await File.AppendAllTextAsync(_logPath, log);
    }

    /// <summary>
    /// Записывает сообщение в журнал по пути "{папка проекта}/lanary.log" <para/>
    /// и в консоль (в засвисимости от настроек конфига)
    /// </summary>
    /// <param name="message">Сообщение которое будет логированно</param>
    public static async void Log(string message)
    {
        string log = buildLog(message);

        if (Config.MainConfig.Logger.IsConsoleLogEnable)
            Console.WriteLine(log);
        await File.AppendAllTextAsync(_logPath, log);
    }

    /// <summary>
    /// Записывает сообщение в журнал по пути "{папка проекта}/lanary.log" <para/>
    /// и в консоль (в засвисимости от настроек конфига)
    /// </summary>
    /// <param name="message">Сообщение которое будет логированно</param>
    public static void Log(int message)
    {
        Logger.Log(message.ToString());
    }

    /// <summary>
    /// Записывает сообщение в журнал по пути "{папка проекта}/lanary.log" <para/>
    /// и в консоль (в засвисимости от настроек конфига)
    /// </summary>
    /// <param name="message">Сообщение которое будет логированно</param>
    public static void Log(double message)
    {
        Logger.Log(message.ToString());
    }

    /// <summary>
    /// Записывает сообщение в журнал по пути "{папка проекта}/lanary.log" <para/>
    /// и в консоль (в засвисимости от настроек конфига)
    /// </summary>
    /// <param name="message">Сообщение которое будет логированно</param>
    public static void Log(float message)
    {
        Logger.Log(message.ToString());
    }

    /// <summary>
    /// Записывает сообщение в журнал по пути "{папка проекта}/lanary.log" <para/>
    /// и в консоль (в засвисимости от настроек конфига)
    /// </summary>
    /// <param name="message">Сообщение которое будет логированно</param>
    public static void Log(decimal message)
    {
        Logger.Log(message.ToString());
    }

    /// <summary>
    /// Записывает сообщение в журнал по пути "{папка проекта}/lanary.log" <para/>
    /// и в консоль (в засвисимости от настроек конфига)
    /// </summary>
    /// <param name="message">Сообщение которое будет логированно</param>
    public static void Log(bool message)
    {
        Logger.Log(message.ToString());
    }
    #endregion

    #region LogError
    private static string buildError(string message)
    {
        return $"[{DateTime.Now}] !ERROR! {message}\n";
    }

    public static async void LogError(string message)
    {
        string log = buildError(message);

        if (Config.MainConfig.Logger.IsConsoleLogEnable)
            Console.WriteLine(log);
        await File.AppendAllTextAsync(_logPath, log);
    }

    public static void LogError(int message)
    {
        Logger.LogError(message.ToString());
    }

    public static void LogError(double message)
    {
        Logger.LogError(message.ToString());
    }

    public static void LogError(float message)
    {
        Logger.LogError(message.ToString());
    }

    public static void LogError(decimal message)
    {
        Logger.LogError(message.ToString());
    }

    public static void LogError(bool message)
    {
        Logger.LogError(message.ToString());
    }

    #endregion
}