using System;
using Graffiti.Samples.BitmapFont;

namespace Graffiti.Samples.MonoGame.BitmapFont
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static Game game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            game = new Game();
            game.Run();
        }
    }
}
