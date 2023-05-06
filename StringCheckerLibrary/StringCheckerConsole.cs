using System;

namespace StringCheckerLibrary;

public abstract class StringCheckerConsole
{
    protected abstract StringCheckerHandler HandlerChecker();

    public void WriteLine(string str)
    {
        var r = HandlerChecker().Check(str);
        Console.WriteLine($"PasswordCheck: {r.Item1} {r.Item2}");
    }
}