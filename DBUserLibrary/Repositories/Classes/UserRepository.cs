using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

using DBUserLibrary.Entities.Abstract;
using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;

using StringCheckerLibrary;
using StringCheckerLibrary.EmailChecker;
using StringCheckerLibrary.PasswordChecker;


namespace DBUserLibrary.Repositories.Classes;

public class UserRepository : BaseRepository, IUserRepository
{
    const string cId = nameof(User.Id);
    const string cEmail = nameof(User.Email);
    const string cPassword = nameof(User.Password);
    const string cDate = nameof(User.Date);

    const string tPersons = "[Users].[dbo].[User]";

    const string pId = "@" + cId;
    const string pEmail = "@" + cEmail;
    const string pPassword = "@" + cPassword;
    const string pDate = "@" + cDate;

    private static StringCheckerHandler eckh = new EmailCheckerHandler();
    private static StringCheckerHandler pckh = new PasswordCheckerHandler();


    public UserRepository(string connection) : base(connection)
    { }


    private static void Check(string email, StringCheckerHandler sckh)
    {
        var ck = sckh.Check(email);
        if (!ck.Item1)
        {
            throw new Exception(ck.Item2);
        }
    }

    private static void EmailCheck(string email) => Check(email, eckh);

    private static void PasswordCheck(string password) => Check(password, pckh);


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
                                reader.GetString(cPassword), reader.GetDateTime(cDate));
            }
            return user;
        }
        catch (SqlException ex)
        {
            // Console.WriteLine(ex.Message);
            return user;
        }
    }

    public int Insert(User user)
    {
        EmailCheck(user.Email);
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

        return TryExecute(command, p) 
	           ? GetUser(cmd, p)!.Id 
	           : throw new Exception($"{nameof(Insert)} Failed.");
    }

    public int Update(User user)
    {
        EmailCheck(user.Email);
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
        return TryExecute(command, p) 
	           ? GetUser(cmd, p)!.Id 
	           : throw new Exception($"{Update} Failed.");
    }

    public int Delete(User user)
    {
        EmailCheck(user.Email);
        PasswordCheck(user.Password);

        var command = $@"
            DELETE FROM {tPersons} 
            WHERE [{cEmail}] = {pEmail} AND [{cPassword}] = {pPassword} AND [{cDate}] = {pDate};";

        var p = new Dictionary<string, object>()
        {
            { pId, user.Id },
            { pEmail, user.Email },
            { pPassword, user.Password },
            { pDate, user.Date }
        };
        return TryExecute(command, p) 
	           ? user.Id 
	           : throw new Exception($"{nameof(Delete)} Failed.");
    }
}