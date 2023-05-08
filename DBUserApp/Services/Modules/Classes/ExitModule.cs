using System;
namespace DBUserApp.Menu.Modules;

public class ExitModule : IModule
{
    public string Name => "Uscita";
    public string Command => "Exit";

    public void Run()
    {
        throw new ExitException("Exit");
    }
}

public class ExitException : Exception
{
    public ExitException(string message) : base(message) { }
}