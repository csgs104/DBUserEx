using System;
using System.Data;
using Microsoft.Data.SqlClient;

using DBUserLibrary.Entities.Abstract;
using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;

using EmailCheckerLibrary;
using PasswordCheckerLibrary;

namespace DBUserLibrary.Classes.Repositories;

public class UserRepository : BaseRepository, IUserRepository
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

        return GetUser<int>(cmd, pId, id);
    }

    public (bool, User?) GetByEmail(string email)
    {
        var cmd = $@"
            SELECT * FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail}";

        return GetUser<string>(cmd, pEmail, email);
    }

    private (bool, User?) GetUser<Type>(string command, string parameter, Type value)
    {
        User? user = null;
        try
        {
            using var cn = new SqlConnection(Connection);
            using var cmd = new SqlCommand(command, cn);
            cmd.Parameters.AddWithValue(parameter, value);
            cn.Open();

            using var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleRow);
            if (reader?.Read() == true)
            {
                user = new User(reader.GetInt32(cId), reader.GetString(cEmail), 
		                        reader.GetString(pPassword), reader.GetDateTime(cDate));
            }
            return (true, user);
        }
        catch (SqlException ex)
        {
            return (false, user);
        }
    }

    private static void CheckUser(User user) 
    {
        var cke = EmailCheckerHandler.EmailCheck(user.Email);
        if (!cke.Item1)
        {
            throw new Exception(cke.Item2);
        }

        var ckp = PasswordCheckerHandler.PasswordCheck(user.Password);
        if (!ckp.Item1)
        {
            throw new Exception(ckp.Item2);
        }
    }

    public (bool, int) Insert(User user)
    {
        CheckUser(user);

        var command = $@"
            INSERT INTO {tPersons} ([{cEmail}],[{cPassword}],[{cDate}])
            VALUES ( {pEmail}, {pPassword}, {pDate} )";

        var p = new Dictionary<string, object>()
        {
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };
	    return TryExecute(command, p);
    }

    public (bool, int) Update(User user)
    {
        CheckUser(user);

        var command = $@"
            UPDATE {tPersons}
            SET 
            [{cEmail}] = {pEmail},
            [{cPassword}] = {pPassword},
            [{cDate}] = {pDate}
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

    public (bool, int) Delete(User user)
    {
        CheckUser(user);

        var command = $@"
            DELETE FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail} AND [{cPassword}] = {pPassword} AND [{cDate}] = {pDate};";

        var p = new Dictionary<string, object>()
        {
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };
        return TryExecute(command, p);
    }
}