﻿using System;

namespace DBUserApp.Menu.Modules;

public class Menu
{
    private readonly IList<IModule> _modules;

    public IList<IModule> Modules { get => _modules; }

    public Menu(IList<IModule> modules) 
    {
        _modules = modules;
    }

    public Menu(string cn) : this(new List<IModule>() { new UserModule(cn) })
    { }


    public void Start()
    {
        Console.WriteLine("DBUsersApp");

        Console.WriteLine("...");
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
                    break;
                }
            }
        }
    }
}