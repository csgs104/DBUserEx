using System;
using System.Collections.Generic;

using DBUserApp.Writers;

using DBUserLibrary.Entities.Abstract;
using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;
using DBUserLibrary.Repositories.Classes;
using DBUserLibrary.Repositories.Exceptions;

using DBUserApp.Services.Modules;
using DBUserApp.Services.Modules.Abstract;
using DBUserApp.Services.Modules.Exceptions;


namespace DBUserApp.Services.Modules.Classes;

public class UserModule : IModule
{
    private readonly IUserRepository _userRepository;

    public UserModule(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string Name => "UserMenu";
    public string Command => "User";

    private const string SearchById = "SBI";
    private const string SearchByEmail = "SBE";
    private const string WriteUserData = "WUF";

    public void Run()
    {
        var operations = Options.Operations();
        operations.Add(SearchById, "Search by Id");
        operations.Add(SearchByEmail, "Search by Email");
        operations.Add(WriteUserData, "Write User Data");

        while (true)
        {
            Console.WriteLine(Name);
            foreach (var operation in operations)
            {
                Console.WriteLine($"[{operation.Key}]:\t{operation.Value}");
            }
            Console.Write("Your Choice: ");
            var choice = Console.ReadLine() ?? string.Empty;
            try
            {
                ManageChoice(choice);
            }
            catch (ExitException e)
            {
                Console.WriteLine(e.Message);
                break;
            }
        }
    }

    private void ManageChoice(string choice)
    {
        switch (choice)
        {
            case Options.INSERT: InsertUser(); break;
            case Options.UPDATE: UpdateUser(); break;
            case Options.DELETE: DeleteUser(); break;
            case SearchById: SearchUserById(); break;
            case SearchByEmail: SearchUserByEmail(); break;
            case WriteUserData: WritingUserData(); break;
            case Options.EXIT: throw new ExitException($"Exit From {Name}.");
            default: Console.WriteLine($"Choice Not Valid: {choice}."); break;
        }
    }

    private void InsertUser()
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");

        var commaseparatedstr = Prompt("Write a CommaSeparatedString: ");
        if (commaseparatedstr is null)
        {
            Console.WriteLine("CommaSeparatedString not readable. Operation cancelled.");
            return;
        }
        var search = SimpleParseCommaSeparatedString(commaseparatedstr);

        User? user = null;
        try
        {
            user = _userRepository.GetByEmail(search.Item1, search.Item2);
        }
        catch (EntityNotFoundException ex)
        {
            Console.Write($"Processing... ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Insert cancelled, {ex.Message}");
            return;
        }

        // ... 
        if (user is null)
        {
            Console.WriteLine($"Insert accepted.");
            var result = _userRepository.InsertUser(new User(search.Item1, search.Item2));
            try
            {
                Console.WriteLine($"Insert completed, Id={result}");
            }
            catch (Exception ex2)
            {
                Console.WriteLine($"Insert cancelled, {ex2.Message}");
            }
        }
        else 
	    {
            Console.WriteLine($"Insert cancelled.");
        }
    }

    private void UpdateUser()
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");
        var commaseparatedstr = Prompt("Write a CommaSeparatedString: ");
        if (commaseparatedstr is null)
        {
            Console.WriteLine("CommaSeparatedString not readable. Operation cancelled.");
            return;
        }
        var search = SimpleParseCommaSeparatedString(commaseparatedstr);


        User? user = null;
        try
        {
            user = _userRepository.GetByEmail(search.Item1, search.Item2);
        }
        catch (EntityNotFoundException ex)
        {
            Console.WriteLine($"Processing... ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update cancelled, {ex.Message}.");
            return;
        }

        // ...
        if (user is not null)
        {
            Console.WriteLine($"Update accepted.");
            Console.WriteLine("Write Your New: \"Email;Password\".");
            Console.WriteLine("Example: \"mynewmail@mail.com;NewPassword1$\".");
            var commaseparatedstrNew = Prompt("Write a CommaSeparatedString: ");
            if (commaseparatedstrNew is null)
            {
                Console.WriteLine("CommaSeparatedString not readable. Operation cancelled.");
                return;
            }
            var searchNew = SimpleParseCommaSeparatedString(commaseparatedstr);

            User? userNew = null;
            try
            {
                userNew = _userRepository.GetByEmail(search.Item1, search.Item2);
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine($"Processing... ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update cancelled, {ex.Message}.");
                return;
            }

            if (userNew is null)
            {
                Console.WriteLine($"Update accepted.");
                var result = _userRepository.UpdateUser(new User(user.Id, searchNew.Item1, searchNew.Item2, user.Date));
                try
                {
                    Console.WriteLine($"Update completed, Id={result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Update cancelled, {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Update cancelled.");
            }
        }
        else
        {
            Console.WriteLine($"Update cancelled.");
        }
    }

