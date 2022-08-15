using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AbilityShare.Logic.Configurations;

public class Preferences // TODO разделить этот класс на статический класс и модель с настройками (ConfigModel)
{
    private static Preferences? _mainPreferences;

    /// <summary>
    /// Singleton настроек
    /// </summary>
    public static Preferences MainPreferences => _mainPreferences ??= Preferences.Load(); // Если конфиг еще не создан (_mainPreferences == null)
                                                                                          // То вызываеться метод Config.Load()

    public Preferences() { }

    /// <summary>
    /// Стандартные настройки<para/>
    /// Для первого запуска программы.
    /// </summary>
    /// <returns></returns>
    public static Preferences GetDefault() { return new(); }


    /// <summary>
    /// Загружает настройки
    /// Если файла по пути из поля Config.MainConfig.PreferencesPath нет, создаст новый конфиг
    /// </summary>
    /// <returns>GetDefault() - если конфига не оказалось по пути из поля Config.MainConfig.PreferencesPath</returns>
    public static Preferences Load()
    {
        Configurations.Logger.LogOnlyInFile("Looking for an existing preferences file.");
        FileInfo fileInfo = new FileInfo(Config.MainConfig.PreferencesPath);
        if (fileInfo.Exists)
        {
            Configurations.Logger.LogOnlyInFile("Preferences found!");
            Configurations.Logger.LogOnlyInFile("Read preferences");

            using (var input = File.OpenText(Config.MainConfig.PreferencesPath))
            {
                var deserializerBuilder = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance);

                var deserializer = deserializerBuilder.Build();

                Configurations.Logger.LogOnlyInFile("Deserializing the preferences");
                var result = deserializer.Deserialize<Preferences>(input);
                return result;
            }
        }
        else
        {
            Configurations.Logger.LogOnlyInFile("Preferences not found!");
            Configurations.Logger.LogOnlyInFile("Create standard preferences.");

            Preferences defaultConf = Preferences.GetDefault();

            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yaml = serializer.Serialize(defaultConf);

            try
            {
                StreamWriter sw = new StreamWriter(Config.MainConfig.PreferencesPath);
                sw.WriteLine(yaml);
                sw.Close();

                Configurations.Logger.LogOnlyInFile($"The preferences config was created along the path: {Config.MainConfig.PreferencesPath}");
            }
            catch (Exception e)
            {
                Configurations.Logger.LogOnlyInFile("Exception: " + e.Message);
            }

            return defaultConf;
        }
    }
}