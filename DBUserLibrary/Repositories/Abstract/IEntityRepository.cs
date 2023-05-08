using System;

using DBUserLibrary.Entities.Abstract;


namespace DBUserLibrary.Repositories.Abstract;

public interface IEntityRepository
{
    public int Insert(Entity entity);

    public int Update(Entity entity);

    public int Delete(Entity entity);
}