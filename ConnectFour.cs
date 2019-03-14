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
            players[0] = new AIMinMax();
            players[1] = new AIRandom();
            byte currentPlayer = 0;

            while (!gameBoard.checkWin()){
                players[currentPlayer++].move(gameBoard);
                if (currentPlayer >= players.Length) currentPlayer = 0;
            }

            Console.WriteLine("Hello World!" + gameBoard.getWinner());
        }
    }
}