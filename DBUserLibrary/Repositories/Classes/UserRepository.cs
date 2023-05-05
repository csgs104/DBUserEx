using System;
using System.Data;
using Microsoft.Data.SqlClient;

using DBUserLibrary.Entities.Abstract;
using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;

using EmailCheckerLibrary;
using PasswordCheckerLibrary;
using System.Data.Common;

namespace DBUserLibrary.Repositories.Classes;

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

    private static void EmailCheck(string email)
    {
        var cke = EmailCheckerHandler.EmailCheck(email);
        if (!cke.Item1)
        {
            throw new Exception(cke.Item2);
        }
    }

    private static void PasswordCheck(string password)
    {
        var ckp = PasswordCheckerHandler.PasswordCheck(password);
        if (!ckp.Item1)
        {
            throw new Exception(ckp.Item2);
        }
    }


    public User GetById(int id, string password)
    {
        PasswordCheck(password);

        var cmd = $@"
            SELECT * FROM {tPersons}
            WHERE [{cId}] = {pId} AND [{cPassword}] = {pPassword}";

        var p = new Dictionary<string, object>()
        {
            { pId, id },
            { pPassword, password }
        };

        User? user = GetUser(cmd, p);
        return user is not null ? user : throw new Exception("User Not Found.");
    }

    public User GetByEmail(string email, string password)
    {
        PasswordCheck(email);
        PasswordCheck(password);

        var cmd = $@"
            SELECT * FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail} AND [{cPassword}] = {pPassword}";

        var p = new Dictionary<string, object>()
        {
            { pEmail, email  },
            { pPassword, password }
        };

        User? user = GetUser(cmd, p);
        return user is not null ? user : throw new Exception("User Not Found.");
    }

    protected User? GetUser(string command, Dictionary<string, object> parameters)
    {
        User? user = null;
        try
        {
            using var cn = new SqlConnection(Connection);
            using var cmd = new SqlCommand(command, cn);
            var sqlParameters = parameters.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
            cmd.Parameters.AddRange(sqlParameters);
            cn.Open();

            using var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleRow);
            if (reader?.Read() == true)
            {
                user = new User(reader.GetInt32(cId), reader.GetString(cEmail),
                                reader.GetString(pPassword), reader.GetDateTime(cDate));
            }
            return user;
        }
        catch (SqlException ex)
        {
            return user;
        }
    }

    public int Insert(User user)
    {
        PasswordCheck(user.Email);
        PasswordCheck(user.Password);

        var command = $@"
            INSERT INTO {tPersons} ([{cEmail}],[{cPassword}],[{cDate}])
            VALUES ( {pEmail}, {pPassword}, {pDate} )";

        var cmd = $@"
            SELECT * FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail} AND [{cPassword}] = {pPassword} AND [{cDate}] = {pDate}";

        var p = new Dictionary<string, object>()
        {
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };

        return TryExecute(command, p) ? GetUser(cmd, p)!.Id : throw new Exception($"{Insert} Failed.");
    }

    public int Update(User user)
    {
        PasswordCheck(user.Email);
        PasswordCheck(user.Password);

        var command = $@"
            UPDATE {tPersons}
            SET 
            [{cEmail}] = {pEmail},
            [{cPassword}] = {pPassword},
            [{cDate}] = {pDate}
            WHERE [{cId}] = {pId};";

        var cmd = $@"
            SELECT * FROM {tPersons} 
            WHERE [{cId}] = {pId} AND [{cEmail}] = {pEmail} AND [{cPassword}] = {pPassword} AND [{cDate}] = {pDate}";

        var p = new Dictionary<string, object>()
        {
            { pId, user.Id },
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };
        return TryExecute(command, p) ? GetUser(cmd, p)!.Id : throw new Exception($"{Update} Failed.");
    }

    public int Delete(User user)
    {
        PasswordCheck(user.Email);
        PasswordCheck(user.Password);

        var command = $@"
            DELETE FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail} AND [{cPassword}] = {pPassword} AND [{cDate}] = {pDate};";

        var cmd = $@"
            SELECT * FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail} AND [{cPassword}] = {pPassword} AND [{cDate}] = {pDate}";

        var p = new Dictionary<string, object>()
        {
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };
        return TryExecute(command, p) ? 1 : 0;
    }
}