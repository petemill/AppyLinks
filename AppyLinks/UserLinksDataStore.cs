using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using Octokit;
using System.Diagnostics;

namespace AppyLinks
{
	public class UserLinksDataStore
	{
		public static async Task<bool> Authorize(string username, string password)
		{
			var githubClient = new Octokit.GitHubClient (new ProductHeaderValue ("AppyLinks"));
			githubClient.Credentials = new Octokit.Credentials (username, password);

			var authenticationResult = await githubClient.Authorization.GetOrCreateApplicationAuthentication(
				"b205f79f35a8b9a48674", 
				Secret.GithubApiSecret(),
				new Octokit.NewAuthorization() { Note = "AppyLinksInitial", Scopes = new[] { "gist" } }
			);

			var authToken = authenticationResult.Token;

			LocalSettings.GithubAuthorizationToken = authToken;

			return true;
		}

		public static async Task<bool> IsAuthenticated()
		{
			return !String.IsNullOrEmpty (LocalSettings.GithubAuthorizationToken);
		}

		public static async Task<IEnumerable<Link>> GetLinks()
		{
			var githubClient = new Octokit.GitHubClient (new ProductHeaderValue ("AppyLinks"));
			githubClient.Credentials = new Octokit.Credentials (LocalSettings.GithubAuthorizationToken);

			var allGists = await githubClient.Gist.GetAll ();
			var appyLinksGist = allGists.Where (gist => String.Equals (gist.Description, "AppyLinks", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault ();

			if (appyLinksGist == null)
				throw new KeyNotFoundException ("Could not find gist.");

			//parse gist for links
			//TODO: support multiple files, and split by file so user can group
			var appyLinksGistFull = await githubClient.Gist.Get (appyLinksGist.Id);
			var content = appyLinksGistFull.Files.Select (file => file.Value.Content).FirstOrDefault();

			if (content == null)
				throw new KeyNotFoundException ("Could not find any files in gist.");

			//parse
			var rawLinks = content.Split (new[] {Environment.NewLine + Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
			Debug.WriteLine ("Found " + rawLinks.Length + " raw links.");
			var links = rawLinks.Select (rawLink => rawLink.Split (new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
				.Select (StringPairToLink).Where(item => item != null);
			Debug.WriteLine ("Parsed to " + links.Count () + " links.");

			return links;
		}

		static Link StringPairToLink(string[] descriptionWithUrlPair)
		{
			Uri destinationAddress;
			try
			{
				destinationAddress = new Uri (descriptionWithUrlPair [1]);
			}
			catch (Exception ex) {
				//TODO: let caller know why we couldn't parse the link, and report back to the user which link failed
				return null;
			}
			return new Link { Title = descriptionWithUrlPair [0], ViewUrl = destinationAddress };
		}


		//helper for accessing device's implementation of local key/value data store
		static IUserSettingsStore LocalSettings
		{
			get {
				return DependencyService.Get<IUserSettingsStore> ();
			}

		}
	}
}

