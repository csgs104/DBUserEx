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

Console.WriteLine("TEST for HOST:");

var start = Startup.CreateHostBuilder() 
            ?? throw new Exception("Not Started.");
var host = start.Build() 
           ?? throw new Exception("Host Not Found.");
var db = host.Services.GetService<IDataBase>() 
         ?? throw new Exception("DataBase not Found.");
var users = host.Services.GetService<IUserRepository>() 
            ?? throw new Exception("UserRepository not Found.");

var module = host.Services.GetService<IModule>()
            ?? throw new Exception("UserModule not Found.");

var menu = host.Services.GetService<Menu>()
            ?? throw new Exception("Menu not Found.");

/*
Console.WriteLine("TEST for DB.");
Console.WriteLine(db.Initialize());

Console.WriteLine("TEST for USERSERVICE.");
Console.WriteLine("Get by: Id and Password.");
Console.WriteLine(users.GetById(1, "Password1$"));
Console.WriteLine("Get by: Email and Password.");
Console.WriteLine(users.GetByEmail("pippipippi@mail.com", "Password1$"));


Console.WriteLine("Isert");
users?.Insert(new User("pappapappa@mail.com", "Password1$"));
*/

Console.WriteLine("TEST for StringChecker.");
var pch = (new PasswordCheckerHandler()).Check("dPg777$");
var ech = (new EmailCheckerHandler()).Check("dPg777$@mail.com");
Console.WriteLine($"{pch.Item1}-{pch.Item2}");
Console.WriteLine($"{ech.Item1}-{ech.Item2}");

/*
Console.WriteLine("TEST for USERTOCSV.");
User user = new User(20, "poppopoppo@mail.com", "Password1$");
Console.WriteLine((new FileWriterUserCSV(user)).FileWrite());

Console.WriteLine("TEST for MENU.");

// menu.Start();
*/

Console.WriteLine("Bye.");