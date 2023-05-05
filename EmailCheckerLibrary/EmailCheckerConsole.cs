using System;

namespace EmailCheckerLibrary;

public static class EmailCheckerConsole
{
    public static void WriteLine(string str)
    {
        var r = EmailCheckerHandler.EmailCheck(str);
        Console.WriteLine($"PasswordCheck: {r.Item1} {r.Item2}");
    }
}