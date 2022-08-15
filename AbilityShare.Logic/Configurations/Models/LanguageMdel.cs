namespace AbilityShare.Logic.Configurations.Models;

/// <summary>
/// Модель содержащая настройки языка / лаяута
/// </summary>
public struct LanguageModel
{
    public string LayoutsFolderPath;

    public string CurrentLayout;

    [NonSerialized] // TODO я ебал тут что то не пашет
    public string[] Layouts;
}
