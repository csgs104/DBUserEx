using System;
using System.Text.RegularExpressions;

using StringCheckerLibrary;


namespace StringCheckerLibrary.PasswordChecker.Checkers;

public class SevenCharactersChecker : BaseStringChecker
{
    public SevenCharactersChecker(IStringChecker? successor = default)
    : base(successor)
    { }


    protected override string Expressions()
        => @".{7,}";

    protected override RegexOptions Options()
        => RegexOptions.None;

    protected override string Message()
        => @"The password must contain at least 7 characters.";
}