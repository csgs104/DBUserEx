using System;
using System.Text.RegularExpressions;

using StringCheckerLibrary;

namespace StringCheckerLibrary.EmailChecker.Checkers;

public class SevenCharactersChecker : BaseStringChecker
{
    protected override string Expressions() 
	    => @".{7,}";

    protected override RegexOptions Options() 
	    => RegexOptions.None;

    protected override string Message() 
	    => "The email must contain at least 7 characters.";
}