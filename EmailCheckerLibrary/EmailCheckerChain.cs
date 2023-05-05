using System;
using EmailCheckerLibrary.EmailCheckers;

namespace EmailCheckerLibrary
{
    public class EmailCheckerChain
    {
        private readonly EmailChecker _chain;

        public EmailChecker Chain { get => _chain; }

        public EmailCheckerChain()
        {
            var ck0 = new SevenCharactersChecker();
            var ck1 = new EmailCharactersChecker();

            ck0.SetSuccessor(ck1);
            ck1.SetSuccessor(null);

            _chain = ck0;
        }

        public EmailCheckerChain(IList<EmailChecker> cks)
        {
            for (int c = 0; c < (cks.Count - 1); c++)
            {
                cks[c].SetSuccessor(cks[c + 1]);
            }
            cks.Last().SetSuccessor(null);

            _chain = cks.First();
        }
    }
}
