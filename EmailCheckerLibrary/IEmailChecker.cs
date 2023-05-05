using System;

namespace EmailCheckerLibrary;

public interface IEmailChecker
{
    public (bool, string) EmailCheck(string str);
}