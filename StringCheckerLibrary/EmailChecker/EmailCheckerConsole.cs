using System;


namespace StringCheckerLibrary.EmailChecker;

public class EmailCheckerConsole : StringCheckerConsole
{
    protected override StringCheckerHandler HandlerChecker()
	    => new EmailCheckerHandler();
}