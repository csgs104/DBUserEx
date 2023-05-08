﻿using Microsoft.Extensions.DependencyInjection;

using DBUserApp.IoC;
using DBUserApp.Services.Abstract;
using DBUserApp.Writers;
using DBUserApp.Menu.Modules;

using DBUserLibrary.DataBases.Abstract;
using DBUserLibrary.DataBases.Classes;
using DBUserLibrary.Entities.Classes;

using FileWriterLibrary;
using FileWriterLibrary.FileWriters;


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
var users = host.Services.GetService<IUserService>() 
            ?? throw new Exception("UserService not Found.");

var menu = host.Services.GetService<IUserService>()
            ?? throw new Exception("UserService not Found.");

Console.WriteLine("TEST for DB.");

Console.WriteLine(db.Initialize());

Console.WriteLine("TEST for USERSERVICE.");

Console.WriteLine("Get by: Id and Password.");
users.GetById(1, "Password1$");
users.GetById(2, "Password1$");
users.GetById(3, "Password1$");

Console.WriteLine("Get by: Email and Password.");
users.GetByEmail("pippipippi@mail.com", "Password1$");
users.GetByEmail("pippapippa@mail.com", "Password1$");
users.GetByEmail("pippopippo@mail.com", "Password1$");

/*
Console.WriteLine("Isert");
users?.Insert(new User("pappapappa@mail.com", "Password1$"));
users?.Insert(new User("pappapappa@mail.com", "Password1$"));
users?.Insert(new User("poppopoppo@mail.com", "Password1$"));
users?.Insert(new User("poppopoppo@mail.com", "Password1$"));
*/

User user = new User(2001, "poppopoppo@mail.com", "Password1$");
Console.WriteLine((new FileWriterUserCSV(user)).FileWrite());

new Menu().Start();

Console.WriteLine("Bye.");