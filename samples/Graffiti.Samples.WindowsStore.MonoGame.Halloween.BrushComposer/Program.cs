using MonoGame.Framework;
using System;

namespace Graffiti.Samples.WindowsStore.MonoGame.Halloween.BrushComposer
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var factory = new GameFrameworkViewSource<Game>();
            Windows.ApplicationModel.Core.CoreApplication.Run(factory);
        }
    }
}
