//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SmartCheckers1._0
//{
//    class Test
//    {
//        Square[,] squares = new Square[8, 8];
//        List<Piece> blackPieces;
//        List<Piece> whitePieces;
//        Utility ut = new Utility();

//        public Test()
//        {
//            SetUpBoard();
//        }

//        private void SetUpBoard()
//        {
//            //set squares
//            squares = new Square[8, 8];
//            for (int i = 0; i < 8; i++)
//            {
//                for (int j = 0; j < 8; j++)
//                {
//                    int clr = 0;

//                    if ((i - j) % 2 != 0)
//                    {
//                        clr = 1;
//                    }

//                    squares[i, j] = new Square(i, j, clr);
//                }

//                //Console.WriteLine(i + ":::" + x + ":::" + y);
//            }

//            //set pieces
//            blackPieces = new List<Piece>();
//            whitePieces = new List<Piece>();

//            for (int i = 0; i < 3; i++)
//            {
//                blackPieces.Add(new Piece(squares[1 - ((7 - i) % 2), 7 - i], 1));
//                blackPieces.Add(new Piece(squares[3 - ((7 - i) % 2), 7 - i], 1));
//                blackPieces.Add(new Piece(squares[5 - ((7 - i) % 2), 7 - i], 1));
//                blackPieces.Add(new Piece(squares[7 - ((7 - i) % 2), 7 - i], 1));

//                whitePieces.Add(new Piece(squares[1 - (i % 2), i], -1));
//                whitePieces.Add(new Piece(squares[3 - (i % 2), i], -1));
//                whitePieces.Add(new Piece(squares[5 - (i % 2), i], -1));
//                whitePieces.Add(new Piece(squares[7 - (i % 2), i], -1));
//            }

//            //clickedPiece = null;

//            //test purposes only
//            //blackPieces[0].isKing = true;
//            //blackPieces[1].isKing = true;
//            //blackPieces[2].isKing = true;
//            //blackPieces[3].isKing = true;
//            //whitePieces[0].isKing = true;
//            //whitePieces[1].isKing = true;
//            //whitePieces[2].isKing = true;
//            //whitePieces[3].isKing = true;
//        }

//        public void RunTest()
//        {
//            ut.PrintBoard(squares);
//            //List<Square[,]> list = ut.GetCatches(squares, 1, squares[0,5].piece);
//            List<Square[,]> list = ut.GetMoves(squares, 1, squares[2, 5].piece);

//            Console.WriteLine(list.Count);

//            for (int i = 0; i < list.Count; i++)
//            {
//                ut.PrintBoard(list[i]);
//            }
//        }
//    }
//}
