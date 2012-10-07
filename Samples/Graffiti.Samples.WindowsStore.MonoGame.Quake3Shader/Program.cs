using System;
using MonoGame.Framework;
using Graffiti.Samples.Quake3Shader;

namespace Graffiti.Samples.WindowsStore.MonoGame.Quake3Shader
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
