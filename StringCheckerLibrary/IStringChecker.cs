using System;

namespace StringCheckerLibrary;

public interface IStringChecker
{
    public (bool, string) Check(string str);
}