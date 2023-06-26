namespace DBUserLibrary.Repositories.Abstract;

using DBUserLibrary.Entities.Abstract;

public interface IEntityRepository
{
    public int Insert(Entity entity);

    public int Update(Entity entity);

    public int Delete(Entity entity);
}