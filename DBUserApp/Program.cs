using Microsoft.Extensions.DependencyInjection;

using DBUserApp.IoC;
using DBUserApp.Writers;
using DBUserApp.Services;
using DBUserApp.Services.Modules.Abstract;

using DBUserLibrary.DataBases.Abstract;
using DBUserLibrary.DataBases.Classes;
using DBUserLibrary.Entities.Classes;

using FileWriterLibrary;
using FileWriterLibrary.FileWriters;
using DBUserLibrary.Repositories.Abstract;

using StringCheckerLibrary;
using StringCheckerLibrary.EmailChecker;
using StringCheckerLibrary.PasswordChecker;

/*
Gestore Credenziali
username (mail), password (7 caratteri, un maiuscolo, un numero, un speciale)
utenza su db, se già presente allora messaggio di errore
se utenza salvata, allora restituire un numero di matricola (un numero)
stampare credenziali dato numero di matricola e password
creare un file con il nome che segue questo pattern matricola-yyyy-mm-gg.csv
file informazioni: matricola; username; password; data => 1; francesco.rossi@email.it; P@sswOrd!, 2023-05-04
Posso creare e salvare delle credenziali
Posso ristampare una ricevuta
*/

Console.WriteLine("Hello.");

Console.WriteLine("#### #### #### #### #### #### #### ####");

var start = Startup.CreateHostBuilder() 
            ?? throw new Exception("Not Started.");
var host = start.Build() 
           ?? throw new Exception("Host Not Found.");
var db = host.Services.GetService<IDataBase>() 
         ?? throw new Exception("DataBase not Found.");
/*
var users = host.Services.GetService<IUserRepository>() 
            ?? throw new Exception("UserRepository not Found.");

var module = host.Services.GetService<IModule>()
            ?? throw new Exception("UserModule not Found."); 
*/
var menu = host.Services.GetService<IMenu>()
            ?? throw new Exception("Menu not Found.");

Console.WriteLine($"Initializing DB: {db.Initialize()}");

Console.WriteLine("#### #### #### #### #### #### #### ####");

menu.Start();

Console.WriteLine("Bye.");