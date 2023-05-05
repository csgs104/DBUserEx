using System;

namespace EmailCheckerLibrary;

public static class EmailCheckerHandler
{
    public static (bool, string) EmailCheck(string str)
    {
        return (new EmailCheckerChain().Chain).EmailCheck(str);
    }
}