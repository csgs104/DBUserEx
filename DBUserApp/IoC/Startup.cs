using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using DBUserLibrary.DataBases.Abstract;
using DBUserLibrary.DataBases.Classes;
using DBUserLibrary.Repositories.Abstract;
using DBUserLibrary.Repositories.Classes;

using FileWriterLibrary;
using FileWriterLibrary.FileWriters;

using DBUserApp.Services.Modules.Abstract;
using DBUserApp.Services.Modules.Classes;
using DBUserApp.Services;
using DBUserApp.Writers;


namespace DBUserApp.IoC;

public static class Startup 
{
    public static IHostBuilder CreateHostBuilder()
    {
        // build the basepath of appsettings.json 
        // not system dependent maybe... try on windows and tell me!
        // var path = Directory.GetCurrentDirectory();
        // var root = Path.GetPathRoot(path) ?? string.Empty;
        // var b = Path.Combine(path.Split(Path.DirectorySeparatorChar).TakeWhile(s => !s.Equals("bin")).ToArray());
        // var basepath = Path.Combine(root, b);

        // add the filepath of appsettings.json to host
        // var host = Host.CreateDefaultBuilder().UseContentRoot(basepath);

        // add the filepath of appsettings.json to host
        var host = Host.CreateDefaultBuilder();

        // configure host
        return host.ConfigureServices((context, service)
            => {

		        var cn = context.Configuration["ConnectionString"] ?? string.Empty;

                var db = new UserDB(cn);
                var userRepo = new UserRepository(cn);
                // var ...Repo = new ...Repository(cn);
                var userMod = new UserModule(userRepo);
                // var ...Mod = new ...Module(...Repo);

                var listMods = new List<IModule>();
                listMods.Add(userMod);
                // listMods.Add(...);
                // listMods.Add(...);
                var menu = new Menu(listMods);

                // adding services
                service.AddSingleton<IDataBase>(_ => db);

                service.AddSingleton<IUserRepository>(_ => userRepo);
                // service.AddSingleton<I...Repository>(_ => ...Repo);
                service.AddSingleton<IUserModule>(_ => userMod);
                // service.AddSingleton<I...Module>(_ => ...Mod);

                service.AddSingleton<IMenu>(_ => menu);
                // ... 
            });
    }
}