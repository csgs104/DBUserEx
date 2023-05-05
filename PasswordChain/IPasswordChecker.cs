using System;

namespace PasswordChecker;

public interface IPasswordChecker
{
    public (bool, string) PasswordCheck(string str);
}

