using System;

namespace StringCheckerLibrary;

// 5

public abstract class StringCheckerHandler : IStringChecker
{
    protected abstract StringCheckerChain CheckerChain();

    public (bool, string) Check(string str)
    {
        return CheckerChain().Chain.Check(str);
    }
}