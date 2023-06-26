namespace StringCheckerLibrary.EmailChecker;

public class EmailCheckerHandler : StringCheckerHandler
{
    protected override StringCheckerChain CheckerChain() 
	    => new EmailCheckerChain();
}