using System;
namespace PasswordChecker;

public static class PasswordCheckerHandler
{
    public static (bool, string) PasswordCheck(string str)
    {
        return (new PasswordCheckerChain().Chain).PasswordCheck(str);
    }
}