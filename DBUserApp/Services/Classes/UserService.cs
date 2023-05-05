using System;
using Microsoft.Extensions.Logging;

using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;
using DBUserLibrary.Repositories.Classes;

using DBUserApp.Services.Abstract;

namespace DBUserApp.Services.Classes;


public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(string connection)
    {
        _userRepository = new UserRepository(connection);
    }

    private void Operation(int op) 
    {
        if (op > 0)
        {
            Console.WriteLine($"OK, row affected: {op}");
        }
        else
        {
            Console.WriteLine($"NOT OK, row affected: {op}.");
        }
    }

    public void Insert(User user)
    {
        Console.Write($"{nameof(Insert)}: ");
        Operation(_userRepository.Insert(user));
    }

    public void Update(User user)
    {
        Console.Write($"{nameof(Update)}: ");
        Operation(_userRepository.Update(user));
    }

    public void Delete(User user)
    {
        Console.Write($"{nameof(Delete)}: ");
        Operation(_userRepository.Delete(user));
    }

    public void GetById(int id)
    {

    }

    public void GetByEmail(string email)
    {

    }
}