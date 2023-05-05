using System;
using PasswordCheckerLibrary.PasswordCheckers;

namespace PasswordCheckerLibrary
{
	public class PasswordCheckerChain
	{
        private readonly PasswordChecker _chain;

        public PasswordChecker Chain { get => _chain; }

        public PasswordCheckerChain()
        {
            var ck0 = new SevenCharactersChecker();
            var ck1 = new CapitalLetterChecker();
            var ck2 = new NumberCharacterChecker();
            var ck3 = new SpecialCharacterChecker();

            ck0.SetSuccessor(ck1);
            ck1.SetSuccessor(ck2);
            ck2.SetSuccessor(ck3);
            ck3.SetSuccessor(null);

            _chain = ck0;
        }

        public PasswordCheckerChain(SevenCharactersChecker ck0, CapitalLetterChecker ck1, 
	                                NumberCharacterChecker ck2, SpecialCharacterChecker ck3)
        {
            ck0.SetSuccessor(ck1);
            ck1.SetSuccessor(ck2);
            ck2.SetSuccessor(null);
            _chain = ck0;
        }

        public PasswordCheckerChain(IList<PasswordChecker> cks)
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