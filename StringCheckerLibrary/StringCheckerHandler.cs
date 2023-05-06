using System;

namespace StringCheckerLibrary;

public abstract class StringCheckerHandler
{
    protected abstract StringCheckerChain CheckerChain();

    public (bool, string) Check(string str)
    {
        return CheckerChain().Chain.Check(str);
    }
}