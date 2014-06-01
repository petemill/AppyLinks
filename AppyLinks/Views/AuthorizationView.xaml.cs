using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppyLinks
{	
	public partial class AuthorizationView : ContentPage
	{	

		public AuthorizationView (Action authorizationComplete)
		{
			InitializeComponent ();

			//could not get the XAML <FileImageSource /> working correctly, so falling back to this pattern
			Logo.Source = ImageSource.FromFile ("Images/appylinkslogo.png");

			//setup behaviour for authentication via github
			GithubLoginUserAction.Clicked += async (sender, e) => {
				//show that we are waiting for authorization to happen
				AuthenticationInProgressIndicator.IsVisible = true;
				AuthenticationInProgressIndicator.IsRunning = true;
				//prevent user submitting again until we're finished
				GithubLoginUserAction.IsEnabled = false;
				//TODO: username and password inputs and message about how credentials won't be stored
				try {
					await UserLinksDataStore.Authorize (GithubUsername.Text, GithubPassword.Text);
					DisplayAlert ("Github Authentication", "Sucessfully logged in via github. To revoke access please visit your account settings page on GitHub.com", "OK", null);

					//notify our owner of completion and let it decide what to do (probably hide this view)
					if (authorizationComplete != null)
						authorizationComplete ();

				} catch (Exception ex) {
					DisplayAlert ("Error", "There was a problem authenticating with github: " + ex.Message, "Dismiss", null);
					//allow user to try again
					//show that we are no longer waiting for authorization to happen
					AuthenticationInProgressIndicator.IsVisible = false;
					AuthenticationInProgressIndicator.IsRunning = false;
					//allow user to submit again
					GithubLoginUserAction.IsEnabled = true;
				}
			};
			
		}

	
	}
}

