using System;
using DBUserLibrary.Entities.Classes;

namespace DBUserLibrary.Repositories.Abstract;

public interface IUserRepository
{
    public User GetById(int id, string password);
    public User GetByEmail(string email, string password);
}