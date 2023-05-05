using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DBUserApp.IoC;

public static class Startup 
{
    public static IHostBuilder CreateHostBuilder()
		=> Host.CreateDefaultBuilder().ConfigureServices((context, service)
	        => {
                var cs = context.Configuration["ConnectionString"];
                /* service.Add...<...>() */
	            });
}

