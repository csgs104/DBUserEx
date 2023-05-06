using System;

using StringCheckerLibrary;
using StringCheckerLibrary.PasswordChecker.Checkers;

namespace StringCheckerLibrary.PasswordChecker;

public class PasswordCheckerChain : StringCheckerChain
{
    protected override IList<StringChecker> Checkers()
    {
        var list = new List<StringChecker>(4);
        list.Add(new SevenCharactersChecker());
        list.Add(new CapitalLetterChecker());
        list.Add(new NumberCharacterChecker());
        list.Add(new SpecialCharacterChecker());
        return list;
    }
}