namespace DBUserLibrary.Repositories.Classes;

using System.Data;
using Microsoft.Data.SqlClient;
using DBUserLibrary.Entities.Abstract;
using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;
using StringCheckerLibrary;
using StringCheckerLibrary.EmailChecker;
using StringCheckerLibrary.PasswordChecker;

public class UserRepository : EntityRepository, IUserRepository
{
    private readonly IStringChecker _emailChecker;
    private readonly IStringChecker _passwordChecker;

    public IStringChecker EmailChecker { get => _emailChecker; }
    public IStringChecker PasswordChecker { get => _passwordChecker; }

    public UserRepository(string cn, IStringChecker emailChecker, IStringChecker passwordChecker) 
	: base(cn, "[Users].[dbo].[User]") 
    {
        _emailChecker = emailChecker;
        _passwordChecker = passwordChecker;
    }

    public UserRepository(string cn) 
	: this(cn, new EmailCheckerHandler(), new PasswordCheckerHandler()) 
    { }

    private void EmailCheck(string email)
    {
        var ck = EmailChecker.Check(email);
        if (!ck.Item1) throw new Exception(ck.Item2);
    }
    
    private void PasswordCheck(string password)
    {
        var ck = PasswordChecker.Check(password);
        if (!ck.Item1) throw new Exception(ck.Item2);
    }

    public User GetById(int id, string password)
    {
        PasswordCheck(password);
        var ni = nameof(User.Id);
        var np = nameof(User.Password);
        var scmd = $@" SELECT * FROM {Table} WHERE [{ni}]=(@{ni}) AND [{np}]=(@{np})";
        var prms = new Dictionary<string, object>();
        prms.Add($"@{ni}", id);
        prms.Add($"@{np}", password);
        var entity = GetEntity(scmd, prms);
        return entity is User user? user : throw new Exception("Not an User.");
    }

    public User GetByEmail(string email, string password)
    {
        EmailCheck(email);
        PasswordCheck(password);
        var ne = nameof(User.Email);
        var np = nameof(User.Password);
        var scmd = $@"SELECT * FROM {Table} WHERE [{ne}]=(@{ne}) AND [{np}]=(@{np})";
        var prms = new Dictionary<string, object>();
        prms.Add($"@{ne}", email);
        prms.Add($"@{np}", password);
        var entity = GetEntity(scmd, prms);
        return entity is User user ? user : throw new Exception("Not an User.");
    }

    protected override Entity ReadEntity(SqlDataReader reader)
    {
        return new User(reader.GetInt32(nameof(User.Id)),
                        reader.GetString(nameof(User.Email)),
                        reader.GetString(nameof(User.Password)),
                        reader.GetDateTime(nameof(User.Date)));
    }


    public int InsertUser(User user)
    {
        EmailCheck(user.Email);
        PasswordCheck(user.Password);
        return base.Insert(user);
    }

    public int UpdateUser(User user)
    {
        EmailCheck(user.Email);
        PasswordCheck(user.Password);
        return base.Update(user);
    }

    public int DeleteUser(User user)
    {
        EmailCheck(user.Email);
        PasswordCheck(user.Password);
        return base.Update(user);
    }
}