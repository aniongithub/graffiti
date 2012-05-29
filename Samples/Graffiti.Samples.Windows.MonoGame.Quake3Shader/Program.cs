using System;
using Graffiti.Samples.Quake3Shader;

namespace Graffiti.Samples.Windows.MonoGame.Quake3Shader
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
