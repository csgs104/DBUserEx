using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using DBUserLibrary.DataBases.Abstract;
using DBUserLibrary.DataBases.Classes;

using FileWriterLibrary;
using FileWriterLibrary.FileWriters;

using DBUserApp.Services.Abstract;
using DBUserApp.Services.Classes;
using DBUserApp.Writers;


namespace DBUserApp.IoC;

public static class Startup 
{
    public static IHostBuilder CreateHostBuilder()
    {
        // build the basepath of appsettings.json 
	    // not system dependent maybe... try on windows and tell me!
        var path = Directory.GetCurrentDirectory();
        var root = Path.GetPathRoot(path) ?? string.Empty;
        var b = Path.Combine(path.Split(Path.DirectorySeparatorChar).TakeWhile(s => !s.Equals("bin")).ToArray());
        var basepath = Path.Combine(root, b, "appsettings.json");

        // add the filepath of appsettings.json to host
        var host = Host.CreateDefaultBuilder().UseContentRoot(basepath);

        // configure host
        return host.ConfigureServices((context, service)
            => {
                    var cs = context.Configuration["ConnectionString"] ?? string.Empty;
                    service.AddSingleton<IDataBase>(_ => new UserDB(cs));
                    service.AddSingleton<IUserService>(_ => new UserService(cs));
                    // ... 
                });
    }
}

