using System;
using System.Text.RegularExpressions;

using PasswordCheckerLibrary;

namespace PasswordCheckerLibrary.PasswordCheckers;

public class SevenCharactersChecker : PasswordChecker
{
    public override (bool, string) PasswordCheck(string str)
    {
        if (Regex.IsMatch(str, ".{7,}", RegexOptions.None))
        {
            if (_successor is not null)
            {
                return _successor.PasswordCheck(str);
            }
            return (true, string.Empty);
        }
        return (false, "The password must contain at least 7 characters.");
    }
}