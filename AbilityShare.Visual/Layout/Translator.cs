using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Collections;
using AbilityShare.Logic.Configurations;

namespace AbilityShare.Visual.Layout;

public static class Translator
{
    // TODO unit tests?
    public static string Translate(string key) // qualche pezzo di merda
    {
        string result;
        var deserializer = new Deserializer();
        try
        {
            string path = Preferences.MainPreferences.Language.LayoutsFolderPath + Preferences.MainPreferences.Language.CurrentLayout + ".yaml";
            var dictionary = deserializer.Deserialize<Dictionary<string, string>>(File.OpenText(path));
            result = dictionary[key];
        }
        catch
        {
            result = key;
        }
        return result;
    }
}