using System;

using DBUserLibrary.Entities.Classes;

namespace DBUserApp.Services.Abstract;

public interface IUserService
{
    public void GetById(int id, string password);

    public void GetByEmail(string email, string password);

    public void Insert(User user);

    public void Update(User user);

    public void Delete(User user);
}