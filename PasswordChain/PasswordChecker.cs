using System;

namespace PasswordChecker;

public abstract class PasswordChecker : IPasswordChecker
{
    protected PasswordChecker? _successor;

    public PasswordChecker()
    {
        _successor = null;
    }

    public PasswordChecker(PasswordChecker? successor)
    {
        _successor = successor;
    }

    public void SetSuccessor(PasswordChecker? successor)
    {
        _successor = successor;
    }

    public abstract (bool, string) PasswordCheck(string str);
}

