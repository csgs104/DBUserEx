using Microsoft.Extensions.DependencyInjection;

using DBUserApp.IoC;

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
// var variable = host.Services.GetService<...>();


Console.WriteLine("Bye.");