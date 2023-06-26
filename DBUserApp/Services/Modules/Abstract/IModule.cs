namespace DBUserApp.Services.Modules.Abstract;

public interface IModule
{
    public string Name { get;  }
    public string Command { get; }

    public void Run();
}