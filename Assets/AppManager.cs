using System.Collections.Generic;
using UnityEngine;
using PokerLib;
using System.Linq;

public class AppManager : MonoBehaviour
{
    public Dictionary<string, bool> elementStatus = new Dictionary<string, bool>();
    private string[] cardSuits = { "H", "D", "C", "S" };
    private string[] cardValues = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

    void Awake()
    {
        for (int i = 0; i < cardSuits.Length; i++)
        {
            for (int j = 0; j < cardValues.Length; j++)
            {
                var card = $"{cardValues[j]}{cardSuits[i]}";
                elementStatus.Add(card, false);
            }
        }
    }

    public void ElementActivated(string name)
    {
        elementStatus[name] = true;
    }

    public void ElementDeactivated(string name)
    {
        elementStatus[name] = false;
    }

    public bool IsElementActive(string name)
    {
        return elementStatus[name] == true;
    }

    public string GetHandCardsType()
    {
        var userCards = new List<string>();
        foreach (var card in elementStatus)
        {
            if (card.Value)
            {
                userCards.Add(card.Key);
            }
        }

        var cards = string.Join(", ", userCards.Select(s => s));

        if (userCards.Count == 5)
        {
            PokerHand handCards = new PokerHand("user", cards);

            switch (handCards.HandType)
            {
                case PokerHandType.HighCard:
                    cards = "High Card";
                    break;
                case PokerHandType.Pair:
                    cards = "Pair";
                    break;
                case PokerHandType.TwoPair:
                    cards = "Two Pair";
                    break;
                case PokerHandType.Straight:
                    cards = "Straight";
                    break;
                case PokerHandType.StraightFlush:
                    cards = "Straight Flush";
                    break;
                case PokerHandType.Flush:
                    cards = "Flush";
                    break;
                case PokerHandType.FourOfKind:
                    cards = "Four of a Kind";
                    break;
                case PokerHandType.FullHouse:
                    cards = "Full House";
                    break;
                case PokerHandType.ThreeOfKind:
                    cards = "Three of a Kind";
                    break;
                case PokerHandType.RoyalFlush:
                    cards = "Royal Flush";
                    break;
                default:
                    cards = handCards.HandType.ToString();
                    break;
            }

            return "Your hand: " + cards;
        }
        else
        {
            return "Your Cards: " + cards;
        }

    }
}
