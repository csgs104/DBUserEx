namespace StringCheckerLibrary;

using System.Text.RegularExpressions;

// 2
public abstract class BaseStringChecker : StringChecker
{
    public BaseStringChecker(IStringChecker? successor = default) 
    : base(successor)
    { }

    public override (bool, string) Check(string str)
    {
        if (Regex.IsMatch(str, Expressions(), Options()))
        {
            if (Successor is not null)
            {
                return Successor.Check(str);
            }
            return (true, string.Empty);
        }
        return (false, Message());
    }

    protected abstract string Expressions();

    protected abstract RegexOptions Options();

    protected abstract string Message();
}