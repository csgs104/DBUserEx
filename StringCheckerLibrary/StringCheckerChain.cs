using System;

// 3
namespace StringCheckerLibrary
{
    public abstract class StringCheckerChain
    {
        private readonly StringChecker _chain;

        public StringChecker Chain { get => _chain; }


        public StringCheckerChain()
        {
            var list = Checkers();
            for (int c = 0; c < (list.Count - 1); c++)
            {
                list[c].SetSuccessor(list[c + 1]);
            }
            list.Last().SetSuccessor(null);
            _chain = list.First();
        }

        public abstract IList<StringChecker> Checkers();
    }
}