using System;
using DBUserLibrary.Entities.Classes;

namespace DBUserLibrary.Repositories.Abstract;

public interface IUserRepository
{
    public (bool, User?) GetById(int id);
    public (bool, User?) GetByEmail(string email);
}