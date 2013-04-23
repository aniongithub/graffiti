using System;

namespace Graffiti.Samples.WP7.XNA.Halloween
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game game = new Graffiti.Samples.Halloween.Game())
            {
                game.Run();
            }
        }
    }
#endif
}

