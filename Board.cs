namespace COMP4106_A2 {
    class Board 
    {
        //constant defaults
        private const byte DefaultRows      = 5;//Rows must be greater than 4 because of the way that the chain search works
        private const byte DefaultColumns   = 7;
        private const bool DefaultExpert    = false;
        

        //variables
        private bool    expert;
        private byte columns;
        private byte[,] grid;


        //constructors
        public Board() : this(DefaultRows, DefaultColumns, DefaultExpert) {}// default constructor -- set all to default
        public Board(bool expertMode) : this(DefaultRows, DefaultColumns, expertMode) {} //set expert mode on or explicitly state that it's off
        public Board(byte rows, byte columns) : this(rows, columns, DefaultExpert) {} // set just the grid
        
        public Board(byte numRows, byte numColumns, bool expertMode){//set everyting (where all other constructors resolve to anyway)
            expert = expertMode;
            columns = numColumns;
            grid = new byte[numRows, numColumns];
        }
        public Board(Board original) : this(original.Rows, original.columns, original.expert) {
            for (short r = 0; r < original.Rows; r++)
                for (short c = 0; c < original.columns; c++)
                    grid[r, c] = original.grid[r, c];
        }


        //getters
        public byte     Columns { get{return columns;} }
        public byte     Rows    { get{return (byte)(grid.Length/columns);}}
        public bool     Expert  { get{return expert;} }
        public byte[,]  Grid    { get{return grid;} }

        public byte getWinner() { return ownerLongestChain(); }


        //public methods
        public bool add (byte column, byte playerId) {//true if action was made false if error with the move
            if (column < 0 || column >= columns)//false if out of bounds
                return false;
            if (grid[Rows-1, column] != 0)//false if column is full
                return false;
            if (checkWin())//false if winner exists
                return false;

            for (byte r = 0; r < Rows; r++)// finally actually add something
                if (grid[r, column] == 0){ // so go up utill the thing can fit in the spot
                    grid[r, column] = playerId;
                    break;
                }
            return true;
        }
        public bool remove (byte column, byte playerId) {//true if action was made false if error with the move
            if (!expert) //only enabled on expert games
                return false;
            if (column < 0 || column >= columns) //false if out of bounds
                return false;
            if (grid[0, column] == 0) //false if no piece
                return false;
            if (grid[0, column] != playerId) //false if wrong player
                return false;
            if (checkWin()) //false if winner exists
                return false;

            for (byte r = 0; r < Rows-1; r++){ // finally remove from the board
                grid[r, column] = grid[r+1, column];//shift all down
                if (grid[r, column] == 0)//if hit 0 exit
                    break;
            }
            return true;
        }
        public bool checkWin() {//true if they won
            return findLongestChain() >= 4;
        }
        public bool canRemove(byte playerId) {
            for (byte c = 0; c < columns; c++)
                if (grid[0, c] == playerId)
                    return true;//only returns if it finds a valid move
            return false;
        }
        public bool hasMove() {
            if (checkWin())
                return false;
            if (expert)
                return true;
            for (byte c = 0; c < columns; c++)
                if (grid[Rows-1, c] == 0)
                    return true;
            return false;//nothing open on the top and can't take from bottom therefore no moves edge case
        }
        public short score () {
            short score = 0;
            score += heruristic1();
            score += heruristic2();
            return score;
        }
        override public string ToString() {
            string s = "  Y\n";

            //turn the grid into a string
            for (int r = Rows-1; r >= 0; r--){
                s += string.Format("{0, 3}│", r);
                for (byte c = 0; c < columns; c++)
                    s += string.Format("{0, 3}", grid[r, c]);
                s+='\n';
            }
            s += "   └";
            for (byte c = 0; c < columns; c++)
                s += "───";
            s += " X\n    ";
            for (byte c = 0; c < columns; c++)
                s += string.Format("{0, 3}", c);

            return s + '\n';
        }
    


        //helper functions
        private enum direction { up=0, right=1, upRight=2, upLeft=3 }//only have to do half of the directions
        private byte findLongestChain() {//basically just for evaluating win conditions
            byte longestChain = 0;
            byte chain = 0;
            for (byte r = 0; r < Rows-3; r++){//This is really inefficient, ignore it please...
                for (byte c = 0; c < columns; c++)
                    if (grid[r, c] != 0){
                        for (byte i = 0; i <= 3; i++){
                            chain = goFetch(r, c, (Board.direction)i, grid[r, c], 0);
                            if (chain > longestChain)
                                longestChain = chain;
                        }
                    }
            }
            return longestChain;
        }
        private byte goFetch(byte r, byte c, direction dir, byte id, byte depth) {
            if (r >= Rows)
                return depth;
            if (c >= columns)
                return depth;
            if (grid[r, c] == id) {
                switch (dir) {
                    case direction.up:
                        return goFetch(++r, c, dir, id, ++depth);
                    case direction.right:
                        return goFetch(r, ++c, dir, id, ++depth);
                    case direction.upRight:
                        return goFetch(++r, ++c, dir, id, ++depth);
                    case direction.upLeft:
                        return goFetch(++r, --c, dir, id, ++depth);
                }
            }
            return depth;//this is an else clause really
        }
        private byte ownerLongestChain() {//This will return the owner of the first of length 4 it finds, edge cases for larger are if larger it will take it, if multiple vs one it still takes the first it finds.
            byte longestChain = 0;
            byte chain = 0;
            byte owner = 0;
            for (byte r = 0; r < Rows-3; r++){//This is really inefficient, ignore it please...
                for (byte c = 0; c < columns; c++)
                    if (grid[r, c] != 0){
                        for (byte i = 0; i <= 3; i++){
                            chain = goFetch(r, c, (Board.direction)i, grid[r, c], 0);
                            if (chain > longestChain){
                                longestChain = chain;
                                owner = grid[r, c];
                            }
                        }
                    }
            }
            return owner;
        }
        private short heruristic1 () {
            byte chain;
            short score = 0;
            for (byte r = 0; r < Rows; r++){//This is really inefficient, ignore it please...
                for (byte c = 0; c < columns; c++)
                    if (grid[r, c] != 0){
                        for (byte i = 0; i <= 3; i++){
                            chain = goFetch(r, c, (Board.direction)i, grid[r, c], 0);
                            if (grid[r, c] == 1)
                                score += (short)(2^chain);
                            else
                                score -= (short)((2^chain));
                        }
                    }
            }
            return score;
        }
        private short heruristic2 () {
            byte chain;
            short score = 0;
            for (byte r = 0; r < Rows; r++){
                for (byte c = 0; c < columns; c++)
                    if (grid[r, c] != 0){
                        for (byte i = 0; i <= 3; i++){
                            chain = H2Fetch(r, c, (Board.direction)i, grid[r, c], 0);
                            if (grid[r, c] == 1)
                                score += (short)(2^chain);
                            else
                                score -= (short)(2^chain);
                        }
                    }
            }
            return score;
        }
        private byte H2Fetch(byte r, byte c, direction dir, byte id, byte depth) {// This algorithm is made specifically for the heuristic where interupting a chain will give bonus score to incentivise minmax AI to play defensively
            if (r >= Rows)
                return depth;
            if (c >= columns)
                return depth;
            if (grid[r, c] == id) {
                switch (dir) {
                    case direction.up:
                        return goFetch(++r, c, dir, id, ++depth);
                    case direction.right:
                        return goFetch(r, ++c, dir, id, ++depth);
                    case direction.upRight:
                        return goFetch(++r, ++c, dir, id, ++depth);
                    case direction.upLeft:
                        return goFetch(++r, --c, dir, id, ++depth);
                }
            }
            if (grid[r, c] != 0 && grid[r, c] != id)
                return depth;
            return 0;
        }
    }
}