using System;
using DBUserLibrary.Entities.Classes;

namespace DBUserLibrary.Repositories.Interfaces;

public interface IUserRepository
{
    public (bool, User?) GetById(int id);
    public (bool, User?) GetByEmail(string email);
}