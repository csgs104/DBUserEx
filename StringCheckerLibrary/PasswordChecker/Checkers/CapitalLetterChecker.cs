namespace StringCheckerLibrary.PasswordChecker.Checkers;

using System.Text.RegularExpressions;

public class CapitalLetterChecker : BaseStringChecker
{
    public CapitalLetterChecker(IStringChecker? successor = default) 
	: base(successor) 
    { }

    protected override string Expressions() 
	    => @"(?=.*?[A-Z])";

    protected override RegexOptions Options() 
	    => RegexOptions.None;

    protected override string Message() 
	    => @"The password must contain at least 1 capital letter.";
}