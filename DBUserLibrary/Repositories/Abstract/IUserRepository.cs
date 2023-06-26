namespace DBUserLibrary.Repositories.Abstract;

using DBUserLibrary.Entities.Classes;

public interface IUserRepository : IEntityRepository
{
    public User GetById(int id, string password);

    public User GetByEmail(string email, string password);

    public int InsertUser(User user);

    public int UpdateUser(User user);

    public int DeleteUser(User user);
}