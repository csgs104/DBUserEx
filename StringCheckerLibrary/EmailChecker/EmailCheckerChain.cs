using System;

using StringCheckerLibrary;
using StringCheckerLibrary.EmailChecker.Checkers;

namespace StringCheckerLibrary.EmailChecker;

public class EmailCheckerChain : StringCheckerChain
{
    protected override IList<StringChecker> Checkers()
    {
        var list = new List<StringChecker>(2);
        list.Add(new SevenCharactersChecker());
        list.Add(new EmailCharactersChecker());
        return list;
    }
}