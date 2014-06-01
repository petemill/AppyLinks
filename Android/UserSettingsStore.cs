using System;
using Android.Preferences;
using Android.Content;
using Xamarin.Forms;
using AppyLinks.Android;

[assembly: Dependency(typeof (UserSettingsStore))]
namespace AppyLinks.Android
{
	public class UserSettingsStore : IUserSettingsStore
	{
		//static control
		static AndroidSettingsStore preferencesInstance = null;
		public static void Init(Context settingsContext)
		{
			//initialise preference store
			preferencesInstance = new AndroidSettingsStore(
				PreferenceManager.GetDefaultSharedPreferences (settingsContext)
			);
		}

		const string SETTINGSKEY_GITHUBAUTHTOKEN = "GithubAuthToken";

		//interface implementation
		public string GithubAuthorizationToken {
			get {
				return preferencesInstance.androidPreferences.GetString (SETTINGSKEY_GITHUBAUTHTOKEN, null);
			}
			set {
				var editor = preferencesInstance.androidPreferences.Edit ();
				editor.PutString (SETTINGSKEY_GITHUBAUTHTOKEN, value);
				editor.Apply ();
			}
		}



		public UserSettingsStore ()
		{
		}
	}

	public class AndroidSettingsStore
	{
		public ISharedPreferences androidPreferences=null;
		public AndroidSettingsStore(ISharedPreferences sharedPreferences)
		{
			androidPreferences = sharedPreferences;
		}
	}
	
				
}

