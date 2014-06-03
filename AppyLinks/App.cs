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

			return new AppyLinksNavigationPage(new LinkList ())
			{

			};
		}
	}

	public class AppyLinksNavigationPage : NavigationPage {
		public AppyLinksNavigationPage(Page root) : base(root) { }
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

