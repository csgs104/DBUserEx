using System;
using System.Text.RegularExpressions;

using StringCheckerLibrary;

namespace StringCheckerLibrary.PasswordChecker.Checkers;

public class SpecialCharacterChecker : BaseStringChecker
{
    protected override string Expressions()
        => @"(?=.*?[#?!@$%^&*-])";

    protected override RegexOptions Options()
        => RegexOptions.None;

    protected override string Message()
        => @"The password must contain at least 1 special character.";
}