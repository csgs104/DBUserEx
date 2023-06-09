﻿namespace StringCheckerLibrary.PasswordChecker;

using Checkers;

public class PasswordCheckerChain : StringCheckerChain
{
    public PasswordCheckerChain() : base() { }

    public override IList<StringChecker> Checkers()
    {
        var list = new List<StringChecker>(4);
        list.Add(new SevenCharactersChecker());
        list.Add(new CapitalLetterChecker());
        list.Add(new NumberCharacterChecker());
        list.Add(new SpecialCharacterChecker());
        return list;
    }
}