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
        // build the filepath of appsettings.json
        var basepath = @"/Users/stefanocasagrande/Desktop/DBUserEx/DBUserApp";
        var filepath = Path.Combine(basepath, "appsettings.json");

        // add the filepath of appsettings.json to host
        var host = Host.CreateDefaultBuilder().UseContentRoot(basepath);

        // configure host
        return host.ConfigureServices((context, service)
            => {
                    var cs = context.Configuration["ConnectionString"];
                    service.AddSingleton<IDataBase>(_ => new UserDB(cs!));
                    service.AddSingleton<IUserService>(_ => new UserService(cs!));
                });
    }
}

