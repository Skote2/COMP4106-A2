using System;
using System.Collections;
using System.Collections.Generic;

namespace COMP4106_A2{
    class MinMaxTree{
        public class Node {
            protected Node parent;
            public Node minChild;
            public Node maxChild;
            public List<Node> children;
            short moveFromParent;
            public short MoveFromParent { get{return moveFromParent;} }
            public short score;
            Board game;
            public Board Game { get{return game;} }

            public Node(Board b) : this(b, null, -1) { }
            public Node(Board b, Node parentNode, short moveMade) {
                moveFromParent = moveMade;
                parent = parentNode;
                children = new List<Node>();
                game = b;
                score = calculateScore();
            }
            private short calculateScore() {
                return game.score();
            }
        }


        // Variables
        private Node head;
        public Node Head { get{return head;} }
        private bool isBuilt;
        private byte depth;
        private byte playerId;


        // Constructor
        public MinMaxTree(Board currentGameState, byte usedPlayerId, byte depthOfTree) {
            head = new Node(currentGameState);
            depth = depthOfTree;
            playerId = usedPlayerId;
        }

        //Pulically accessable methods
        private void buildTree(Node parent, short curDepth){
            Random r = new Random();
            Board copy;
            bool validPlay;
            if (curDepth > 0){
                if (!parent.Game.Expert){
                    if (parent.Game.hasMove()){
                        for (byte c = 0; c < parent.Game.Columns; c++){
                            copy = new Board(parent.Game);
                            if (depth%2 == 0 ? curDepth % 2 == 0 : curDepth % 2 == 1)//if even
                                validPlay = copy.add(c, 1);
                            else
                                validPlay = copy.add(c, 2);
                            
                            if (validPlay)
                                parent.children.Add(new Node(copy, parent, c));
                        }
                        short minScore = short.MaxValue, maxScore = short.MinValue;
                        foreach (Node child in parent.children){
                            if (child.score < minScore){
                                parent.minChild = child;
                                minScore = child.score;
                            }
                            else if (child.score == minScore)
                                if (r.Next(2)%2 == 1)//50% chance to switch if even stat
                                    parent.minChild = child;
                            if (child.score > maxScore){
                                parent.maxChild = child;
                                maxScore = child.score;
                            }
                            else if (child.score == maxScore)
                                if (r.Next(2) == 1)//50% chance to switch if even stat
                                    parent.maxChild = child;
                        }
                        buildTree(parent.maxChild, (short)(curDepth-1));
                        buildTree(parent.minChild, (short)(curDepth-1));
                    }
                }
                else {
                    if (parent.Game.hasMove()){
                        for (byte c = 0; c < parent.Game.Columns; c++){
                            copy = new Board(parent.Game);
                            if (depth%2 == 0 ? curDepth % 2 == 0 : curDepth % 2 == 1)//if even
                                validPlay = copy.add(c, 1);
                            else
                                validPlay = copy.add(c, 2);
                            
                            if (validPlay)
                                parent.children.Add(new Node(copy, parent, c));
                        }
                        for (byte c = 0; c < parent.Game.Columns; c++){
                            copy = new Board(parent.Game);
                            if (depth%2 == 0 ? curDepth % 2 == 0 : curDepth % 2 == 1)//if even
                                validPlay = copy.remove(c, 1);
                            else
                                validPlay = copy.remove(c, 2);
                            
                            if (validPlay)
                                parent.children.Add(new Node(copy, parent, (short)(c+parent.Game.Columns)));
                        }
                        
                        short minScore = short.MaxValue, maxScore = short.MinValue;
                        foreach (Node child in parent.children){
                            if (child.score < minScore){
                                parent.minChild = child;
                                minScore = child.score;
                            }
                            else if (child.score == minScore)
                                if (r.Next(2)%2 == 1)//50% chance to switch if even stat
                                    parent.minChild = child;
                            if (child.score > maxScore){
                                parent.maxChild = child;
                                maxScore = child.score;
                            }
                            else if (child.score == maxScore)
                                if (r.Next(2)%2 == 1)//50% chance to switch if even stat
                                    parent.maxChild = child;
                        }
                        buildTree(parent.maxChild, (short)(curDepth-1));
                        buildTree(parent.minChild, (short)(curDepth-1));
                    }
                }
            }
            isBuilt = true;
        }

        public Queue<byte> getMoves () {
            if (!isBuilt)
                buildTree(head, depth);
            minmaxTree(head, true, depth);
            Queue<byte> commands = new Queue<byte>();
            Node cur = head;
            for (byte d = depth; d != 0; d--){
                if (cur.maxChild.score == cur.score)
                    commands.Enqueue((byte)cur.maxChild.MoveFromParent);
                else
                    commands.Enqueue((byte)cur.minChild.MoveFromParent);
            }
            return commands;
        }

        private byte minmaxTree(Node curNode, bool maximizingPlayer, short curDepth){
            if (curDepth == 0)// || game over in position)
                return (byte)curNode.score;//static evaluation of position
            if (curNode.children.Count == 0)
                return (byte)curNode.score;
            if (maximizingPlayer){
                curNode.score = (short)minmaxTree(curNode.maxChild, false, (short)(curDepth-1));
                return (byte)curNode.score;
            }
            else{
                curNode.score = minmaxTree(curNode.minChild, true, (short)(curDepth-1));
                return (byte)curNode.score;
            }
        }
    }
}
