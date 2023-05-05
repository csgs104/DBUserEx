using System;

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
	    => $"Id:{Id}";
    
    public virtual string ToCommaSeparatedString() 
	    => $"{Id}";
}