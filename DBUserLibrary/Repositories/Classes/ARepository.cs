using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DBUserLibrary.Repositories.Classes;

public abstract class ARepository
{
    private readonly string _connection;

    public string Connection { get => _connection; }

    public ARepository(string connection)
	{
        _connection = connection;
	}

    protected (bool, int) TryExecute(string command, Dictionary<string, object> parameters)
    {
        try
        {
            using var cn = new SqlConnection(_connection);
            using var cmd = new SqlCommand(command, cn);
            var sqlParameters = parameters.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
            cmd.Parameters.AddRange(sqlParameters);

            cn.Open();
            return (true, cmd.ExecuteNonQuery());
        }
        catch (SqlException ex)
        {
            return (false, 0);
        }
    }
}