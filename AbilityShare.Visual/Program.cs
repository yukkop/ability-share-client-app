using AbilityShare.Logic.Services;
using AbilityShare.Logic.Configurations;
using AbilityShare.Visual.Layout;
using printer = System.Console;

Logger.Log("> Start new seance:"); // Первый лог инициализирует конфиг | test функции Config.Load()

var preferences = Preferences.MainPreferences; // Только ради того что бы протестировать вызов настроек (Preferences.Load())

Logger.Log("==> Test Translater:"); // проверка работоспособности языковой системы
Logger.Log($"> Значение которое есть в файле en.yaml : {Translator.Translate("valid-value")}");
Logger.Log($"> Значение которого нет в файле en.yaml : {Translator.Translate("invalid-value")}");
Logger.Log("==><==><==><==");

await Test();

async Task Test()
{
    var reqMachine = new PlayerDataService();
    await reqMachine.GetPlayerData();
    printer.WriteLine(reqMachine.status);
}
