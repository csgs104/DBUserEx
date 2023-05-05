using System;
using System.Text.RegularExpressions;

using EmailCheckerLibrary;

namespace EmailCheckerLibrary.EmailCheckers;

public class SevenCharactersChecker : EmailChecker
{
    public override (bool, string) EmailCheck(string str)
    {
        if (Regex.IsMatch(str, ".{7,}", RegexOptions.None))
        {
            if (_successor is not null)
            {
                return _successor.EmailCheck(str);
            }
            return (true, string.Empty);
        }
        return (false, "The email must contain at least 7 characters.");
    }
}
