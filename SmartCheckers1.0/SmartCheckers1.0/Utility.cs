using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SmartCheckers1._0
{
    class Utility
    {
        public List<State> GetPossibleStates(String[,] board, int color)
        {
            
            List<State> states = new List<State>();
            //this.PrintBoard(board);

            //Get the list of possible catches for each piece and add them to states list
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    String currentSqr = board[i, j];
                    Vector2 position = new Vector2(i, j);

                    if (color == -1)
                    {
                        if ((currentSqr == "w") || (currentSqr == "W"))
                        {
                            states.AddRange(GetCatches(board, color, position));
                        }
                    }
                    if (color == 1)
                    {
                        if ((currentSqr == "b") || (currentSqr == "B"))
                        {
                            states.AddRange(GetCatches(board, color, position));
                        }
                    }
                }
            }

            if (states.Count == 0)  //if there are no catches found
            {
                //Get the list of possible moves for each piece and add them to states list
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    String currentSqr = board[i, j];
                    Vector2 position = new Vector2(i, j);

                    if(color == -1)
                    {
                        if ((currentSqr == "w") || (currentSqr == "W"))
                        {
                            states.AddRange(GetMoves(board, color, position));
                        }
                    }
                    if (color == 1)
                    {
                        if ((currentSqr == "b") || (currentSqr == "B"))
                        {
                            states.AddRange(GetMoves(board, color, position));
                        }
                    }
                    
                }
            }
            }

            //PrintStates(states);

            return states;
        }

        public void PrintStates(List<State> states){
            Console.WriteLine(states.Count + " states received.");

            foreach (State state in states)
            {
                List<Move> moves = state.moves;
                Console.Write("Moves: ");
                foreach (Move move in moves)
                {
                    Console.Write(move.from.X + "|" + move.from.Y + "->" + move.to.X + "|" + move.to.Y);
                    Console.Write("::::");
                }
                Console.WriteLine();

                PrintBoard(state.board);
            }
                        
            Console.WriteLine("#############################################################");
        }

        public List<State> GetCatches(String[,] board, int color, Vector2 current)
        {
            List<State> catches = new List<State>();

            //if white or king
            if ((color == -1) || (board[(int)current.X, (int)current.Y] == "B") || (board[(int)current.X, (int)current.Y] == "W"))
            {
                //find the opposite color
                String oppositeColor="";
                if ((board[(int)current.X, (int)current.Y] == "b") || (board[(int)current.X, (int)current.Y] == "B"))
                {
                    oppositeColor = "w";
                }
                if ((board[(int)current.X, (int)current.Y] == "w") || (board[(int)current.X, (int)current.Y] == "W"))
                {
                    oppositeColor = "b";
                }

                //left side
                if ((current.X > 1) && (current.Y < 6) && (board[(int)current.X - 1, (int)current.Y + 1] != "*") && (board[(int)current.X - 1, (int)current.Y + 1].ToLower() == oppositeColor) && (board[(int)current.X - 2, (int)current.Y + 2] == "*"))
                {
                    String[,] tempBoard = new String[8, 8];
                    Array.Copy(board, tempBoard, 64);

                    tempBoard[(int)current.X - 1, (int)current.Y + 1] = "*";
                    tempBoard[(int)current.X - 2, (int)current.Y + 2] = tempBoard[(int)current.X, (int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    Move myMove = new Move(new Vector2(current.X, current.Y), new Vector2(current.X - 2, current.Y + 2));

                    List<State> nextStates = GetCatches(tempBoard, color, new Vector2(current.X - 2, current.Y + 2));
                    if (nextStates.Count == 0)
                    {
                        List<Move> tempList = new List<Move>();
                        tempList.Add(myMove);
                        catches.Add(new State(tempBoard, tempList));
                    }
                    else
                    {
                        foreach (State next in nextStates)
                        {
                            next.moves.Insert(0, myMove);
                            catches.Add(next);
                        }
                    }
                }

                //right side
                if ((current.X < 6) && (current.Y < 6) && (board[(int)current.X + 1, (int)current.Y + 1] != "*") && (board[(int)current.X + 1, (int)current.Y + 1].ToLower() == oppositeColor) && (board[(int)current.X + 2, (int)current.Y + 2] == "*"))
                {
                    String[,] tempBoard = new String[8, 8];
                    Array.Copy(board, tempBoard, 64);

                    tempBoard[(int)current.X + 1, (int)current.Y + 1] = "*";
                    tempBoard[(int)current.X + 2, (int)current.Y + 2] = tempBoard[(int)current.X, (int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    Move myMove = new Move(new Vector2(current.X, current.Y), new Vector2(current.X + 2, current.Y + 2));

                    List<State> nextStates = GetCatches(tempBoard, color, new Vector2(current.X + 2, current.Y + 2));
                    if (nextStates.Count == 0)
                    {
                        List<Move> tempList = new List<Move>();
                        tempList.Add(myMove);
                        catches.Add(new State(tempBoard, tempList));
                    }
                    else
                    {
                        foreach (State next in nextStates)
                        {
                            next.moves.Insert(0, myMove);
                            catches.Add(next);
                        }
                    }
                }
            }

            //if black or king
            if ((color == 1) || (board[(int)current.X, (int)current.Y] == "B") || (board[(int)current.X, (int)current.Y] == "W"))
            {
                //find the opposite color
                String oppositeColor="";
                if ((board[(int)current.X, (int)current.Y] == "b") || (board[(int)current.X, (int)current.Y] == "B"))
                {
                    oppositeColor = "w";
                }
                if ((board[(int)current.X, (int)current.Y] == "w") || (board[(int)current.X, (int)current.Y] == "W"))
                {
                    oppositeColor = "b";
                }

                //left side
                if ((current.X > 1) && (current.Y > 1) && (board[(int)current.X - 1, (int)current.Y - 1] != "*") && (board[(int)current.X - 1, (int)current.Y - 1].ToLower() == oppositeColor) && (board[(int)current.X - 2, (int)current.Y - 2] == "*"))
                {
                    String[,] tempBoard = new String[8, 8];
                    Array.Copy(board, tempBoard, 64);

                    tempBoard[(int)current.X - 1, (int)current.Y - 1] = "*";
                    tempBoard[(int)current.X - 2, (int)current.Y - 2] = tempBoard[(int)current.X, (int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    Move myMove = new Move(new Vector2(current.X, current.Y), new Vector2(current.X - 2, current.Y - 2));

                    List<State> nextStates = GetCatches(tempBoard, color, new Vector2(current.X - 2, current.Y - 2));
                    if (nextStates.Count == 0)
                    {
                        List<Move> tempList = new List<Move>();
                        tempList.Add(myMove);
                        catches.Add(new State(tempBoard, tempList));
                    }
                    else
                    {
                        foreach (State next in nextStates)
                        {
                            next.moves.Insert(0, myMove);
                            catches.Add(next);
                        }
                    }
                }

                //right side
                if ((current.X < 6) && (current.Y > 1) && (board[(int)current.X + 1, (int)current.Y - 1] != "*") && (board[(int)current.X + 1, (int)current.Y - 1].ToLower() == oppositeColor) && (board[(int)current.X + 2, (int)current.Y - 2] == "*"))
                {
                    String[,] tempBoard = new String[8, 8];
                    Array.Copy(board, tempBoard, 64);

                    tempBoard[(int)current.X + 1, (int)current.Y - 1] = "*";
                    tempBoard[(int)current.X + 2, (int)current.Y - 2] = tempBoard[(int)current.X, (int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    Move myMove = new Move(new Vector2(current.X, current.Y), new Vector2(current.X + 2, current.Y - 2));

                    List<State> nextStates = GetCatches(tempBoard, color, new Vector2(current.X + 2, current.Y - 2));
                    if (nextStates.Count == 0)
                    {
                        List<Move> tempList = new List<Move>();
                        tempList.Add(myMove);
                        catches.Add(new State(tempBoard, tempList));
                    }
                    else
                    {
                        foreach (State next in nextStates)
                        {
                            next.moves.Insert(0, myMove);
                            catches.Add(next);
                        }
                    }
                }
            }

            return catches;
        }

        public List<State> GetMoves(String[,] board, int color, Vector2 current)
        {
            List<State> moves = new List<State>();
            //Square[,] tempBoard = board;

            //if white or king
            if ((color == -1) || (board[(int)current.X,(int)current.Y] == "B") || (board[(int)current.X,(int)current.Y] =="W"))
            {
                //Console.WriteLine("white");

                //left side
                if ((current.X > 0) && (current.Y < 7) && (board[(int)current.X - 1, (int)current.Y + 1] == "*"))
                {
                    String[,] tempBoard = new String[8,8];
                    Array.Copy(board,tempBoard,64);

                    tempBoard[(int)current.X-1,(int)current.Y+1] = tempBoard[(int)current.X,(int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    List<Move> tempList = new List<Move>();
                    tempList.Add(new Move(new Vector2(current.X, current.Y), new Vector2(current.X - 1, current.Y + 1)));
                    moves.Add(new State(tempBoard,tempList));
                    //moves.Add(new Move(tempBoard, new Vector2(current.X, current.Y), new Vector2(current.X - 1, current.Y + 1)));
                }

                //right side
                if ((current.X < 7) && (current.Y < 7) && (board[(int)current.X + 1, (int)current.Y + 1] == "*"))
                {
                    String[,] tempBoard = new String[8, 8];
                    Array.Copy(board, tempBoard, 64);

                    tempBoard[(int)current.X + 1, (int)current.Y + 1] = tempBoard[(int)current.X, (int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    List<Move> tempList = new List<Move>();
                    tempList.Add(new Move(new Vector2(current.X, current.Y), new Vector2(current.X + 1, current.Y + 1)));
                    moves.Add(new State(tempBoard, tempList));
                    //moves.Add(new Move(tempBoard, new Vector2(current.X, current.Y), new Vector2(current.X + 1, current.Y + 1)));
                }
            }

            //if black or king
            if ((color == 1) || (board[(int)current.X, (int)current.Y] == "B") || (board[(int)current.X, (int)current.Y] == "W"))
            {
                //Console.WriteLine("black");
                //left side
                if ((current.X > 0) && (current.Y > 0) && (board[(int)current.X - 1, (int)current.Y - 1] == "*"))
                {
                    String[,] tempBoard = new String[8, 8];
                    Array.Copy(board, tempBoard, 64);

                    tempBoard[(int)current.X - 1, (int)current.Y - 1] = tempBoard[(int)current.X, (int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    List<Move> tempList = new List<Move>();
                    tempList.Add(new Move(new Vector2(current.X, current.Y), new Vector2(current.X - 1, current.Y - 1)));
                    moves.Add(new State(tempBoard, tempList));
                    //moves.Add(new Move(tempBoard, new Vector2(current.X, current.Y), new Vector2(current.X - 1, current.Y - 1)));
                }

                //right side
                if ((current.X < 7) && (current.Y > 0) && (board[(int)current.X + 1, (int)current.Y - 1] == "*"))
                {
                    String[,] tempBoard = new String[8, 8];
                    Array.Copy(board, tempBoard, 64);

                    tempBoard[(int)current.X + 1, (int)current.Y - 1] = tempBoard[(int)current.X, (int)current.Y];
                    tempBoard[(int)current.X, (int)current.Y] = "*";

                    List<Move> tempList = new List<Move>();
                    tempList.Add(new Move(new Vector2(current.X, current.Y), new Vector2(current.X + 1, current.Y - 1)));
                    moves.Add(new State(tempBoard, tempList));
                    //moves.Add(new Move(tempBoard, new Vector2(current.X, current.Y), new Vector2(current.X + 1, current.Y - 1)));
                }
            }

            return moves;
        }

        public String[,] ConvertBoard(Square[,] board)
        {
            String[,] retArray = new String[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Square tempSquare = board[i, j];
                    String tempString = "";

                    if (tempSquare.piece == null)   //if the square is empty
                    {
                        tempString = "*";
                    }
                    else if (tempSquare.piece.color == 1)   //if piece is black
                    {
                        if (tempSquare.piece.isKing)    //if it is a black king
                        {
                            tempString = "B";
                        }
                        else
                        {
                            tempString = "b";
                        }
                    }
                    else if (tempSquare.piece.color == -1)   //if piece is white
                    {
                        if (tempSquare.piece.isKing)    //if it is a white king
                        {
                            tempString = "W";
                        }
                        else
                        {
                            tempString = "w";
                        }
                    }

                    retArray[i, j] = tempString;
                }
            }

            return retArray;
        }

        public void PrintBoard(String[,] board)
        {
            Console.WriteLine("------------------------------------------");
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
