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
            var (scoreP1, scoreP2) = PlayRecursiveCombat(deckP1, deckP2);
            return (scoreP1 + scoreP2).ToString();
        }


        public (int, int) PlayRecursiveCombat(List<int> deckP1, List<int> deckP2)
        {
            int scoreP1 = 0, scoreP2 = 0;
            int cardP1 = 0, cardP2 = 0;
        
            List<string> playedDeckP1 = new List<string>();
            List<string> playedDeckP2 = new List<string>();

            //While both players have 1 or more cards
            while (deckP1.Count > 0 && deckP2.Count > 0)
            {
                //string currentDecks = DecksToString(deckP1, deckP2);
                string currentDeckP1 = DeckToString(deckP1);
                string currentDeckP2 = DeckToString(deckP2);

                //Check if both decks have been used before
                if (playedDeckP1.Any(d => d.Equals(currentDeckP1)) || playedDeckP2.Any(d => d.Equals(currentDeckP2)))
                {
                    //Both decks have been used before -> Player 1 wins always
                    return (999, 0); //Score doesn't matter....
                }

                //Remember played decks
                playedDeckP1.Add(currentDeckP1);
                playedDeckP2.Add(currentDeckP2);
     
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
                    var (subScoreP1, subScoreP2) = PlayRecursiveCombat(subDeckP1, subDeckP2);
                    if (subScoreP1 > subScoreP2)
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


            //****** Game is finished --> Count scores ******//
            //We can count always for both player, because the loser has no cards, so he gets no point

            //Player 1 
            int cardValue = deckP1.Count();
            foreach (int card in deckP1)
            {
                scoreP1 += (card * cardValue);
                cardValue--;
            }

            //Player 2
            cardValue = deckP2.Count();
            foreach (int card in deckP2)
            {
                scoreP2 += (card * cardValue);
                cardValue--;
            }

            return (scoreP1, scoreP2);
        }

        public string DecksToString(List<int> deckP1, List<int> deckP2)
        {
            string total = String.Join(",", deckP1) + String.Join(",", deckP2);
            return total;
        }

        public string DeckToString(List<int> deck)
        {
            string total = String.Join(",", deck) ;
            return total;
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
