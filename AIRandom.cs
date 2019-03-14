using System;

namespace COMP4106_A2 {
    class AIRandom : Player
    {
        public AIRandom() : base() {}

        override public void move (Board b) {//makes a random valid move on the board
            Random r = new Random();

            //b.add(r.Next(b.Columns));
        }
    }
}