namespace DBUserLibrary.Entities.Abstract;

public abstract class Entity : IEntity
{
    private readonly int _id;

    public int Id { get => _id; }

    public Entity(int id = default)
    {
        _id = id;
    }

    public override string ToString()
    {
        return $"Id:{Id}";
    }

    public virtual string ToCommaSeparatedString()
    {
        return $"{Id}";
    }

    public virtual Dictionary<string, object> ToDictionary()
    {
        var dic = new Dictionary<string, object>();
        dic.Add(nameof(Id), Id);
        return dic;
    }
}