using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day22
    {
        MainProgram mainProgram = new MainProgram();

        public List<int> StartingDeck1;
        public List<int> StartingDeck2;
        public List<int> WinnerDeck = new List<int>();

        public Day22(string path)
        {
            var decksText = mainProgram.ConvertFileToParagraphs(path);

            StartingDeck1 = CreateDeck(decksText[0]);
            StartingDeck2 = CreateDeck(decksText[1]);
        }

        private List<int> CreateDeck(string deckText)
        {
            var deck = new List<int>();

            var lines = mainProgram.SplitParagraphByLineEndings(deckText);

            for(int i = 1; i < lines.Count(); i++)
            {
                deck.Add(int.Parse(lines[i]));

            }

            return deck;
        }

        public void RunDay22Part1()
        {
            var winningDeck = new List<int>();
            while(StartingDeck1.Count != 0 && StartingDeck1.Count != 0)
            {
                PlayRound();
            }
            if(StartingDeck1.Count != 0)
            {
                winningDeck = StartingDeck1;
            }
            else
            {
                winningDeck = StartingDeck1;   
            }

            var score = CalculateScore(winningDeck);
            Console.WriteLine($"The winning player's score is {score}");
        }

        private object CalculateScore(List<int> winningDeck)
        {
            var length = winningDeck.Count;
            var score = 0;

            foreach(var card in winningDeck)
            {
                score += length * card;
                length--;
            }

            return score;
        }

        private void PlayRound()
        {
            var cardPlayer1 = StartingDeck1.First();
            StartingDeck1.Remove(cardPlayer1);
            var cardPlayer2 = StartingDeck1.First();
            StartingDeck1.Remove(cardPlayer2);

            if(cardPlayer1 > cardPlayer2)
            {
                StartingDeck1.AddRange(new List<int> { cardPlayer1, cardPlayer2 });
            }
            if(cardPlayer2 > cardPlayer1)
            {
                StartingDeck1.AddRange(new List<int> { cardPlayer2, cardPlayer1 });
            }
        }

        public void RunDay22Part2()
        {
            PlayRecursiveGame(StartingDeck1, StartingDeck2);

            var score = CalculateScore(WinnerDeck);
            Console.WriteLine($"The winning player's score is {score}");

            //Console.WriteLine($"There are {notSeaMonster} waves in the water");
        }

        public int PlayRecursiveGame(List<int> deck1, List<int> deck2)
        {
            var winningDeck = new List<int>();
            List<(List<int>, List<int>)> hands = new List<(List<int>, List<int>)>();
            hands.Add((deck1.ToList(), deck2.ToList()));

            while (deck1.Count != 0 && deck2.Count != 0)
            {
                var winner = 0;
                var cardPlayer1 = deck1.First();
                deck1.Remove(cardPlayer1);
                var cardPlayer2 = deck2.First();
                deck2.Remove(cardPlayer2);

                if (cardPlayer1 <= deck1.Count && cardPlayer2 <= deck2.Count)
                {
                    var recursiveDeck1 = deck1.GetRange(0, cardPlayer1).ToList();
                    var recursiveDeck2 = deck2.GetRange(0, cardPlayer2).ToList();

                    winner = PlayRecursiveGame(recursiveDeck1, recursiveDeck2);
                }

                if ((cardPlayer1 > cardPlayer2 && winner == 0) || winner == 1)
                {
                    deck1.AddRange(new List<int> { cardPlayer1, cardPlayer2 });
                }
                if ((cardPlayer2 > cardPlayer1 && winner == 0) || winner == 2)
                {
                    deck2.AddRange(new List<int> { cardPlayer2, cardPlayer1 });
                }

                if (CheckHandAlreadyDone((deck1,deck2), hands))
                    return 1;

                hands.Add((deck1.ToList(), deck2.ToList()));
            }
            
            if (deck1.Count != 0)
            {
                WinnerDeck = deck1;
                return 1;
            }
            else
            {
                WinnerDeck = deck2;
                return 2;
            }
        }

        private bool CheckHandAlreadyDone((List<int>, List<int>) deck, List<(List<int>, List<int>)> hands)
        {
            foreach(var hand in hands)
            {
                var result = CheckSpecificHand(deck, hand);

                if (result == true)
                    return result;
            }
            return false;
        }

        private bool CheckSpecificHand((List<int>, List<int>) deck, (List<int>, List<int>) hand)
        {
            var player1deck = deck.Item1;
            var player1history = hand.Item1;
            if (player1deck.Count != player1history.Count)
                return false;
            for (int i = 0; i < player1deck.Count; i++)
            {
                if (player1deck[i] != player1history[i])
                    return false;
            }

            var player2deck = deck.Item2;
            var player2history = hand.Item2;
            if (player2deck.Count != player2history.Count)
                return false;
            for (int i = 0; i < player2deck.Count; i++)
            {
                if (player2deck[i] != player2history[i])
                    return false;
            }
            return true;
        }
    }
}
