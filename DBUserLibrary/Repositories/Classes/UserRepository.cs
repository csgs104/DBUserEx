using System;
using System.Data;
using Microsoft.Data.SqlClient;

using DBUserLibrary.Entities.Interfaces;
using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Interfaces;
using DBUserLibrary.Repositories.Classes;

namespace DBUserLibrary.Classes.Repositories;

public class UserRepository : ARepository, IUserRepository
{
    const string cId = nameof(User.Id);
    const string cEmail = nameof(User.Email);
    const string cPassword = nameof(User.Password);
    const string cDate = nameof(User.Date);

    const string tPersons = "[Persons].[dbo].[Person]";

    const string pId = "@" + cId;
    const string pEmail = "@" + cEmail;
    const string pPassword = "@" + cPassword;
    const string pDate = "@" + cDate;

    public UserRepository(string connection) : base(connection)
    { }

    public (bool, User?) GetById(int id)
	{
        var cmd = $@"
                SELECT * FROM {tPersons}
                WHERE [{cId}] = {pId}";

        return GetUser(cmd, pId, id);
    }

    public (bool, User?) GetByEmail(string email)
    {
        var cmd = $@"
            SELECT * FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail}";

        return GetUser(cmd, pEmail, email);
    }

    private (bool, User?) GetUser<T>(string command, string parameter, T value)
    {
        try
        {
            using var cn = new SqlConnection(Connection);
            using var cmd = new SqlCommand(command, cn);
            cmd.Parameters.AddWithValue(parameter, value);

            cn.Open();
            using var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleRow);
            
	        User user = null;
            if (reader?.Read() == true)
            {
                user = new User()
                {
                    Id = reader.GetInt32(cId),
                    Email = reader.GetString(cEmail),
                    Password = reader.GetString(pPassword),
                    Date = reader.GetDateTime(cDate)
                };
            }
            return (true, user);
        }
        catch (SqlException ex)
        {
            // Console.Error.WriteLine(ex);
            return (false, null);
        }
    }

    public (bool, int) Insert(User user)
    {
        var command = $@"
            INSERT INTO {tPersons} ([{cId}],[{cEmail}],[{cPassword}],[{cDate}])
            VALUES ( {pId}, {pEmail}, {pPassword}, {pDate} )";

        var p = new Dictionary<string, object>()
        {
            { pId, user.Id },
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };
	    return TryExecute(command, p);
    }

    public (bool, int) Update(User user)
    {
        var command = $@"
            UPDATE {tPersons}
            SET 
            [{cEmail}] = {pEmail},
            [{cPassword}] = {pPassword},
            [{cDate}] = [{pDate}]
            WHERE [{cId}] = {pId};";

        var p = new Dictionary<string, object>()
        {
            { pId, user.Id },
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };
        return TryExecute(command, p);
    }

    public (bool, int) Delete(User person)
    {
        var command = $@"
            DELETE FROM {tPersons} 
            WHERE [{cId}] = {pId};";

        var p = new Dictionary<string, object>()
        {
            { pId, person.Id }
        };
        return TryExecute(command, p);
    }

    public (bool, int) Delete(int id)
    {
        var command = $@"
            DELETE FROM {tPersons} 
            WHERE [{cId}] = {pId};";

        var p = new Dictionary<string, object>()
        {
            { pId, id }
        };
        return TryExecute(command, p);
    }

    public (bool, int) Delete(string email)
    {
        var command = $@"
            DELETE FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail};";

        var p = new Dictionary<string, object>()
        {
            { pEmail, email }
        };
        return TryExecute(command, p);
    }
}