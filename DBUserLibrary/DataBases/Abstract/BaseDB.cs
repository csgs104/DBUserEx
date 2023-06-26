namespace DBUserLibrary.DataBases.Abstract;

using Microsoft.Data.SqlClient;

public abstract class BaseDB : IDataBase
{
    private readonly string _connection;

    public string Connection { get => _connection; }

    public BaseDB(string connection)
    {
        _connection = connection;
    }

    public abstract string CreateDataBase();

    public abstract string CreateTables();

    public abstract string InsertTables();

    public bool Initialize()
    {
        try
        {
            using var cn = new SqlConnection(Connection);
            using var createDB = new SqlCommand(CreateDataBase(), cn);
            using var createTables = new SqlCommand(CreateTables(), cn);
            using var insertTables = new SqlCommand(InsertTables(), cn);
            cn.Open();
            createDB.ExecuteNonQuery();
            createTables.ExecuteNonQuery();
            insertTables.ExecuteNonQuery();
            return true;
        }
        catch (SqlException ex)
        {
            // Console.WriteLine(ex.Message);
            return false;
        }
    }
}