    private void DeleteUser()
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");

        var commaseparatedstr = Prompt("Write a CommaSeparatedString: ");
        if (commaseparatedstr is null)
        {
            Console.WriteLine("CommaSeparatedString not readable. Operation cancelled.");
            return;
        }
        var search = SimpleParseCommaSeparatedString(commaseparatedstr);

        User? user = null;
        try
        {
            user = _userRepository.GetByEmail(search.Item1, search.Item2);
        }
        catch (EntityNotFoundException ex)
        {
            Console.Write($"Processing... ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete cancelled, {ex.Message}");
            return;
        }

        // ... 
        if (user is not null)
        {
            Console.WriteLine($"Delete accepted.");
            var result = _userRepository.Delete(user);
            try
            {
                Console.WriteLine($"Insert completed, Id={result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert cancelled, {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Insert cancelled.");
        }
    }

    private void SearchUserById()
    {
        Console.WriteLine("Write Your: \"Id;Password\".");
        Console.WriteLine("Example: \"2;Password1$\".");
        var commaseparatedstr = Prompt("Read CommaSeparatedString: ");
        if (commaseparatedstr is null)
        {
            Console.WriteLine("CommaSeparatedString not readable. Operation cancelled.");
            return;
        }
        var search = SimpleParseCommaSeparatedString(commaseparatedstr);

        try
        {
            var user = _userRepository.GetById(int.Parse(search.Item1), search.Item2);
            Console.WriteLine($"User found: {user}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User not found: {ex.Message}");
        }
    }

    private void SearchUserByEmail()
    {
        Console.WriteLine("Write Your: \"Id;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");
        var commaseparatedstr = Prompt("Read CommaSeparatedString: ");
        if (commaseparatedstr is null)
        {
            Console.WriteLine("CommaSeparatedString not readable. Operation cancelled.");
            return;
        }
        var search = SimpleParseCommaSeparatedString(commaseparatedstr);

        try
        {
            var user = _userRepository.GetByEmail(search.Item1, search.Item2);
            Console.WriteLine($"User found: {user}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User not found: {ex.Message}");
        }
    }

    private void WritingUserData() 
    {
        Console.WriteLine("Write Your: \"Id;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");
        var commaseparatedstr = Prompt("Read CommaSeparatedString: ");
        if (commaseparatedstr is null)
        {
            Console.WriteLine("CommaSeparatedString not readable. Operation cancelled.");
            return;
        }
        var search = SimpleParseCommaSeparatedString(commaseparatedstr);

        User? user = null;
        try
        {
            user = _userRepository.GetByEmail(search.Item1, search.Item2);
        }
        catch (EntityNotFoundException ex)
        {
            Console.Write($"Processing... ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Writing cancelled, {ex.Message}");
            return;
        }

        if (user is not null)
        {
            Console.WriteLine($"Writing accepted.");
            if ((new FileWriterUserCSV(user)).FileWrite())
            {
                Console.WriteLine($"Writing completed.");
            }
            else 
	        {
                Console.WriteLine($"Writing cancelled.");
            }
        }
        else
        {
            Console.WriteLine($"Writing cancelled.");
        }
    }


    private static string? Prompt(string text)
    {
        Console.Write(text);
        var str = Console.ReadLine();

        if (str is not null)
        {
            Console.WriteLine("Text not null.");
            return str;
        }
        else
        {
            Console.WriteLine("Text null.");
            return null;
        }
    }

    private static (string, string) SimpleParseCommaSeparatedString(string commaseparatedstr)
    {
        var split = commaseparatedstr.Split(';') ?? Array.Empty<string>();
        if (split.Length != 2)
        {
            throw new Exception("Error.");
        }
        return (split[0], split[1]);
    }
}