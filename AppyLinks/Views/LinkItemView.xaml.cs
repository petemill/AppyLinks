using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppyLinks
{	
	public partial class LinkItemView : ContentPage
	{	
		public Link LinkItem { get; set; }

		public LinkItemView (Link itemToDisplay)
		{
			LinkItem = itemToDisplay;
			InitializeComponent ();
			this.BindingContext = LinkItem;

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			//if (LinkBrowser!= null)
			//	LinkBrowser.BindingContext = LinkItem;
		}
	}
}

