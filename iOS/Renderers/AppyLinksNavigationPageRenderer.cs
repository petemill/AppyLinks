using System;
using System.Drawing;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms;
using AppyLinks;
using AppyLinks.iOS;


[assembly: ExportRenderer(typeof(AppyLinksNavigationPage), typeof(AppyLinksNavigationPageRenderer))]
namespace AppyLinks.iOS
{
	public class AppyLinksNavigationPageRenderer : NavigationRenderer
	{
	

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//add the logo, scale down to 40 height (it will work out the rest and spill out the width)
			var imageView = new UIImageView (new RectangleF (0, 0, 150, 30)); //);
			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			imageView.Image = UIImage.FromFile ("Images/appylinkslogo.png");
			this.NavigationBar.TopItem.TitleView = imageView;

		}


	}
}

