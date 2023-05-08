using System;

using DBUserApp.Services.Modules;
using DBUserApp.Services.Modules.Abstract;
using DBUserApp.Services.Modules.Classes;
using DBUserApp.Services.Modules.Exceptions;


namespace DBUserApp.Services;

public class Menu : IMenu
{
    private readonly IList<IModule> _modules;

    public IList<IModule> Modules { get => _modules; }


    public Menu(IList<IModule> modules) 
    {
        _modules = modules;
        _modules.Add(new ExitModule());
    }


    public void Start()
    {
        Console.WriteLine("#### #### #### #### #### #### #### ####");
        Console.WriteLine("DBUsersApp");
        while (true)
        {
            Console.WriteLine("MENU:");

            foreach (var modules in Modules)
            {
                Console.WriteLine($"[{modules.Command}]:\t{modules.Name}");
            }

            Console.Write("Your Choice: ");
            var choice = Console.ReadLine();
            var service = Modules.SingleOrDefault(x => x.Command.Equals(choice));

            if (service is null)
            {
                Console.WriteLine($"Not Valid Choice: {choice}.");
            }

            else
            {
                try
                {
                    Console.Write("RUN: ");
                    service.Run();
                }
                catch (ExitException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("#### #### #### #### #### #### #### ####");
                    break;
                }
            }
        }
    }
}