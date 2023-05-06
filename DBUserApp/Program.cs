using Microsoft.Extensions.DependencyInjection;

using DBUserApp.IoC;
using DBUserApp.Services.Abstract;
using DBUserApp.Writers;

using DBUserLibrary.DataBases.Abstract;
using DBUserLibrary.DataBases.Classes;
using DBUserLibrary.Entities.Classes;

using FileWriterLibrary;

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

var host = Startup.CreateHostBuilder().Build();
var db = host.Services.GetService<IDataBase>();
var users = host.Services.GetService<IUserService>();

Console.WriteLine(db?.Initialize());

users?.GetById(1, "Password1$");
users?.GetById(2, "Password1$");
users?.GetById(3, "Password1$");

users?.GetByEmail("pippipippi@mail.com", "Password1$");
users?.GetByEmail("pippapippa@mail.com", "Password1$");
users?.GetByEmail("pippopippo@mail.com", "Password1$");

users?.Insert(new User("pappapappa@mail.com", "Password1$", new DateTime(2023, 05, 06)));
users?.Insert(new User("pappapappa@mail.com", "Password1$", new DateTime(2023, 05, 06)));
users?.Insert(new User("poppopoppo@mail.com", "Password1$", new DateTime(2023, 05, 06)));
users?.Insert(new User("poppopoppo@mail.com", "Password1$", new DateTime(2023, 05, 06)));

/*
FileWriter fv = new FileWriterUserCSV(new User("poppopoppo@mail.com", "Password1$", new DateTime(2023, 05, 06)));
*/

Console.WriteLine("Bye.");