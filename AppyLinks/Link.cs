using System;

namespace AppyLinks
{
	public class Link
	{
		public string Title { get; set; }
		public Uri ViewUrl { get; set; }
		public string ViewUrlText {
			get {
				return ViewUrl.ToString ();
			}
		}
		public Link ()
		{
		}
	}
}

