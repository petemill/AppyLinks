using System;
using Xamarin.Forms;

namespace AppyLinks
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			//whilst testing reset authentication token every time we start up
			DependencyService.Get<IUserSettingsStore> ().GithubAuthorizationToken = "";
			
			return new NavigationPage(new LinkList ())
			{

			};
		}
	}

	public static partial class Secrets
	{
		//this is defined again in a separate file with a partial class
		public static string GithubApiSecret()
		{
			return "REPLACEWITHYOURSECRETKEY";
		}
	}
}

