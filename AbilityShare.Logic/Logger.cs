using System;
using System.IO;

namespace AbilityShare.Logic;

public class Logger
{
    private static string _logPath = "lanary.log";

    #region Log

    private static string buildLog(string message)
    {
        return $"[{DateTime.Now}] {message}\n";
    }

    public static async void LogInFile(string message)
    {
        string log = buildLog(message);

        await File.AppendAllTextAsync(_logPath, log);
    }
    public static async void Log(string message)
    {
        string log = buildLog(message);

        Console.WriteLine(message);
        await File.AppendAllTextAsync(_logPath, log);
    }

    public static void Log(int message)
    {
        Logger.Log(message.ToString());
    }

    public static void Log(double message)
    {
        Logger.Log(message.ToString());
    }

    public static void Log(float message)
    {
        Logger.Log(message.ToString());
    }

    public static void Log(decimal message)
    {
        Logger.Log(message.ToString());
    }

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

        Console.WriteLine(message);
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