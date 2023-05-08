using System;

using DBUserApp.Services.Modules.Abstract;
using DBUserApp.Services.Modules.Exceptions;

namespace DBUserApp.Services.Modules.Classes;

public class ExitModule : IModule
{
    public string Name => "ExitMenu";
    public string Command => "Exit";

    public void Run()
    {
        throw new ExitException("Exit");
    }
}