namespace DBUserLibrary.Repositories.Abstract;

using System.Data;
using Microsoft.Data.SqlClient;

public abstract class BaseRepository
{
    private readonly string _connection;

    public string Connection { get => _connection; }

    public BaseRepository(string cn)
	{
        _connection = cn;
	}

    protected SqlParameter[] SqlParameters(string cmd, Dictionary<string, object> prms)
    {
        return prms.Select(x => new SqlParameter(x.Key, x.Value)).ToArray() 
	    ?? throw new NullReferenceException("Parameters Not Found.");
    }

    protected int Execute(string cmd, Dictionary<string, object> prms)
    {
        using var sqlcn = new SqlConnection(Connection);
        using var sqlcmd = new SqlCommand(cmd, sqlcn);
        sqlcmd.Parameters.AddRange(SqlParameters(cmd, prms));
        sqlcn.Open();

        return sqlcmd.ExecuteNonQuery();
    }

    protected bool TryExecute(string cmd, Dictionary<string, object> prms, out int res)
    {
        try
        {
            res = Execute(cmd, prms);
            return true;
        }
        catch (Exception ex)
        {
            res = 0;
            return false;
        }
    }

    protected bool TryExecute(string cmd, Dictionary<string, object> prms)
    {
        return TryExecute(cmd, prms, out _);
    }
}