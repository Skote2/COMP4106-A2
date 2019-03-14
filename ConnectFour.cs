using System;
namespace COMP4106_A2
{
    class ConnectFour
    {
        static void Main(string[] args)
        {
            Board gameBoard = new Board(true);
            
            Player[] players = new Player[2];
            players[0] = new AIMinMax();
            players[1] = new AIRandom();
            byte currentPlayer = 0;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press a key for each move.\n");
            // outputToConsole(gameBoard.ToString());
            int wins = 0;
            int loops = 50;
            for (int i = 0; i < loops; i++){
                gameBoard = new Board(true);
                while (!gameBoard.checkWin() && gameBoard.hasMove()){
                    players[currentPlayer++].move(gameBoard);
                    if (currentPlayer >= players.Length) currentPlayer = 0;

                    // Console.ReadKey();//delay for a keypress
                    // outputToConsole(gameBoard.ToString());
                }
                if (gameBoard.getWinner() == 1)
                    wins++;
                if (i%10 == 0)
                    Console.WriteLine("{0,3}%", i/0.5);
            }
            Console.WriteLine("The AI won {0}/50 games", wins);
            // if (gameBoard.checkWin())
            //     Console.WriteLine("Player id: " + gameBoard.getWinner() + " Wins!!!");
            // if (!gameBoard.hasMove() && !gameBoard.checkWin())
            //     Console.WriteLine("Cat's game, no moves left.");
            Console.WriteLine("Exiting.");
        }

        private static void outputToConsole(string s) {
            foreach (char c in s) {
                if (c == '1'){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(c);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (c == '2') {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(c);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                    Console.Write(c);
            }
        }
    }
}