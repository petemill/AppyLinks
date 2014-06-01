using System;

namespace AppyLinks
{
	public interface IUserSettingsStore
	{
		string GithubAuthorizationToken { get; set; }
	}

}

