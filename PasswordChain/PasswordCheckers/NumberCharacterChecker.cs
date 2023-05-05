﻿using System;
using System.Text.RegularExpressions;

using PasswordCheckerLibrary;

namespace PasswordCheckerLibrary.PasswordCheckers;

public class NumberCharacterChecker : PasswordChecker
{
    public override (bool, string) PasswordCheck(string str)
    {
        if (Regex.Match(str, "(?=.*?[0 - 9])").Success)
        {
            if (_successor is not null)
            {
                return _successor.PasswordCheck(str);
            }
            return (true, string.Empty);
        }
        return (false, "The password must contain at least 1 number character.");
    }
}