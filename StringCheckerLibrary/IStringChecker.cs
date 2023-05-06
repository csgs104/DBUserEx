using System;

namespace StringCheckerLibrary;

// 0

public interface IStringChecker
{
    public (bool, string) Check(string str);
}