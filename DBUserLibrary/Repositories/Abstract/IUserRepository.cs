using System;
using DBUserLibrary.Entities.Classes;

namespace DBUserLibrary.Repositories.Abstract;

public interface IUserRepository
{
    public User GetById(int id, string password);
    public User GetByEmail(string email, string password);

    public int Insert(User user);
    public int Update(User user);
    public int Delete(User user);
}