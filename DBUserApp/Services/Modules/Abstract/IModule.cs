using System;

namespace DBUserApp.Menu.Modules;

public interface IModule
{
    public string Name { get;  }
    public string Command { get; }

    public void Run();
}

