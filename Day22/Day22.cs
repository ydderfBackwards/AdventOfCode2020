using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day22
    {
        public string SolvePart1(string input)
        {
            var (deckP1, deckP2) = ReadDecks(input);
            int score = PlayCombat(deckP1, deckP2);

            return score.ToString();
        }


        public string SolvePart2(string input)
        {
            var (deckP1, deckP2) = ReadDecks(input);
            int score = PlayRecursiveCombat(deckP1, deckP2);
            return score.ToString();

        }

        public string DeckToString(List<int> deck)
        {
            string total = "";

            foreach (int card in deck)
            {
                total += card.ToString("D2");
            }

            return total;
        }

        public bool PlaySubCombat(List<int> deckP1Org, List<int> deckP2Org)
        {
            bool p1Wins = false;
            int cardP1 = 0, cardP2 = 0;
            List<string> playedDecksP1 = new List<string>();
            List<string> playedDecksP2 = new List<string>();
            List<int> deckP1 = new List<int>(deckP1Org);
            List<int> deckP2 = new List<int>(deckP2Org);

            string currentDeckP1 = "", currentDeckP2 = "";

            while (deckP1.Count > 0 && deckP2.Count > 0)
            {
                currentDeckP1 = DeckToString(deckP1);
                currentDeckP2 = DeckToString(deckP2);

                //Check if both decks have been used before
                if (playedDecksP1.FindIndex(p1 => p1.Equals(currentDeckP1)) == playedDecksP2.FindIndex(p2 => p2.Equals(currentDeckP2)) && playedDecksP1.FindIndex(p1 => p1.Equals(currentDeckP1)) >= 0)
                {
                    //Both decks have been used before -> Player 1 wins always
                    p1Wins = true;
                    return p1Wins;
                }

                //Remember played deck
                playedDecksP1.Add(currentDeckP1);
                playedDecksP2.Add(currentDeckP2);

                //Draw cards
                cardP1 = deckP1.First();
                cardP2 = deckP2.First();

                //Check if they have enough cards to play sub game
                if (deckP1.Count > cardP1 && deckP2.Count > cardP2)
                {
                    List<int> subDeckP1 = deckP1.GetRange(1, cardP1);
                    List<int> subDeckP2 = deckP2.GetRange(1, cardP2);

                    if (PlaySubCombat(subDeckP1, subDeckP2))
                    {
                        //Player 1 wins.
                        deckP1.Add(cardP1);
                        deckP1.Add(cardP2);
                    }
                    else
                    {
                        //Player 2 wins.
                        deckP2.Add(cardP2);
                        deckP2.Add(cardP1);
                    }
                }
                else
                {
                    //play normal game
                    if (cardP1 > cardP2)
                    {
                        //Player 1 wins.
                        deckP1.Add(cardP1);
                        deckP1.Add(cardP2);
                    }
                    else
                    {
                        //Player 2 wins.
                        deckP2.Add(cardP2);
                        deckP2.Add(cardP1);
                    }
                }
                //Remove played cards
                deckP1.RemoveAt(0);
                deckP2.RemoveAt(0);
            }

            //****** Game is finished --> Determine winner ******//
            if (deckP1.Count > 0) { p1Wins = true; }
            else { p1Wins = false; }

            return p1Wins;
        }

        public int PlayRecursiveCombat(List<int> deckP1, List<int> deckP2)
        {
            int score = 0;
            int cardP1 = 0, cardP2 = 0;

            //While both players have 1 or more cards
            while (deckP1.Count > 0 && deckP2.Count > 0)
            {
                //Draw cards
                cardP1 = deckP1.First();
                cardP2 = deckP2.First();

                //Check if they have enough cards to play sub game
                if (deckP1.Count > cardP1 && deckP2.Count > cardP2)
                {
                    //Get cards for sub combat
                    List<int> subDeckP1 = deckP1.GetRange(1, cardP1);
                    List<int> subDeckP2 = deckP2.GetRange(1, cardP2);

                    //Play sub combat
                    if (PlaySubCombat(subDeckP1, subDeckP2))
                    {
                        //Player 1 wins.
                        deckP1.Add(cardP1);
                        deckP1.Add(cardP2);
                    }
                    else
                    {
                        //Player 2 wins.
                        deckP2.Add(cardP2);
                        deckP2.Add(cardP1);
                    }
                }
                else
                {
                    //play normal game
                    if (cardP1 > cardP2)
                    {
                        //Player 1 wins.
                        deckP1.Add(cardP1);
                        deckP1.Add(cardP2);
                    }
                    else
                    {
                        //Player 2 wins.
                        deckP2.Add(cardP2);
                        deckP2.Add(cardP1);
                    }

                }
                //Remove played cards
                deckP1.RemoveAt(0);
                deckP2.RemoveAt(0);
            }


            //****** Game is finished --> Count score ******//
            if (deckP1.Count() > deckP2.Count())
            {
                //Player 1 has won
                int cardValue = deckP1.Count();
                foreach (int card in deckP1)
                {
                    score += (card * cardValue);
                    cardValue--;
                }
            }
            else
            {
                //Player 2 has won
                int cardValue = deckP2.Count();
                foreach (int card in deckP2)
                {
                    score += (card * cardValue);
                    cardValue--;
                }
            }


            return score;
        }



        public int PlayCombat(List<int> deckP1, List<int> deckP2)
        {
            int score = 0;
            int cardP1 = 0, cardP2 = 0;

            //While both players have 1 or more cards
            while (deckP1.Count > 0 && deckP2.Count > 0)
            {
                //Draw cards
                cardP1 = deckP1.First();
                cardP2 = deckP2.First();

                if (cardP1 > cardP2)
                {
                    //Player 1 wins.
                    deckP1.Add(cardP1);
                    deckP1.Add(cardP2);
                }
                else
                {
                    //Player 2 wins.
                    deckP2.Add(cardP2);
                    deckP2.Add(cardP1);
                }
                //Remove played cards
                deckP1.RemoveAt(0);
                deckP2.RemoveAt(0);
            }

            //****** Game is finished --> Count score ******//
            if (deckP1.Count() > deckP2.Count())
            {
                //Player 1 has won
                int cardValue = deckP1.Count();
                foreach (int card in deckP1)
                {
                    score += (card * cardValue);
                    cardValue--;
                }
            }
            else
            {
                //Player 2 has won
                int cardValue = deckP2.Count();
                foreach (int card in deckP2)
                {
                    score += (card * cardValue);
                    cardValue--;
                }
            }


            return score;
        }

        public (List<int>, List<int>) ReadDecks(string input)
        {
            List<int> deckPlayer1 = new List<int>();
            List<int> deckPlayer2 = new List<int>();

            string[] players = input.Split(Environment.NewLine + Environment.NewLine);

            string[] cardsPlayer1 = players[0].Split(Environment.NewLine);
            deckPlayer1 = cardsPlayer1[1..cardsPlayer1.Length].Select(x => int.Parse(x)).ToList();

            string[] cardsPlayer2 = players[1].Split(Environment.NewLine);
            deckPlayer2 = cardsPlayer2[1..cardsPlayer2.Length].Select(x => int.Parse(x)).ToList();

            return (deckPlayer1, deckPlayer2);
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day22();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
