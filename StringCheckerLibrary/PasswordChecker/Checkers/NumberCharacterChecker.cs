namespace StringCheckerLibrary.PasswordChecker.Checkers;

using System.Text.RegularExpressions;

public class NumberCharacterChecker : BaseStringChecker
{
    public NumberCharacterChecker(IStringChecker? successor = default)
    : base(successor)
    { }

    protected override string Expressions()
        => @"(?=.*?[0-9])";

    protected override RegexOptions Options()
        => RegexOptions.None;

    protected override string Message()
        => @"The password must contain at least 1 number character.";
}