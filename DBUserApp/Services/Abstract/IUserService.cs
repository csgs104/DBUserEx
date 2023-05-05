using System;

using DBUserLibrary.Entities.Classes;

namespace DBUserApp.Services.Abstract;

public interface IUserService
{
    public void Insert(User user);
    public void Update(User user);
    public void Delete(User user);

    public void GetById(int id);
    public void GetByEmail(string email);

    // public void Query();
}