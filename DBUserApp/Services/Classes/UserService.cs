using System;
using Microsoft.Extensions.Logging;

using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;
using DBUserLibrary.Repositories.Classes;

using DBUserApp.Services.Abstract;

namespace DBUserApp.Services.Classes;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(string connection)
    {
        _userRepository = new UserRepository(connection);
    }

    public void Insert(User user)
    {
        Console.Write($"Result: ");
        try
	    {
            Console.WriteLine($"OK, {nameof(Insert)}_Id={_userRepository.Insert(user)}"); 
	    }
        catch (Exception ex) 
	    {
            Console.WriteLine($"Not OK, {ex.Message}");
        }
    }

    public void Update(User user)
    {
        Console.Write($"Result: ");
        try
        {
            Console.WriteLine($"OK, {nameof(Update)}_Id={_userRepository.Update(user)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Not OK, {ex.Message}");
        }
    }

    public void Delete(User user)
    {
        Console.Write($"Result: ");
        try
        {
            Console.WriteLine($"OK, {nameof(Delete)}_Id={_userRepository.Delete(user)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Not OK, {ex.Message}");
        }
    }

    public void GetById(int id, string password)
    {
        Console.Write($"Result: ");
        try
        {
            Console.WriteLine($"OK, User=[{_userRepository.GetById(id, password)}]");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Not OK, {ex.Message}");
        }
    }

    public void GetByEmail(string email, string password)
    {
        Console.Write($"Result: ");
        try
        {
            Console.WriteLine($"OK, User=[{_userRepository.GetByEmail(email, password)}]");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Not OK, {ex.Message}");
        }
    }
}