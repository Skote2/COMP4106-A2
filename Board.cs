namespace COMP4106_A2 {
    class Board 
    {
        //constant defaults
        private const byte DefaultRows      = 5;
        private const byte DefaultColumns   = 7;
        private const bool DefaultExpert    = false;
        

        //variables
        private bool    expert;
        private byte[,] grid;


        //constructors
        public Board() : this(DefaultRows, DefaultColumns, DefaultExpert) {}// default constructor -- set all to default
        public Board(bool expertMode) : this(DefaultRows, DefaultColumns, expertMode) {} //set expert mode on or explicitly state that it's off
        public Board(byte rows, byte columns) : this(rows, columns, DefaultExpert) {} // set just the grid
        
        public Board(byte rows, byte columns, bool expertMode){//set everyting (where all other constructors resolve to anyway)
            expert = expertMode;
            grid = new byte[rows, columns];
        }


        //getters
        public byte     Columns { get{return Columns;} }
        public bool     Expert  { get{return expert;} }
        public byte[,]  Grid    { get{return grid;} }

        public byte getWinner() { return ownerLongestChain(); }


        //public methods
        public bool add (byte column, byte playerId) {//true if action was made false if error with the move
            //add to the board
            //false if out of bounds
            //false if column is full
            //false if winner

            return checkWin();
        }
        public bool remove (byte column, byte playerId) {//true if action was made false if error with the move
            //remove from the board
            //false if wrong player
            //false if no piece
            //false if out of bounds
            //false if winner
            
            return checkWin();
        }
        public bool checkWin() {//true if they won
            return findLongestChain() >= 4;
        }
        override public string ToString() {
            string s = "";

            //turn the grid into a string

            return s;
        }


        //helper functions
        private byte findLongestChain() {//basically just for evaluating win conditions
            return 0;
        }
        private byte ownerLongestChain() {
            return 0;
        }
    }
}