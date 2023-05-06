using System;

using StringCheckerLibrary;

namespace StringCheckerLibrary.PasswordChecker;

public class PasswordCheckerHandler : StringCheckerHandler
{
    protected override StringCheckerChain CheckerChain()
        => new PasswordCheckerChain();
}