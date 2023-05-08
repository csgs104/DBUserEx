using System;

// 0
namespace StringCheckerLibrary;

public interface IStringChecker
{
    public (bool, string) Check(string str);
}