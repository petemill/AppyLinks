using System;
using MonoTouch.Foundation;
using Xamarin.Forms;
using AppyLinks.iOS;

[assembly: Dependency(typeof (UserSettingsStore))]
namespace AppyLinks.iOS
{
	public class UserSettingsStore : IUserSettingsStore
	{
		const string SETTINGSKEY_GITHUBAUTHTOKEN = "GithubAuthToken";

		#region IUserSettingsStore implementation

		public string GithubAuthorizationToken {
			get {
				return NSUserDefaults.StandardUserDefaults.StringForKey (SETTINGSKEY_GITHUBAUTHTOKEN);
			}
			set {
				NSUserDefaults.StandardUserDefaults.SetString (value, SETTINGSKEY_GITHUBAUTHTOKEN);
			}
		}

		#endregion

		public UserSettingsStore ()
		{
		}
	}
}

