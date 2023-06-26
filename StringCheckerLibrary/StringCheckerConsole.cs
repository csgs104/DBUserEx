namespace StringCheckerLibrary;

// OPTIONAL
public abstract class StringCheckerConsole
{
    protected abstract StringCheckerHandler HandlerChecker();

    public void WriteLine(string str)
    {
        var r = HandlerChecker().Check(str);
        Console.WriteLine($"Check: {r.Item1} - {r.Item2}");
    }
}