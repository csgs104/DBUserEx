using System;
using DBUserLibrary.Entities.Interfaces;

namespace DBUserLibrary.Entities.Classes;

public class User : IEntity
{ 
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime Date { get; set; }

    public User() 
    { }

    public User(int id, string email, string password, DateTime date)
    {
        Id = id;
        Email = email;
        Password = password;
        Date = date;
    }

    public override string ToString()
        => $"Id:{Id}, User:{Email}, Password:{Password}, Date:{Date}";

    public string ToCommaSeparatedString() => $"{Id},{Email},{Password},{Date}";
}