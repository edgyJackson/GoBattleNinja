using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models
{
    public class PokemonDataClass
    {
		public string Source { get; set; }


		public PokemonDataClass(string endPoint)
		{
			Source = endPoint;
		}




/*
		public GitHubUser GetGitHubUser(string secret, string gitUser)
		{
			string jsonResponse = SendRequest(Source, secret, gitUser);


			JObject geo = JObject.Parse(jsonResponse);

			GitHubUser me = new GitHubUser()
			{
				img = (string)geo["avatar_url"],
				userName = (string)geo["login"],
				name = (string)geo["name"],
				email = (string)geo["email"],
				company = (string)geo["company"],
				location = (string)geo["location"]

			};






			return me;

		}

		public IEnumerable<Repo> GetUserRepos(string secret, string gitUser)
		{
			string jsonResponse = SendRequest(Source, secret, gitUser);


			JArray geo = JArray.Parse(jsonResponse);

			List<Repo> repoList = new List<Repo>();
			for (int i = 0; i < geo.Count; ++i)
			{

				string ReName = (string)geo[i]["name"];
				string Ownr = (string)geo[i]["owner"]["login"];
				string LastUpdatd = (string)geo[i]["updated_at"];
				string link = (string)geo[i]["html_url"];
				string repImg = (string)geo[i]["owner"]["avatar_url"];



				repoList.Add(new Repo { RepoName = ReName, Owner = Ownr, LastUpdated = LastUpdatd, repoLink = link, repoImg = repImg });




			}






			return repoList;

		}







		static public string SendRequest(string uri, string credentials, string username)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.Headers.Add("Authorization", "token " + credentials);
			request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
			request.Accept = "application/json";

			string jsonString = null;
			// TODO: You should handle exceptions here
			using (WebResponse response = request.GetResponse())
			{
				Stream stream = response.GetResponseStream();
				StreamReader reader = new StreamReader(stream);
				jsonString = reader.ReadToEnd();
				reader.Close();
				stream.Close();
			}
			return jsonString;
		}*/
	}
}

