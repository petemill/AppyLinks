using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;


namespace AppyLinks.Android
{
	[Activity (Label = "AppyLinks.Android.Android", MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			//initialise our settings store with the app context
			UserSettingsStore.Init (this.ApplicationContext);

			Xamarin.Forms.Forms.Init (this, bundle);
			SetPage (App.GetMainPage ());
		}
	}
}

