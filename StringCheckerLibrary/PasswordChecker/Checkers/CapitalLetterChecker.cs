using System;
using System.Text.RegularExpressions;

using StringCheckerLibrary;

namespace StringCheckerLibrary.PasswordChecker.Checkers;

public class CapitalLetterChecker : BaseStringChecker
{
    protected override string Expressions() 
	    => @"(?=.*?[A - Z])";

    protected override RegexOptions Options() 
	    => RegexOptions.None;

    protected override string Message() 
	    => @"The password must contain at least 1 capital letter.";
}