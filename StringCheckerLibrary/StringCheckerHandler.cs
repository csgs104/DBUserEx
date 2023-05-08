using System;

// 5
namespace StringCheckerLibrary;

public abstract class StringCheckerHandler : IStringChecker
{
    protected abstract StringCheckerChain CheckerChain();

    public (bool, string) Check(string str)
    {
        return CheckerChain().Chain.Check(str);
    }
}