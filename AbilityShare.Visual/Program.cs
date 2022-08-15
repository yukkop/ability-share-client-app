using AbilityShare.Logic.Services;
using AbilityShare.Logic.Configurations;
using printer = System.Console;

Logger.Log("> Start new seance:"); // Первый лог инициализирует конфиг | test функции Config.Load()

var preferences = Preferences.MainPreferences; // Только ради того что бы протестировать вызов настроек (Preferences.Load())

await Test();

async Task Test()
{
    var reqMachine = new PlayerDataService();
    await reqMachine.GetPlayerData();
    printer.WriteLine(reqMachine.status);
}
