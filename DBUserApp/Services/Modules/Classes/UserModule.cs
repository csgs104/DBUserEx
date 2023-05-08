using System;
using System.Collections.Generic;

using DBUserLibrary.Entities.Abstract;
using DBUserLibrary.Entities.Classes;
using DBUserLibrary.Repositories.Abstract;
using DBUserLibrary.Repositories.Classes;
using DBUserLibrary.Repositories.Exceptions;

using DBUserApp.Writers;
using DBUserApp.Services.Modules;
using DBUserApp.Services.Modules.Abstract;
using DBUserApp.Services.Modules.Exceptions;


namespace DBUserApp.Services.Modules.Classes;

public class UserModule : IUserModule
{
    private readonly IUserRepository _userRepo;


    public UserModule(IUserRepository userRepo)
    {
        _userRepo = userRepo;
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

        Console.WriteLine(Name);
        while (true)
        {
            Console.WriteLine("#### #### #### ####");
            Console.WriteLine("UserMenu:");

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
                Console.WriteLine("Returning to MENU.");
                Console.WriteLine("#### #### #### ####");
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

    public void InsertUser()
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");

        (string?, string?) search = (null, null);
        try { search = Search(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        if (search.Item1 is null || search.Item2 is null) return;

        User? user = null;
        try { user = _userRepo.GetByEmail(search.Item1, search.Item2); }
        catch (EntityNotFoundException ex) { Console.Write($"Processing... "); }
        catch (Exception ex) { Console.WriteLine($"Insert cancelled, {ex.Message}"); return; }

        if (user is null)
        {
            Console.WriteLine($"accepted.");
            try { var result = _userRepo.InsertUser(new User(search.Item1, search.Item2)); 
		          Console.WriteLine($"Insert completed, Id={result}"); }
            catch (Exception ex) { Console.WriteLine($"cancelled, {ex.Message}"); }
        }
        else { Console.WriteLine($"cancelled."); }
    }

    public void UpdateUser()
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");

        (string?, string?) search = (null, null);
        try { search = Search(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        if (search.Item1 is null || search.Item2 is null) return;

        User? user = null;
        try { user = _userRepo.GetByEmail(search.Item1, search.Item2); }
        catch (EntityNotFoundException ex) { Console.Write($"Processing... "); }
        catch (Exception ex) { Console.WriteLine($"Update cancelled, {ex.Message}."); return; }

        if (user is not null)
        {
            Console.WriteLine($"accepted.");
            Console.WriteLine("Write Your New: \"Email;Password\".");
            Console.WriteLine("Example: \"mynewmail@mail.com;NewPassword1$\".");

            (string?, string?) searchNew = (null, null);
            try { searchNew = Search(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (searchNew.Item1 is null || searchNew.Item2 is null) return;

            User? userNew = null;
            try { userNew = _userRepo.GetByEmail(searchNew.Item1, searchNew.Item2); }
            catch (EntityNotFoundException ex) { Console.Write($"Processing... "); }
            catch (Exception ex) { Console.WriteLine($"Update cancelled, {ex.Message}."); return; }

            if (userNew is null)
            {
                Console.WriteLine($"accepted.");
                try{ var result = _userRepo.UpdateUser(new User(user.Id, searchNew.Item1, searchNew.Item2, user.Date));
                     Console.WriteLine($"Update completed, Id={result}"); }
                catch (Exception ex) { Console.WriteLine($"Update cancelled, {ex.Message}"); }
            }
            else { Console.WriteLine($"cancelled."); }
        }
        else { Console.WriteLine($"cancelled."); }
    }

    public void DeleteUser()
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");

        (string?, string?) search = (null, null);
        try { search = Search(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        if (search.Item1 is null || search.Item2 is null) return;

        User? user = null;
        try { user = _userRepo.GetByEmail(search.Item1, search.Item2); }
        catch (EntityNotFoundException ex) { Console.Write($"Processing... "); }
        catch (Exception ex) { Console.WriteLine($"Delete cancelled, {ex.Message}"); return; }

        if (user is not null)
        {
            Console.WriteLine($"accepted.");
            try { var result = _userRepo.Delete(user);
                  Console.WriteLine($"Insert completed, Id={result}"); }
            catch (Exception ex) { Console.WriteLine($"Insert cancelled, {ex.Message}"); }
        }
        else { Console.WriteLine($"Insert cancelled."); }
    }

    public void SearchUserById()
    {
        Console.WriteLine("Write Your: \"Id;Password\".");
        Console.WriteLine("Example: \"2;Password1$\".");

        (string?, string?) search = (null, null);
        try { search = Search(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        if (search.Item1 is null || search.Item2 is null) return;

        try { var user = _userRepo.GetById(int.Parse(search.Item1), search.Item2);
              Console.WriteLine($"Found: {user}"); }
        catch (Exception ex) { Console.WriteLine($"Not Found: {ex.Message}"); }
    }

    public void SearchUserByEmail()
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");

        (string?, string?) search = (null, null);
        try { search = Search(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        if (search.Item1 is null || search.Item2 is null) return;

        try
        {
            var user = _userRepo.GetByEmail(search.Item1, search.Item2);
            Console.WriteLine($"User found: {user}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User not Found: {ex.Message}");
        }
    }

    public void WritingUserData() 
    {
        Console.WriteLine("Write Your: \"Email;Password\".");
        Console.WriteLine("Example: \"mymail@mail.com;Password1$\".");

        (string?, string?) search = (null, null);
        try { search = Search(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); } 
	    if (search.Item1 is null || search.Item2 is null) return;

        User? user = null;
        try { user = _userRepo.GetByEmail(search.Item1, search.Item2); }
        catch (EntityNotFoundException ex) { Console.Write($"Processing... "); }
        catch (Exception ex) { Console.WriteLine($"Writing cancelled, {ex.Message}"); return; }

        if (user is not null)
        {
            Console.WriteLine($"accepted.");
            if ((new FileWriterUserCSV(user)).FileWrite()) { Console.WriteLine($"Writing completed."); }
            else { Console.WriteLine($"Writing cancelled."); }
        }
        else { Console.WriteLine($"Writing cancelled."); }
    }

    private static (string, string) Search() 
    {
        Console.Write("Read CommaSeparatedString: ");
        var commaseparatedstr = Console.ReadLine();
        if (commaseparatedstr is null)
        {
            throw new Exception("CommaSeparatedString not readable. Operation cancelled.");
        }
        var split = commaseparatedstr.Split(';') ?? Array.Empty<string>();
        if (split.Length != 2)
        {
            throw new Exception("CommaSeparatedString is readable, but not correct. Operation cancelled.");
        }
        return (split[0], split[1]);
    }
}