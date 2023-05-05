using System;

namespace PasswordChecker;

public static class PasswordCheckerConsole
{
    public static void WriteLine(string str)
    {
        var r = PasswordCheckerHandler.PasswordCheck(str);
        Console.WriteLine($"PasswordCheck: {r.Item1} {r.Item2}");
    }
}
