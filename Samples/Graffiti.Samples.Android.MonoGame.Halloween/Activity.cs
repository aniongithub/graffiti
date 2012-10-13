using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.Xna.Framework;

using AndroidContent = Android.Content;

namespace Graffiti.Samples.Android.MonoGame.Halloween
{
	[Activity (Label = "Graffiti.Samples.Android.MonoGame.Halloween", 
	           MainLauncher = true,
	           Icon = "@drawable/icon",
	           Theme = "@style/Theme.Splash",
	           LaunchMode=AndroidContent.PM.LaunchMode.SingleInstance,
	           ConfigurationChanges = AndroidContent.PM.ConfigChanges.Orientation | 
			AndroidContent.PM.ConfigChanges.KeyboardHidden | 
			AndroidContent.PM.ConfigChanges.Keyboard)]
	public class Activity : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create our OpenGL view, and display it
			Graffiti.Samples.Halloween.Game.Activity = this;
			var g = new Graffiti.Samples.Halloween.Game ();
			SetContentView (g.Window);
			g.Run ();
		}
		
	}
}


