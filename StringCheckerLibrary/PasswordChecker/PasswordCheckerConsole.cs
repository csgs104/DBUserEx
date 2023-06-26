namespace StringCheckerLibrary.PasswordChecker;

public class PasswordCheckerConsole : StringCheckerConsole
{
    protected override StringCheckerHandler HandlerChecker()
        => new PasswordCheckerHandler();
}