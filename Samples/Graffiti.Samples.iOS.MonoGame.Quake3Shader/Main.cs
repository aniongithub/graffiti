
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Graffiti.Samples.iOS.MonoGame.Quake3Shader
{
	[Register("AppDelegate")]
	class Program : UIApplicationDelegate
	{
		Graffiti.Samples.Quake3Shader.Game game;

		public override void FinishedLaunching (UIApplication app)
		{
			// Fun begins..
			game = new Graffiti.Samples.Quake3Shader.Game ();
			game.Run ();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}    
}

