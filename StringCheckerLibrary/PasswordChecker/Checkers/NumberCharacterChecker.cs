using System;
using System.Text.RegularExpressions;

using StringCheckerLibrary;

namespace StringCheckerLibrary.PasswordChecker.Checkers;

public class NumberCharacterChecker : BaseStringChecker
{
    protected override string Expressions()
        => @"(?=.*?[0 - 9])";

    protected override RegexOptions Options()
        => RegexOptions.None;

    protected override string Message()
        => @"The password must contain at least 1 number character.";
}