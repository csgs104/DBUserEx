using System;

namespace DBUserLibrary.Entities.Interfaces;

public interface IEntity
{
    public int Id { get; set; }

    public string ToString();
    public string ToCommaSeparatedString();
}