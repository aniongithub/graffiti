using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using AndroidContent = Android.Content;

using Microsoft.Xna.Framework;

namespace Graffiti.Samples.Android.MonoGame.Quake3Shader
{
	[Activity (Label = "Graffiti.Samples.Android.MonoGame.Quake3Shader", 
	           MainLauncher = true,
	           Icon = "@drawable/icon",
	           Theme = "@style/Theme.Splash",
	           LaunchMode=AndroidContent.PM.LaunchMode.SingleInstance,
	           ConfigurationChanges = AndroidContent.PM.ConfigChanges.Orientation | 
			AndroidContent.PM.ConfigChanges.KeyboardHidden | 
			AndroidContent.PM.ConfigChanges.Keyboard)]
	public class Activity1 : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create our OpenGL view, and display it
			Game.Activity = this;
			var g = new Game ();
			SetContentView (g.Window);
			g.Run ();
		}
		
	}
}


