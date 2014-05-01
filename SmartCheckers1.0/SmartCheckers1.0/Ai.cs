using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCheckers1._0
{
    class Ai
    {
        Utility myUti;
        int difficulty;

        public Ai(int difficulty)
        {
            this.myUti = new Utility();
            this.difficulty = difficulty;
        }

        public State GetBestMove(Square[,] currentBoard, int color)
        {
            State retState = null;
            int bestValue = 0;

            String[,] convBoard = myUti.ConvertBoard(currentBoard);
            List<State> possible = myUti.GetPossibleStates(convBoard, color);

            if (color == 1)
            {
                bestValue = int.MinValue;

                foreach (State state in possible)
                {
                    int value = MinMax(state.board, difficulty, int.MinValue, int.MaxValue, 1);
                    if (value > bestValue)
                    {
                        bestValue = value;
                        retState = state;
                    }
                }
            }

            if (color == -1)
            {
                bestValue = int.MaxValue;

                foreach (State state in possible)
                {
                    int value = MinMax(state.board, difficulty, int.MinValue, int.MaxValue, -1);
                    if (value < bestValue)
                    {
                        bestValue = value;
                        retState = state;
                    }
                }
            }

            return retState;
        }

        private double heuristicValue(int myColor, String[,] squares)
        {
            double myValue = 0, enemyValue = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (squares[i, j] != "*")
                    {
                        if ((squares[i, j] == "w") || (squares[i, j] == "b"))
                        {
                            if (((squares[i, j] == "w") && (myColor == -1)) || ((squares[i, j] == "b") && (myColor == 1)))
                            {                           
                                myValue = myValue + 100 + Math.Pow((i + 1), 2);
                            }
                            else
                            {
                                enemyValue = enemyValue + 100 + Math.Pow((i + 1), 2);
                            }
                        }
                        else
                        {
                            if (((squares[i, j] == "W") && (myColor == -1)) || ((squares[i, j] == "B") && (myColor == 1)))
                            {
                                myValue += 200;

                                if (j == 0 || j == 7)
                                {
                                    if (i == 0 || i == 7)
                                    {
                                        myValue -= 40;
                                    }
                                    else
                                    {
                                        myValue -= 20;
                                    }
                                }
                            }
                            else
                            {
                                enemyValue += 200;

                                if (j == 0 || j == 7)
                                {
                                    if (i == 0 || i == 7)
                                    {
                                        enemyValue -= 40;
                                    }
                                    else
                                    {
                                        enemyValue -= 20;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return (myValue - enemyValue)*myColor;
        }

        public int MinMax(String[,] board, int depth, int alpha, int beta, int color)
        {
            if (depth == 0)
            {
                return (int)heuristicValue(color, board);
            }
                        
            List<State> childStates = myUti.GetPossibleStates(board, -color);

            if (childStates.Count == 0)
            {
                return (int)heuristicValue(color, board);
            }

            if (color == 1)
            {
                foreach (State child in childStates)
                {
                    int retAlpha = MinMax(child.board, depth - 1, alpha, beta, -color);

                    if (retAlpha > alpha)
                    {
                        alpha = retAlpha;
                    }

                    if (beta < alpha)
                    {
                        break;
                    }

                }

                return alpha;
            }
            else
            {
                foreach (State child in childStates)
                {
                    int retBeta = MinMax(child.board, depth - 1, alpha, beta, -color);

                    if (retBeta < beta)
                    {
                        beta = retBeta;
                    }

                    if (beta < alpha)
                    {
                        break;
                    }

                }
                
                return beta;
            }
        }
    }
}
