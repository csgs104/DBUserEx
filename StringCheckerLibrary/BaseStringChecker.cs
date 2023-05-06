using System;
using System.Text.RegularExpressions;

namespace StringCheckerLibrary;

public abstract class BaseStringChecker : StringChecker
{
    protected abstract string Expressions();

    protected abstract RegexOptions Options();

    protected abstract string Message();


    public override (bool, string) Check(string str)
    {
        if (Regex.IsMatch(str, Expressions(), Options()))
        {
            if (_successor is not null)
            {
                return _successor.Check(str);
            }
            return (true, string.Empty);
        }
        return (false, Message());
    }
}