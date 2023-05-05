using System;

namespace EmailCheckerLibrary;

public abstract class EmailChecker : IEmailChecker
{
    protected EmailChecker? _successor;

    public EmailChecker()
    {
        _successor = null;
    }

    public EmailChecker(EmailChecker? successor)
    {
        _successor = successor;
    }

    public void SetSuccessor(EmailChecker? successor)
    {
        _successor = successor;
    }

    public abstract (bool, string) EmailCheck(string str);
}