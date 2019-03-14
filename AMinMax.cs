namespace COMP4106_A2 {
    class AIMinMax : Player
    {
        public AIMinMax() : base() {}

        override public void move(Board b) {
            //remember to bound the search
            //heuristics are
            //  1. an exponentally growing value based on length of the chain made for current board state (1 for 1 long, 2 for 2 long, 4 for 3 long, 8 for 4 long)
            //  2. same thing but as points for tokens which intercept oponent rows.
        }
    }
}