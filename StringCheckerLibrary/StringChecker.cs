using System;

namespace StringCheckerLibrary;

// 1

public abstract class StringChecker : IStringChecker
{
    protected IStringChecker? _successor;

    public StringChecker()
    {
        _successor = null;
    }

    public StringChecker(IStringChecker? successor)
    {
        _successor = successor;
    }

    public void SetSuccessor(IStringChecker? successor)
    {
        _successor = successor;
    }

    public abstract (bool, string) Check(string str);
}