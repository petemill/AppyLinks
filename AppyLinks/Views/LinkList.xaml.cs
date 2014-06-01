using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace AppyLinks
{	
	public partial class LinkList : ContentPage
	{	
		public LinkList ()
		{
			InitializeComponent ();

			//TODO: have a refresh button
			CheckAuthAndRefresh ();
		}


		async void CheckAuthAndRefresh()
		{
			//this is how we refresh the list items, once we know we're authenticated
			Action refreshList = async () => {
				//fetch gist
				listFetchingActivity.IsRunning = true;
				listFetchingActivity.IsVisible = true;
				try
				{
					var links = await UserLinksDataStore.GetLinks();
					urlView.ItemsSource = links;

				}
				catch (KeyNotFoundException ex) {
					DisplayAlert ("You haven't made any links yet", "Please make sure you create a GitHub gist called 'AppyLinks' and create links in the format Title [New Line] Url [New Line]. Leave an empty line between links.", "I'll go do that!", null);
				}
				catch (Exception ex) {
					DisplayAlert ("Error", "Unkown error. Message: " + ex.Message, "OK", null);
				}

				listFetchingActivity.IsRunning = false;
				listFetchingActivity.IsVisible = false;
			};

			//if not authenticated, display authentication overlay first
			var isAuthenticated = await UserLinksDataStore.IsAuthenticated ();
			if (!isAuthenticated) {
				Navigation.PushModalAsync (new AuthorizationView (() => {
					Navigation.PopModalAsync ();
					refreshList();
				}));
			} else {
				refreshList();
			}
		}



		public async void ItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var selectedLink = e.SelectedItem as Link;

			await Navigation.PushAsync (new ContentPage {
				Title = selectedLink.Title,
				Content = new ContentView {
					Content = new WebView (){ Source = new UrlWebViewSource (){ Url = selectedLink.ViewUrl.AbsoluteUri } }
				}
			});
		}
	}

}

