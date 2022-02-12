using System;
using System.Collections.Generic;
using System.Threading;
using TehDeck;

namespace Example {
    public class Game {

        private enum PlayerMove {
            DRAW,
            ADD,
            EXIT,
            INVALID,
            DEBUG
        }

        private const ConsoleColor PLAYER_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor AI_COLOR = ConsoleColor.Yellow;

        private Deck deck;

        private bool isPlayerTurn;

        private bool isGameOver;

        public Game() {
            DeckInfo deckInfo = new DeckInfo(6, GenerateCardInfos());

            deck = new Deck(deckInfo);
            isPlayerTurn = true;
            isGameOver = false;

            #region Local_Function

            List<CardInfo> GenerateCardInfos() {
                List<CardInfo> cardInfos = new List<CardInfo>();
                cardInfos.Add(new CardInfo(new JokerCard(), 1));
                cardInfos.Add(new CardInfo(new BlankCard()));
                return cardInfos;
            }

            #endregion
        }

        public void Start() {
            Print($"Do not draw the joker!{Environment.NewLine + Environment.NewLine}Use 'DRAW' to draw a card.{Environment.NewLine}Use 'ADD' to add cards into the deck and shuffle before drawing.{Environment.NewLine}'EXIT' if you wish to exit the game.{Environment.NewLine}");
        }

        public bool Update() {
            if (isGameOver) {
                ResetGame();
                return false;
            } else if (isPlayerTurn) {
                return UpdatePlayerTurn();
            } else {
                UpdateAiTurn();
                return false;
            }
        }

        private bool UpdatePlayerTurn() {
            PlayerMove playerMove = GetPlayerMove();

            if (playerMove == PlayerMove.EXIT) {
                return true;
            } else if (playerMove == PlayerMove.INVALID) {
                Print("Invalid Move! Do 'DRAW', 'ADD' or 'EXIT'." + Environment.NewLine, ConsoleColor.Red);
            } else if (playerMove == PlayerMove.DRAW) {
                PerformDraw();
            } else if (playerMove == PlayerMove.ADD) {
                PerformDraw(true);
            } else if (playerMove == PlayerMove.DEBUG) {
                ShowWholeDeck();
            }

            return false;

            #region Local_Function

            PlayerMove GetPlayerMove() {
                Print($"There are {deck.CardCount} cards left in the deck.{Environment.NewLine}Make your move. 'DRAW', 'ADD' or 'EXIT'.", PLAYER_COLOR);
                string input = Console.ReadLine();
                Print("");

                switch (input.ToUpper()) {
                    case "DRAW":
                        return PlayerMove.DRAW;
                    case "ADD":
                        return PlayerMove.ADD;
                    case "EXIT":
                        return PlayerMove.EXIT;
                    case "DEBUG":
                        return PlayerMove.DEBUG;
                    default:
                        return PlayerMove.INVALID;
                }
            }

            #endregion
        }

        private void UpdateAiTurn() {
            Print($"There are {deck.CardCount} cards left in the deck.{Environment.NewLine}The AI makes a move...{Environment.NewLine}", AI_COLOR);
            Thread.Sleep(2000);

            if (deck.CardCount == 1) {
                PerformDraw(true);
            } else {
                PerformDraw();
            }
        }

        private void PerformDraw(bool withAdd = false) {
            string refer = isPlayerTurn ? "You" : "The AI";
            var msgColor = isPlayerTurn ? PLAYER_COLOR : AI_COLOR;


            if (withAdd) {
                Print($"{refer} adds 6 more cards to the deck and shuffles...", msgColor);
                AddCard();
            }

            var drawnCard = deck.NextCard();

            Print($"{refer} draw a {drawnCard.Name}.", msgColor);

            if (drawnCard.GetType() == typeof(JokerCard)) {
                Print($"{refer} have lost the game...{Environment.NewLine}", isPlayerTurn ? ConsoleColor.Red : ConsoleColor.Green);
                isGameOver = true;
            } else {
                Print($"{refer} have passed the turn...{Environment.NewLine}", msgColor);
                isPlayerTurn = !isPlayerTurn;
            }

            #region Local_Function

            void AddCard() {
                deck.PushCard(new JokerCard());
                var blankCard = new BlankCard();
                for (int i = 0; i < 5; ++i) {
                    deck.PushCard(blankCard);
                }

                deck.Shuffle();
            }

            #endregion
        }

        private void ResetGame() {
            Print($"Resetting game...{Environment.NewLine}");
            deck.ResetDeck();
            isGameOver = false;
        }

        private void ShowWholeDeck() {
            Print($"{Environment.NewLine}Showing the whole deck...{Environment.NewLine}");
            foreach (var item in deck.RevealDeck()) {
                Print($"{item.Name}");
            }
            Print($"{Environment.NewLine}End of deck...{Environment.NewLine}");
        }

        private static void Print(string msg, ConsoleColor consoleColor = ConsoleColor.White) {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(msg);
        }
    }
}
