namespace StringCheckerLibrary;

// 1
public abstract class StringChecker : IStringChecker
{
    private IStringChecker? _successor;

    protected IStringChecker? Successor { get => _successor; }

    public StringChecker(IStringChecker? successor = default)
    {
        _successor = successor;
    }

    public void SetSuccessor(IStringChecker? successor)
    {
        _successor = successor;
    }

    public abstract (bool, string) Check(string str);
}