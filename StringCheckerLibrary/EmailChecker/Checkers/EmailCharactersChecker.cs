using System;
using System.Text.RegularExpressions;

using StringCheckerLibrary;


namespace StringCheckerLibrary.EmailChecker.Checkers;

public class EmailCharactersChecker : BaseStringChecker
{
    public EmailCharactersChecker(IStringChecker? successor = default)
    : base(successor)
    { }


    protected override string Expressions() 
	    => @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    protected override RegexOptions Options() 
	    => RegexOptions.IgnoreCase;

    protected override string Message() 
	    => @"The email must be a valid email.";
}