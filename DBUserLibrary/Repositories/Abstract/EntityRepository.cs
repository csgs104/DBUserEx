using System;
using System;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

using DBUserLibrary.Entities.Abstract;
using DBUserLibrary.Entities.Classes;

namespace DBUserLibrary.Repositories.Abstract;

public abstract class EntityRepository : BaseRepository, IEntityRepository
{
    private readonly string _table;

    public string Table { get => _table; }

    public EntityRepository(string cn, string table) : base(cn) 
    {
        _table = table;
    }


    protected Entity GetEntity(string cmd, Dictionary<string, object> prms)
    {
        Entity? entity = null;
        using var sqlcn = new SqlConnection(Connection);
        using var sqlcmd = new SqlCommand(cmd, sqlcn);
        sqlcmd.Parameters.AddRange(SqlParameters(cmd, prms));
        sqlcn.Open();

        using var reader = sqlcmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleRow) 
	                       ?? throw new NullReferenceException("SqlDataReader is null.");

        if (reader.Read() == true)
        {
            entity = ReadEntity(reader);
        }

        return entity 
	           ?? throw new EntityNotFoundException("Entity Not Found.");
    }

    protected abstract Entity ReadEntity(SqlDataReader reader);


    public virtual int Insert(Entity entity)
    {
        var dic = entity.ToDictionary();
        dic.Remove(nameof(Entity.Id));

        var c = dic.Aggregate("*", (acc, s) => acc + $"[{s.Key}]*").Trim('*').Replace("*", ",");
        var p = dic.Aggregate("*", (acc, s) => acc + $"@{s.Key}*").Trim('*').Replace("*", ",");
        var w = dic.Aggregate("*", (acc, s) => acc + $"[{s.Key}]=(@{s.Key})*").Trim('*').Replace("*", "AND");

        var icmd = $@"INSERT INTO {Table} ({c}) VALUES ({p})";
        var scmd = $@"SELECT * FROM {Table} WHERE {w}";

        var prms = dic.ToDictionary(e => $"@{e.Key}", e => e.Value);

        return TryExecute(icmd, prms)
               ? GetEntity(scmd, prms).Id
               : throw new Exception($"{nameof(Insert)} Failed.");
    }


    public virtual int Update(Entity entity)
    {
        var dic = entity.ToDictionary();
        dic.Remove(nameof(Entity.Id));

        var w = dic.Aggregate("*", (acc, s) => acc + $"[{s.Key}]=(@{s.Key})*").Trim('*').Replace("*", "AND");
        var u = dic.Aggregate("*", (acc, s) => acc + $"[{s.Key}]=(@{s.Key})*").Trim('*').Replace("*", ",");

        var ucmd = $@"UPDATE {Table} SET {u} WHERE [{nameof(Entity.Id)}]=({entity.Id})";
        var scmd = $@"SELECT * FROM {Table} WHERE [{nameof(Entity.Id)}]=({entity.Id})AND{w}";

        var prms = dic.ToDictionary(e => $"@{e.Key}", e => e.Value);

        return TryExecute(ucmd, prms)
               ? GetEntity(scmd, prms).Id
               : throw new Exception($"Update Failed.");
    }


    public virtual int Delete(Entity entity)
    {
        var dic = entity.ToDictionary();
        dic.Remove(nameof(Entity.Id));

        var w = dic.Aggregate("*", (acc, s) => acc + $"[{s.Key}]=(@{s.Key})*").Trim('*').Replace("*", "AND");

        var dcmd = $@"DELETE FROM {Table} WHERE [{nameof(Entity.Id)}]=({entity.Id})AND{w}";

        var prms = dic.ToDictionary(e => $"@{e.Key}", e => e.Value);

        return TryExecute(dcmd, prms)
               ? entity.Id
               : throw new Exception($"{nameof(Delete)} Failed.");
    }
}

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
}