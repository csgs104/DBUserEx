using Microsoft.Extensions.DependencyInjection;

using DBUserApp.IoC;

Console.WriteLine("Hello.");

var host = Startup.CreateHostBuilder().Build();
// var variable = host.Services.GetService<...>();


Console.WriteLine("Bye.");