using System;
using System.Text.RegularExpressions;

using PasswordChecker;

namespace PasswordChecker.PasswordCheckers;

public class SpecialCharacterChecker : PasswordChecker
{
    public override (bool, string) PasswordCheck(string str)
    {
        if (Regex.Match(str, "(?=.*?[#?!@$%^&*-])").Success)
        {
            if (_successor is not null)
            {
                return _successor.PasswordCheck(str);
            }
            return (true, string.Empty);
        }
        return (false, "The password must contain at least 1 special character.");
    }
}

