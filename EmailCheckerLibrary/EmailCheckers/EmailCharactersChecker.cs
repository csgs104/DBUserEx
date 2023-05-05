using System;
using System.Text.RegularExpressions;

namespace EmailCheckerLibrary.EmailCheckers;

public class EmailCharactersChecker : EmailChecker
{
    public override (bool, string) EmailCheck(string str)
    {
        if (Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
        {
            if (_successor is not null)
            {
                return _successor.EmailCheck(str);
            }
            return (true, string.Empty);
        }
        return (false, "The email must be a valid email.");
    }
}