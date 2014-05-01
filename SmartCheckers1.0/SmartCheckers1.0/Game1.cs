using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SmartCheckers1._0
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        bool fullScreen;
        MouseState mouse;
        Vector2 mousePoint;

        //Textures
        Texture2D backgroundTexture;
        Texture2D blackTexture;
        Texture2D blackClickTexture;
        Texture2D whiteTexture;
        Texture2D whiteClickTexture;
        Texture2D crownTexture;
        Texture2D cursorTexture;
        Texture2D winTexture;
        Texture2D lostTexture;

        SpriteFont scoreFont;
        SpriteFont scoreFont2;

        Square[,] squares;
        List<Piece> whitePieces;
        List<Piece> blackPieces;
        int difficulty;

        Piece clickedPiece;
        int humanColor;
        int currentColor;
        bool moveDone;
        bool catchDone;
        int totalMoves; //for research purposes
        
        Utility ut;
        Ai brain;
        bool gameOver;
        int gameWinner;

        public Game1(bool full, int human, int difficulty)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.fullScreen = full;
            this.humanColor = human;
            this.currentColor = 1;
            this.moveDone = false;
            this.catchDone = false;

            this.gameOver = false;
            this.gameWinner = 0;
            this.totalMoves = 0;
            this.difficulty = difficulty;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = this.fullScreen;
            graphics.ApplyChanges();
            Window.Title = "Smart Checkers 1.0";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            SetUpBoard();
            base.Initialize();
        }

        private void SetUpBoard()
        {
            //set squares
            squares = new Square[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int clr = 0;

                    if ((i - j) % 2 != 0)
                    {
                        clr = 1;
                    }

                    squares[i,j] = new Square(i, j, clr);
                }
                
            }

            //set pieces
            blackPieces = new List<Piece>();
            whitePieces = new List<Piece>();
                        
            ////////////////////// NORMAL LAYOUT //////////////////////////////////////////
            blackPieces.Add(new Piece(squares[0, 7], 1));
            blackPieces.Add(new Piece(squares[2, 7], 1));
            blackPieces.Add(new Piece(squares[4, 7], 1));
            blackPieces.Add(new Piece(squares[6, 7], 1));
            blackPieces.Add(new Piece(squares[1, 6], 1));
            blackPieces.Add(new Piece(squares[3, 6], 1));
            blackPieces.Add(new Piece(squares[5, 6], 1));
            blackPieces.Add(new Piece(squares[7, 6], 1));
            blackPieces.Add(new Piece(squares[0, 5], 1));
            blackPieces.Add(new Piece(squares[2, 5], 1));
            blackPieces.Add(new Piece(squares[4, 5], 1));
            blackPieces.Add(new Piece(squares[6, 5], 1));

            whitePieces.Add(new Piece(squares[1, 0], -1));
            whitePieces.Add(new Piece(squares[3, 0], -1));
            whitePieces.Add(new Piece(squares[5, 0], -1));
            whitePieces.Add(new Piece(squares[7, 0], -1));
            whitePieces.Add(new Piece(squares[0, 1], -1));
            whitePieces.Add(new Piece(squares[2, 1], -1));
            whitePieces.Add(new Piece(squares[4, 1], -1));
            whitePieces.Add(new Piece(squares[6, 1], -1));
            whitePieces.Add(new Piece(squares[1, 2], -1));
            whitePieces.Add(new Piece(squares[3, 2], -1));
            whitePieces.Add(new Piece(squares[5, 2], -1));
            whitePieces.Add(new Piece(squares[7, 2], -1));
            ////////////////////// NORMAL LAYOUT //////////////////////////////////////////

            clickedPiece = null;
            ut = new Utility();
            brain = new Ai(this.difficulty);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            // TODO: use this.Content to load your game content here
            backgroundTexture = Content.Load<Texture2D>("back");
            blackTexture = Content.Load<Texture2D>("black");
            blackClickTexture = Content.Load<Texture2D>("black_click");
            whiteTexture = Content.Load<Texture2D>("white");
            whiteClickTexture = Content.Load<Texture2D>("white_click");
            crownTexture = Content.Load<Texture2D>("crown");
            cursorTexture = Content.Load<Texture2D>("cursor");
            winTexture = Content.Load<Texture2D>("win");
            lostTexture = Content.Load<Texture2D>("loss");

            scoreFont = Content.Load<SpriteFont>("ScoreFont");
            scoreFont2 = Content.Load<SpriteFont>("ScoreFont2");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            MouseHandler();
            if (!gameOver)
            {                
                CheckKings();
                int n = CheckWin();
                if (n != 0)
                {
                    GameOver(n);
                }

                PlayComputer();
            }
            base.Update(gameTime);
        }

        private void PlayComputer()
        {
            if (currentColor != humanColor)
            {
                State bestState = brain.GetBestMove(squares, currentColor);
                if (bestState == null)
                {
                    GameOver(-currentColor);
                    return;
                }
                else
                {
                    ExecuteMoves(bestState.moves);
                }
            }
        }

        private void ExecuteMoves(List<Move> moves)
        {                       
            foreach (Move move in moves)
            {
                Vector2 s = move.from;
                Vector2 d = move.to;

                if ((Math.Abs(s.X - d.X) == 1) && (Math.Abs(s.Y - d.Y) == 1))
                {
                    clickedPiece = squares[(int)s.X, (int)s.Y].piece;
                    squares[(int)s.X, (int)s.Y].piece.sqr = squares[(int)d.X, (int)d.Y];
                    squares[(int)d.X, (int)d.Y].piece = squares[(int)s.X, (int)s.Y].piece;
                    squares[(int)s.X, (int)s.Y].piece = null;
                }
                else
                {
                    clickedPiece = squares[(int)s.X, (int)s.Y].piece;
                    squares[(int)s.X, (int)s.Y].piece.sqr = squares[(int)d.X, (int)d.Y];
                    squares[(int)d.X, (int)d.Y].piece = squares[(int)s.X, (int)s.Y].piece;
                    squares[(int)s.X, (int)s.Y].piece = null;

                    RemovePiece(squares[(int)(d.X + s.X) / 2, (int)(d.Y + s.Y) / 2].piece);
                    squares[(int)(d.X + s.X) / 2, (int)(d.Y + s.Y) / 2].piece = null;
                }
            }

            SwitchPlayer();
        }

        private void waitUp()
        {
            System.Threading.Thread.Sleep(1000);
        }

        private void SwitchPlayer()
        {
            moveDone = false;
            catchDone = false;
            currentColor = -currentColor;
            clickedPiece = null;

            totalMoves++;
        }

        private void MouseHandler()
        {
            mouse = Mouse.GetState();
            mousePoint = new Vector2(mouse.X, mouse.Y);

            if ((mousePoint.X < 20) || (mousePoint.X > 580) || (mousePoint.Y < 20) || (mousePoint.Y > 580) || gameOver)
            {
                IsMouseVisible = true;
                return;
            }
                
            IsMouseVisible = false;

            if (mouse.LeftButton == ButtonState.Pressed)
            {                
                Square currentSquare = GetSquare(mousePoint);
                if (currentSquare.color == 1)
                {
                    HandleClick(currentSquare);
                }                
            }            
        }

        private void HandleClick(Square currentSquare)
        {
            if (currentColor != humanColor)
            {
                return;
            }

            if ((currentSquare.piece != null) && (currentSquare.piece.color == humanColor) && !moveDone)
            {
                clickedPiece = currentSquare.piece;
            }
            else if ((currentSquare.piece == null) && (clickedPiece != null) && (clickedPiece.color == humanColor))
            {
                bool move = TryMove(clickedPiece, currentSquare);
                if (move)
                {
                    SwitchPlayer();
                }
                else
                {
                    bool ct = TryCatch(clickedPiece, currentSquare);
                    if (ct && !CatchesAvailable(clickedPiece))
                    {
                        SwitchPlayer();
                    }
                }
            }
        }

        private Square GetSquare(Vector2 position)  //returns the square at a given coordinate
        {
            int x = (int)(position.X - 20) / 70;
            int y = (int)(position.Y - 20) / 70;

            return squares[x,y];
        }

        private Vector2 GetScreenCoordinate(int x, int y)
        {
            return (new Vector2(20 + 35 + (70 * x), 20 + 35 + (70 * y)));
        }

        private bool TryMove(Piece p, Square dest)  //try to move a piece
        {
            if (catchDone)
            {
                return false;
            }

            if(!p.isKing)   //if clicked piece is a normal piece
            {
                if (((dest.y + humanColor) == p.sqr.y) && (Math.Abs(dest.x - p.sqr.x) == 1))
                {
                    p.sqr.piece = null;
                    p.sqr = dest;
                    dest.piece = p;
                    moveDone = true;
                    return true;
                }                
            }
            else            //if clicked piece is a king
            {
                if ((Math.Abs(dest.y - p.sqr.y) == 1) && (Math.Abs(dest.x - p.sqr.x) == 1))
                {
                    p.sqr.piece = null;
                    p.sqr = dest;
                    dest.piece = p;
                    moveDone = true;
                    return true;
                }   
            }

            return false;
        }

        private bool TryCatch(Piece p, Square dest)
        {
            if (!p.isKing)   //if clicked piece is a normal piece
            {
                if (((dest.y + (2 * humanColor)) == p.sqr.y) && (Math.Abs(dest.x - p.sqr.x) == 2) && (squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece != null) && (squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece.color == -humanColor))
                {
                    RemovePiece(squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece);
                    squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece = null;                    
                    p.sqr.piece = null;
                    p.sqr = dest;
                    dest.piece = p;
                    catchDone = true;
                    moveDone = true;
                    return true;
                }
            }
            else            //if clicked piece is a king
            {
                if ((Math.Abs(dest.y - p.sqr.y) == 2) && (Math.Abs(dest.x - p.sqr.x) == 2) && (squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece != null) && (squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece.color == -humanColor))
                {
                    RemovePiece(squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece);
                    squares[(dest.x + p.sqr.x) / 2, (dest.y + p.sqr.y) / 2].piece = null;
                    p.sqr.piece = null;
                    p.sqr = dest;
                    dest.piece = p;
                    catchDone = true;
                    moveDone = true;
                    return true;
                }
            }

            return false;
        }

        private bool CatchesAvailable(Piece p)
        {
            

            if (((p.color == -1) || (p.isKing)) && (p.sqr.y <6))
            {
                
                    if ((p.sqr.x > 1) && (squares[p.sqr.x - 1, p.sqr.y + 1].piece != null) && (squares[p.sqr.x - 1, p.sqr.y + 1].piece.color == -p.color) && (squares[p.sqr.x - 2, p.sqr.y + 2].piece == null))
                    {
                        return true;
                    }
                    if ((p.sqr.x < 6) && (squares[p.sqr.x + 1, p.sqr.y + 1].piece != null) && (squares[p.sqr.x + 1, p.sqr.y + 1].piece.color == -p.color) && (squares[p.sqr.x + 2, p.sqr.y + 2].piece == null))
                    {
                        return true;
                    }
                
            }
            if (((p.color == 1) || (p.isKing)) && (p.sqr.y > 1))
            {
                
                    if ((p.sqr.x > 1) && (squares[p.sqr.x - 1, p.sqr.y - 1].piece != null) && (squares[p.sqr.x - 1, p.sqr.y - 1].piece.color == -p.color) && (squares[p.sqr.x - 2, p.sqr.y - 2].piece == null))
                    {
                        return true;
                    }
                    if ((p.sqr.x < 6) && (squares[p.sqr.x + 1, p.sqr.y - 1].piece != null) && (squares[p.sqr.x + 1, p.sqr.y - 1].piece.color == -p.color) && (squares[p.sqr.x + 2, p.sqr.y - 2].piece == null))
                    {
                        return true;
                    }
                
            }            

            return false;
        }

        private void RemovePiece(Piece p)
        {
            if (p.color == -1)
            {
                whitePieces.Remove(p);
            }
            else
            {
                blackPieces.Remove(p);
            }
        }

        private void CheckKings()
        {
            foreach (Piece p in blackPieces)
            {
                if (p.sqr.y == 0)
                {
                    p.isKing = true;
                }
            }
            foreach (Piece p in whitePieces)
            {
                if (p.sqr.y == 7)
                {
                    p.isKing = true;
                }
            }
        }

        private int CheckWin()
        {
            int pLeft = 0;
            if(currentColor == 1)
            {
                pLeft = blackPieces.Count;    
            }
            else{
                pLeft = whitePieces.Count;
            }

            String[,] b = ut.ConvertBoard(squares);
            List<State> st = ut.GetPossibleStates(b, currentColor);

            if ((pLeft == 0) || (st.Count == 0))
            {
                return -currentColor;
            }

            else
                return 0;
        }

        private void GameOver(int winner)
        {
            gameOver = true;
            gameWinner = winner;
            Console.WriteLine("Total Moves done by both parties: "+totalMoves);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            DrawBackground();
            DrawPieces();
            DrawScores();
            DrawCursor();
            if (gameOver)
            {
                DrawFinal();
            }            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBackground()  //draw background texture
        {
            Rectangle screenRectangle = new Rectangle(0, 0, 1000, 600);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }

        private void DrawPieces()
        {
            foreach (Piece p in blackPieces)
            {
                if (p == clickedPiece)
                {
                    spriteBatch.Draw(blackClickTexture, GetScreenCoordinate(p.sqr.x, p.sqr.y), null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(blackTexture, GetScreenCoordinate(p.sqr.x, p.sqr.y), null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 0);
                }
                
                if (p.isKing)
                {
                    spriteBatch.Draw(crownTexture, GetScreenCoordinate(p.sqr.x, p.sqr.y), null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 0);
                }                
            }
            foreach (Piece p in whitePieces)
            {
                if (p == clickedPiece)
                {
                    spriteBatch.Draw(whiteClickTexture, GetScreenCoordinate(p.sqr.x, p.sqr.y), null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(whiteTexture, GetScreenCoordinate(p.sqr.x, p.sqr.y), null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 0);
                }
                
                if (p.isKing)
                {
                    spriteBatch.Draw(crownTexture, GetScreenCoordinate(p.sqr.x, p.sqr.y), null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 0);
                }  
            }
        }

        private void DrawScores()
        {
            String black = "You", white = "Computer";
            if (humanColor == -1)
            {
                black = "Computer";
                white = "You";
            }

            spriteBatch.DrawString(scoreFont, "Pieces Left", new Vector2(620, 225), Color.White);
            spriteBatch.DrawString(scoreFont2, black, new Vector2(650, 290), Color.BlanchedAlmond);
            spriteBatch.Draw(blackTexture, new Vector2(800, 280), Color.White);
            
            spriteBatch.DrawString(scoreFont2, white, new Vector2(650, 370), Color.BlanchedAlmond);
            spriteBatch.Draw(whiteTexture, new Vector2(800, 360), Color.White);
                        
            spriteBatch.DrawString(scoreFont, blackPieces.Count.ToString(), new Vector2(900, 290), Color.Chartreuse);
            spriteBatch.DrawString(scoreFont, whitePieces.Count.ToString(), new Vector2(900, 370), Color.Chartreuse);
        }

        private void DrawCursor()
        {
            if (!IsMouseVisible)
            {
                spriteBatch.Draw(cursorTexture, mousePoint, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
        }

        private void DrawFinal()
        {
            if (gameWinner == humanColor)
            {
                spriteBatch.Draw(winTexture, new Vector2(100, 180), Color.White);
            }
            else
            {
                spriteBatch.Draw(lostTexture, new Vector2(100, 180), Color.White);
            }
        }

    }
}
