using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using AbilityShare.Logic.Configurations.Models;

namespace AbilityShare.Logic.Configurations;

public class Config
{
    private static Config? _mainConfig;

    /// <summary>
    /// Singleton конфигураций
    /// </summary>
    public static Config MainConfig => _mainConfig ??= Config.Load(); // Если конфиг еще не создан (_mainConfig == null)
                                                                      // То вызываеться метод Config.Load()

    /// <summary>
    /// Путь к файлу конфигураций
    /// </summary>
    public static readonly string ConfigPath = "config.yaml";

    /// <summary>
    /// Конфигурации Логгера
    /// </summary>
    public LoggerModel Logger;

    public Config()
    {
        Logger = new LoggerModel
        {
            IsConsoleLogEnable = false
        };
    }

    /// <summary>
    /// Стандартные конфигурации<para/>
    /// Для первого запуска программы.
    /// </summary>
    /// <returns></returns>
    public static Config GetDefault()
    {
        return new Config();
    }

    /// <summary>
    /// Загружает конфиг
    /// Если файла по пути из поля ConfigPath нет, создаст новый конфиг
    /// </summary>
    /// <returns>GetDefault() - если конфига не оказалось по пути из поля ConfigPath</returns>
    public static Config Load()
    {
        Configurations.Logger.LogOnlyInFile("Looking for an existing config.");
        FileInfo fileInfo = new FileInfo(ConfigPath);
        if (fileInfo.Exists)
        {
            Configurations.Logger.LogOnlyInFile("Config found!");
            Configurations.Logger.LogOnlyInFile("Read config");

            using (var input = File.OpenText(ConfigPath))
            {
                var deserializerBuilder = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance);

                var deserializer = deserializerBuilder.Build();

                Configurations.Logger.LogOnlyInFile("Deserializing the config");
                var result = deserializer.Deserialize<Config>(input);
                return result;
            }
        }
        else
        {
            Configurations.Logger.LogOnlyInFile("Config not found!");
            Configurations.Logger.LogOnlyInFile("Create standard configurations.");

            Config defaultConf = Config.GetDefault();

            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yaml = serializer.Serialize(defaultConf);

            try
            {
                StreamWriter sw = new StreamWriter(ConfigPath);
                sw.WriteLine(yaml);
                sw.Close();

                Configurations.Logger.LogOnlyInFile($"The standard config was created along the path: {ConfigPath}");
            }
            catch (Exception e)
            {
                Configurations.Logger.LogOnlyInFile("Exception: " + e.Message);
            }

            return defaultConf;
        }
    }
}