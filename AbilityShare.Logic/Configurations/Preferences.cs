using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using AbilityShare.Logic.Configurations.Models;

namespace AbilityShare.Logic.Configurations;

public class Preferences // TODO разделить этот класс на статический класс и модель с настройками (ConfigModel)
{
    private static Preferences? _mainPreferences;

    /// <summary>
    /// Singleton настроек
    /// </summary>
    public static Preferences MainPreferences => _mainPreferences ??= Preferences.Load(); // Если конфиг еще не создан (_mainPreferences == null)
                                                                                          // То вызываеться метод Config.Load()
    public LanguageModel Language;

    public Preferences()
    {
        Language = new LanguageModel
        {
            LayoutsFolderPath = "layouts/"
        };
    }

    /// <summary>
    /// Стандартные настройки<para/>
    /// Для первого запуска программы.
    /// </summary>
    /// <returns></returns>
    public static Preferences GetDefault()
    {
        var preferences = new Preferences();
        if (!Directory.Exists(preferences.Language.LayoutsFolderPath))
        {
            Configurations.Logger.LogError($"Layout directory not exists [{Path.GetFullPath(preferences.Language.LayoutsFolderPath)}]");
            preferences.Language.CurrentLayout = "en";
            Directory.CreateDirectory(preferences.Language.LayoutsFolderPath);
            File.Create(preferences.Language.LayoutsFolderPath + preferences.Language.CurrentLayout + ".yaml");
        }
        else
        {
            string[] fileNames = Directory.GetFiles(preferences.Language.LayoutsFolderPath, "*.yaml");
            if (fileNames.Length == 0)
            {
                preferences.Language.CurrentLayout = "en";
                File.Create(preferences.Language.LayoutsFolderPath + preferences.Language.CurrentLayout + ".yaml");
            }
            else
            {
                preferences.Language.CurrentLayout = fileNames.First().Split(".").First();
            }
        }

        return fillCalculatedFields(preferences);
    }

    /// <summary>
    /// Заполняет те настройки которые вычисляються тем либо иным образо <para/>
    /// А не достаються из файла настроек
    /// </summary>
    /// <returns></returns>
    private static Preferences fillCalculatedFields(Preferences preferences)
    {
        string[] fileNames = Directory.GetFiles(preferences.Language.LayoutsFolderPath, "*.yaml");
        preferences.Language.Layouts = fileNames.Select(fileName => fileName.Split(@"/").Last().Split(".").First()).ToArray();

        return preferences;
    }

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
                return fillCalculatedFields(result);
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