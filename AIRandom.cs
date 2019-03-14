using System;

namespace COMP4106_A2 {
    class AIRandom : Player
    {
        public AIRandom() : base() {}

        override public void move (Board b) {//makes a random valid move on the board
            Random r = new Random();
            if (b.Expert){
                byte remove = (byte)r.Next(5);
                if (remove == 0 && b.canRemove(Id))
                    while (!b.remove((byte)r.Next(b.Columns), Id));
                else
                    while (!b.add((byte)r.Next(b.Columns), Id));
            }
            else
                while (!b.add((byte)r.Next(b.Columns), Id));//loop placements until it makes a good one.
        }
    }
}