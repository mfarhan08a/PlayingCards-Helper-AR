using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerLib
{
    class PokerHand
    {
        public PokerHandType HandType { get; private set; }
        public String Name { get; private set; }
        public List<PokerCard> PokerCardList { get; private set; }
        public List<PokerCard> HighCardList { get; private set; }

        //The NameLine is the line of text that should contain only the name of the player of this hand (No strict rules)
        //The PokerCardsLine is the line that should contain information about the 5 cards for this hand
        public PokerHand(string NameLine, string PokerCardsLine)
        {
            Name = NameLine;

            AddCardsToHand(PokerCardsLine);

            HandType = FindHandType();
        }

        private void AddCardsToHand(string PokerCardsLine)
        {
            PokerCardList = new List<PokerCard>();

            //Checks to see if there are any commas
            if (PokerCardsLine.IndexOf(',') < 0)
                throw new Exception("Poker hands must be separated by a comma [,]");

            //Count to see if there are exactly 4 commas
            if (PokerCardsLine.Split(',').Length - 1 != 4)
                throw new Exception("Each hand must have exactly 5 cards");

            //Remove whitespace
            PokerCardsLine = PokerCardsLine.Replace(" ", "");

            for (int i = 0; i < 5; i++)
            {
                if (i < 4) //For first 4 cards, all delimited by a comma
                {
                    PokerCardList.Add(new PokerCard(PokerCardsLine.Substring(0, PokerCardsLine.IndexOf(','))));
                    PokerCardsLine = PokerCardsLine.Substring(PokerCardsLine.IndexOf(',') + 1, PokerCardsLine.Length - PokerCardsLine.IndexOf(',') - 1);
                }
                else //For the final card
                {
                    PokerCardList.Add(new PokerCard(PokerCardsLine));
                }
            }
        }

        private PokerHandType FindHandType()
        {
            //This is the list used for tiebreakers
            HighCardList = new List<PokerCard>();

            //First, sort the hand in descending order of value
            PokerCardList = PokerCardList.OrderByDescending(f => f.Value).ToList();


            ////////////////////////////////////////////////////////////////////////////
            //Determine hand in descending order of priority
            if(IsFlush())
            {
                if(IsStraight())
                {
                    HighCardList = PokerCardList;
                    if(PokerCardList[0].Value == PokerCardValue.Ace) //If highest card is an Ace
                    {
                        return PokerHandType.RoyalFlush;
                    }
                    return PokerHandType.StraightFlush;
                }
            }

            if (IsMultipleOfValue(4, 0))
            {
                return PokerHandType.FourOfKind;
            }

            if (IsMultipleOfValue(3, 0) && IsMultipleOfValue(2, 3))
            {
                return PokerHandType.FullHouse;
            }

            PokerCardList = PokerCardList.OrderByDescending(f => f.Value).ToList();

            if (IsFlush())
            {
                HighCardList = PokerCardList;
                return PokerHandType.Flush;
            }

            if (IsStraight())
            {
                HighCardList = PokerCardList;
                return PokerHandType.Straight;
            }

            if (IsMultipleOfValue(3, 0))
            {
                return PokerHandType.ThreeOfKind;
            }

            else if (IsMultipleOfValue(2, 0))
            {
                if (IsMultipleOfValue(2, 2))
                {
                    return PokerHandType.TwoPair;
                }
                return PokerHandType.Pair;
            }

            HighCardList = PokerCardList;
            return PokerHandType.HighCard;
            ////////////////////////////////////////////////////////////////////////////
        }

        private bool IsFlush()
        {
            bool flush = true;
            PokerCardSuit flushSuit = PokerCardList[0].Suit;

            //If any suits do not match, not a flush
            for (int i = 1; i < PokerCardList.Count; i++)
                if (PokerCardList[i].Suit != flushSuit)
                    flush = false;

            return flush;
        }

        private bool IsStraight()
        {
            bool straight = true;

            //If ordered cards not different by one
            for (int i = 1; i < PokerCardList.Count; i++)
                if (PokerCardList[i - 1].Value - PokerCardList[i].Value != 1)
                    straight = false;

            return straight;
        }

        private bool IsMultipleOfValue(int repeat, int offset)
        {
            int sameValueCount;
            int bestMultiIndex = -1;

            for (int i = offset; i < PokerCardList.Count; i++)
            {
                sameValueCount = 1;
                for (int j = offset; j < PokerCardList.Count; j++)
                    if (i != j)
                        if (PokerCardList[i].Value == PokerCardList[j].Value)
                            sameValueCount++;

                if (sameValueCount == repeat) //find multiple of a value
                {
                    if (bestMultiIndex >= 0)
                    {
                        if (PokerCardList[bestMultiIndex].Value < PokerCardList[i].Value)
                            bestMultiIndex = i;
                    }
                    else
                        bestMultiIndex = i;
                }
            }

            if (bestMultiIndex >= 0) //If there is a multiple of a value
            {
                for (int i = offset; i < PokerCardList.Count; i++) //First add the multiple of a Value to the high card list
                    if (PokerCardList[i].Value == PokerCardList[bestMultiIndex].Value)
                        HighCardList.Add(PokerCardList[i]);

                for (int i = offset; i < PokerCardList.Count; i++) //Then add all other cards in descending order
                    if (PokerCardList[i].Value != PokerCardList[bestMultiIndex].Value)
                        HighCardList.Add(PokerCardList[i]);

                for (int i = 0; i < PokerCardList.Count; i++) //Rearrange hand for future comparisons
                    PokerCardList[i] = HighCardList[i];

                return true;
            }

            return false;
        }
    }
}
