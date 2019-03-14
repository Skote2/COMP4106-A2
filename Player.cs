namespace COMP4106_A2 {
    abstract class Player {
        private static byte nextId = 1;
        private byte id;

        public Player () { id = nextId++; }
        public abstract void move (Board b);
    }
}