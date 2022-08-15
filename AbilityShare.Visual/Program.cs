using Logic;
using printer = System.Console;

var reqMachine = new Logic.ActivePlayerData();
await reqMachine.GetPlayerData();
printer.WriteLine(reqMachine.status);
