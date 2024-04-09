using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace PokerLib
{
    public class PokerRound
    {
        public string[] Lines { get; private set; }

        //Constructor if text is in a file, given by relative filename
        public PokerRound(string inputFileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            try
            {
                if (!File.Exists(currentDirectory + "\\" + inputFileName))
                    throw new Exception("File does not exist");

                if (inputFileName.Substring(inputFileName.Length - 4) != ".txt")
                    throw new Exception("Input must be a .txt file");

                Lines = File.ReadAllLines(currentDirectory + "\\" + inputFileName);
            }
            catch (Exception e)
            {
                //To reduce verbosity of this relatively simple program, just display the custom message and
                //do not display anything about the stack trace or exception type
                Console.WriteLine("Error: " + e.Message);
            }
        }

        //Constructor if text is given as string array
        public PokerRound(string[] newLines)
        {
            Lines = newLines;
        }

        public void DetermineWinner()
        {
            try
            {
                for (int i = 0; i < Lines.Length; i++)
                {
                    Console.WriteLine(Lines[i]);
                }
                if (Lines.Length % 2 != 0 || Lines.Length == 0)
                    throw new Exception("Must have even number of lines in input file (Name / Hand on alternating lines)" + Lines.Length);

                List<PokerHand> pokerHandList = new List<PokerHand>();      //List for all hands
                List<PokerHand> highHandList = new List<PokerHand>();       //List for high subset of hands
                List<PokerHand> highestHandList = new List<PokerHand>();    //List for highest hands after high card tiebreaker

                //Add all poker hands from lines in the text file
                for (int i = 0; i + 1 < Lines.Length; i += 2)
                    pokerHandList.Add(new PokerHand(Lines[i], Lines[i + 1]));

                //Order them by hand types (Descending order)
                pokerHandList = pokerHandList.OrderByDescending(f => f.HandType).ToList();

                //Move all of the highest hand subset to the next list
                for (int i = 0; i < pokerHandList.Count; i++)
                    if (pokerHandList[i].HandType == pokerHandList[0].HandType)
                        highHandList.Add(pokerHandList[i]);

                //Create bool list to determine which Hands to keep after tiebreaker
                List<bool> keepHighHand = new List<bool>();

                for (int i = 0; i < highHandList.Count; i++)
                    keepHighHand.Add(true);

                PokerCardValue maxCard;

                //Starting at the highest card (card lists are already in descending order), determine
                //The max card in order and eliminate all other hands that do not have that max card in that position
                for (int i = 0; i < 5; i++)
                {
                    maxCard = PokerCardValue.Two;

                    for (int j = 0; j < highHandList.Count; j++)
                        if (maxCard < highHandList[j].HighCardList[i].Value && keepHighHand[j])
                            maxCard = highHandList[j].HighCardList[i].Value;

                    for (int j = 0; j < highHandList.Count; j++)
                        if (highHandList[j].HighCardList[i].Value < maxCard)
                            keepHighHand[j] = false;
                }

                //Move all of the highest hands to the final list
                for (int i = 0; i < highHandList.Count; i++)
                    if (keepHighHand[i])
                        highestHandList.Add(highHandList[i]);

                //Write all winners to the console
                if (highestHandList.Count == 1)
                {
                    Console.WriteLine("Winner: " + highestHandList[0].Name + "(" + highestHandList[0].HandType + ")");
                }
                else
                {
                    Console.Write("Winners: ");
                    for (int i = 0; i < highestHandList.Count; i++)
                    {
                        Console.Write(highestHandList[i].Name + "(" + highestHandList[i].HandType + ")");
                        if (i < highestHandList.Count - 1)
                        {
                            Console.Write(", ");
                        }
                    }
                }
            }

            catch (Exception e)
            {
                //To reduce verbosity of this relatively simple program, just display the custom message and
                //do not display anything about the stack trace or exception type
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
