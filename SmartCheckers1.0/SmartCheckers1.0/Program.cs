using System;

namespace SmartCheckers1._0
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            SmartCheckers sm = new SmartCheckers();
            sm.ShowDialog();

            //using (Game1 game = new Game1(false, 1))
            //{
            //    game.Run();
            //}

        }
    }
#endif
}

