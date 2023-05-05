using System;
using DBUserLibrary.Entities.Abstract;

namespace DBUserLibrary.Entities.Classes;

public class User : Entity
{
    private readonly string _email = null!;
    private readonly string _password = null!;
    private readonly DateTime _date;

    public string Email { get => _email; }
    public string Password { get => _password; }
    public DateTime Date { get => _date; }

    public User(int id, string email, string password, DateTime date) : base(id)
    {
        _email = email;
        _password = password;
        _date = date;
    }

    public User(string email, string password, DateTime date) : base()
    {
        _email = email;
        _password = password;
        _date = date;
    }

    public User() : this(string.Empty, string.Empty, default)
    { }

    public override string ToString()
        => $"{base.ToString()}, User:{Email}, Password:{Password}, Date:{Date.ToString("yyyy-MM-dd")}";

    public override string ToCommaSeparatedString() 
	    => $"{base.ToCommaSeparatedString()},{Email},{Password},{Date.ToString("yyyy-MM-dd")}";
}