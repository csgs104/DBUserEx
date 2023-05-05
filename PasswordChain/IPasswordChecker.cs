using System;

namespace PasswordCheckerLibrary;

public interface IPasswordChecker
{
    public (bool, string) PasswordCheck(string str);
}