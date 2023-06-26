namespace DBUserLibrary.DataBases.Abstract;

public interface IDataBase
{
    public string CreateDataBase();
    public string CreateTables();
    public string InsertTables();
    public bool Initialize();
}