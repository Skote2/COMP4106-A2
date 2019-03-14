using System;
namespace COMP4106_A2
{
    class ConnectFour
    {
        static void Main(string[] args)
        {
            // byte rows = 5;
            // byte columns = 7;
            // bool expertMode = false;
            // foreach (string s in args){
            //     switch(s)
            //     {
            //         case "-E":
            //             expertMode = true;
            //             break;
            //         case "-R="
            //     }
                    
            // }
            Board gameBoard = new Board();
            
            Player[] players = new Player[2];
            players[0] = new AIRandom();
            players[1] = new AIRandom();
            byte currentPlayer = 0;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press a key for each move.\n");
            outputToConsole(gameBoard.ToString());
            while (!gameBoard.checkWin() && gameBoard.hasMove()){
                players[currentPlayer++].move(gameBoard);
                if (currentPlayer >= players.Length) currentPlayer = 0;

                Console.ReadKey();//delay for a keypress
                
                outputToConsole(gameBoard.ToString());
            }
            if (gameBoard.checkWin())
                Console.WriteLine("Player id: " + gameBoard.getWinner() + " Wins!!!");
            if (!gameBoard.hasMove())
                Console.WriteLine("Cat's game, no moves left.");
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