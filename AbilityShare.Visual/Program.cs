using AbilityShare.Logic.Services;
using printer = System.Console;

var reqMachine = new PlayerDataService();
`await reqMachine.GetPlayerData();
printer.WriteLine(reqMachine.status);
