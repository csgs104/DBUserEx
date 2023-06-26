namespace StringCheckerLibrary.EmailChecker;

using Checkers;

public class EmailCheckerChain : StringCheckerChain
{
    public EmailCheckerChain() : base() { }

    public override IList<StringChecker> Checkers()
    {
        var list = new List<StringChecker>(2);
        list.Add(new SevenCharactersChecker());
        list.Add(new EmailCharactersChecker());
        return list;
    }
}