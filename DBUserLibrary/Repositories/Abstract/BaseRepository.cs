using System;
using System.Data;
using DBUserLibrary.Entities.Classes;
using Microsoft.Data.SqlClient;

namespace DBUserLibrary.Repositories.Abstract;

public abstract class BaseRepository
{
    private readonly string _connection;

    public string Connection { get => _connection; }

    public BaseRepository(string connection)
	{
        _connection = connection;
	}

    protected bool TryExecute(string command, Dictionary<string, object> parameters)
    {
        try
        {
            using var cn = new SqlConnection(Connection);
            using var cmd = new SqlCommand(command, cn);
            var sqlParameters = parameters.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
            cmd.Parameters.AddRange(sqlParameters);
            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }
        catch (SqlException ex)
        {
            return false;
        }
    }
}