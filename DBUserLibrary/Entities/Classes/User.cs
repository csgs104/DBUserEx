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


    public User(int id, string email, string password, DateTime date) 
	: base(id)
    {
        _email = email;
        _password = password;
        _date = date;
    }

    public User(string email, string password, DateTime date) 
	: base()
    {
        _email = email;
        _password = password;
        _date = date;
    }

    public User(int id, string email, string password) 
	: this(id, email, password, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
    { }

    public User(string email, string password)
    : this(email, password, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
    { }


    public override string ToString()
    { 
        return $"{base.ToString()}, Email:{Email}, Password:{Password}, Date:{Date.ToString("yyyy-MM-dd")}";
    }

    public override string ToCommaSeparatedString()
    { 
	    return $"{base.ToCommaSeparatedString()};{Email};{Password};{Date.ToString("yyyy-MM-dd")}";
    }

    public override Dictionary<string, object> ToDictionary()
    {
        var dic = base.ToDictionary();
        dic.Add(nameof(Email), Email);
        dic.Add(nameof(Password), Password);
        dic.Add(nameof(Date), Date);
        return dic;
    }
}