using System;

namespace Example {
    class Program {

        // A simple game where you attempt to not draw a joker card against a dummy AI.

        // 'DRAW' -> Draws a card.
        // 'ADD' -> Add cards to the deck and shuffles before drawing.
        // 'EXIT' -> Exits the game.
        static void Main(string[] args) {

            Game game = new Game();
            game.Start();

            bool exitGame;
            do {
                exitGame = game.Update();
            } while (!exitGame);

            Console.WriteLine("Press anything to exit...");
            Console.ReadKey();
        }
    }
}
