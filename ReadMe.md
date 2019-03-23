# COMP4106-A2: Connect Four

Carleton University\
Winter 2019

Artificial Intelligence\
John Oommen\
Assignment 2

By: David N. Zilio (Skote2) \
Demo: 12:00 Thursday Mar 14<sup>th</sup> 2019\
TA: Karim Hersi

This is the second and final assignment in the _COMP4106 - Artificial intelligence_ course, excluding the final project.\
This assignment is, the simple game most people are aquainted with, __Connect Four__.

## Connect Four

### Game

 More specifically this is a version which has the option to allow players to remove their own tiles from the bottom row. This is quoted as an _expert mode_. The standard rules with regards to play are the same: a player may add their disc to any column that is not full, a player wins when they create a straight, vertical or diagonal line of 4 discs in a row, and a cats game is when the grid is filled leaving no moves with no winner (note that this is not possible in expert mode).

### AI

 The AI in this game have two variants: a random variant which has no strategy at all, and a heuristic based minmax alpha-beta pruned tree. The random AI needs no further explanation. The heuristic based AI on the other hand has a large number of descriptors packed in there so They're broken down below.

#### Heruistics

There are two heuristics which I had to define that the AI will use to score a board state, allowing it to determine wheter boards are supperior to one another.

1. For the first heuristic I chose to build it an exponential point system of sequential pieces. For any line that the AI owns on the board the AI recieves points equal to 2 to the power of the length of the line. This means a line of length 1 is worth 2 points, a line of length 4 is worth 16 points. It's good to offer mild encouragement to the AI for making moves even if they don't do anything too special so that the game progresses to a state that the AI can make decent moves, eg. if nothing is on the board then the AI won't be inclined to do anything so give it points for doing something.
2. The second heruistic is the same point system but based instead of lengthening the AI's own lists the AI will gain points for blocking other players' chains. The only modification to the point system is that it will be 2^x -1, the minus one. The minus one is in place to give points for blocking the other player but not to incentivise the AI to try and block the other player over taking the oportunity to win.

#### MinMax Tree

The MinMax tree is a single parent many child tree which is built from top down, the root node is the current, real, board state and children off of parent are the possible moves from that board state. On the way back up determining the "ideal" decision is to alternate between taking the best and the worst scored children up through the parent alternating between best and worst on even and odd hights. This is where the name min max comes from because the tree is alternating between minumum and maximum states every level of height.

* Alpha-Beta Pruning:\
since the only children elidgable to be brough up the tree are the minimum and maximum the queue that loads the next depth of predictions to get children will be told only to load minumums and maximums with respect to a parent. By only calculating down the tree for the minumum and maximum children a very large ammount of useless calculations is avoided.

### Performance

Light tweaking of the Heuristics can have massive effects but generally the Intelligent AI won 80+% of the time. Some light tweaks were observed making the AI significantly more capable (90%ish). I'm certain there is some detail in the implementation which requires a slight tweak to get the AI winning nearly every game. It seems to prioritize stacking over anything else even when it doesn't make sense to do so, which is likely a result of the way I'm rewarding the AI. The heuristics rewarded the AI for extending lines even if they can't win so it doesn't know if it's following frugal routes. This could have been fixed but I just never got around to it.