namespace DBUserApp.Services.Modules.Classes;

using DBUserApp.Services.Modules.Abstract;
using DBUserApp.Services.Modules.Exceptions;

public class ExitModule : IModule
{
    public string Name => "ExitMenu";
    public string Command => "Exit";

    public void Run()
    {
        throw new ExitException("Exit");
    }
}