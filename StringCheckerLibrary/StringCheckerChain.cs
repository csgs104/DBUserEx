using System;

// 4

namespace StringCheckerLibrary
{
	public abstract class StringCheckerChain
    {
        private readonly StringChecker _chain;

        public StringChecker Chain { get => _chain; }

        public StringCheckerChain()
        {
            for (int c = 0; c < (Checkers().Count - 1); c++)
            {
                Checkers()[c].SetSuccessor(Checkers()[c + 1]);
            }
            Checkers().Last().SetSuccessor(null);
            _chain = Checkers().First();
        }

        protected abstract IList<StringChecker> Checkers();
    }
}