using System;

namespace PokerLib
{
    class PokerCard
    {
        public PokerCardSuit Suit { get; private set; }
        public PokerCardValue Value { get; private set; }

        public PokerCard(string pokerCardCode)
        {
            if (pokerCardCode.Length != 2 && pokerCardCode.Length != 3)
                throw new Exception("Incorretly formatted card length, all cards must be 2-3 characters (ex. QH, 10D, 4C): " + pokerCardCode);

            Suit = getSuitFromCode(pokerCardCode);
            Value = getValueFromCode(pokerCardCode);
        }

        private PokerCardSuit getSuitFromCode(string pokerCardCode)
        {
            switch (pokerCardCode.Substring(pokerCardCode.Length - 1, 1).ToUpper())
            {
                case "C":
                    return PokerCardSuit.Clubs;
                case "H":
                    return PokerCardSuit.Hearts;
                case "S":
                    return PokerCardSuit.Spades;
                case "D":
                    return PokerCardSuit.Diamonds;
                default:
                    throw new Exception(pokerCardCode.Substring(pokerCardCode.Length - 1, 1) + " is not a valid suit (D, H, C, S)");
            }
        }

        private PokerCardValue getValueFromCode(string pokerCardCode)
        {
            if (pokerCardCode.Length == 3)
            {
                if (pokerCardCode.Substring(0, 2) == "10")
                    return PokerCardValue.Ten;
                else
                    throw new Exception(pokerCardCode.Substring(0, 2) + " is not a valid card value (2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, A)");
            }
            else
            {
                switch (pokerCardCode.Substring(0, 1).ToUpper())
                {
                    case "2":
                        return PokerCardValue.Two;
                    case "3":
                        return PokerCardValue.Three;
                    case "4":
                        return PokerCardValue.Four;
                    case "5":
                        return PokerCardValue.Five;
                    case "6":
                        return PokerCardValue.Six;
                    case "7":
                        return PokerCardValue.Seven;
                    case "8":
                        return PokerCardValue.Eight;
                    case "9":
                        return PokerCardValue.Nine;
                    case "J":
                        return PokerCardValue.Jack;
                    case "Q":
                        return PokerCardValue.Queen;
                    case "K":
                        return PokerCardValue.King;
                    case "A":
                        return PokerCardValue.Ace;
                    default:
                        throw new Exception(pokerCardCode.Substring(0, 1) + " is not a valid card value (2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, A)");
                }
            }
        }
    }
}
