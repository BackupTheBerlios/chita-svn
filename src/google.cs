using System;
using System.Net;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Google
{
	class GoogleSearch
	{
		static void Main(string[] args)
		{
			Console.Write("Please enter string to search google for: ");
			string searchString = HttpUtility.UrlEncode(Console.ReadLine());
			Console.Write("Please enter the number of search results you want to see: ");
			int numSearch= System.Int32.Parse(Console.ReadLine());
			Console.WriteLine();
			Console.Write("Please wait...\r");

			// Query google.
			WebClient webClient = new WebClient();
			byte[] response =
				webClient.DownloadData("http://www.google.com/search?&num="+numSearch+"&q="
				+ searchString);

			// Check response for results
			string regex = "g><a\\shref=\"?(?<URL>[^\">]*)>(?<Name>[^<]*)";
			MatchCollection matches
				= Regex.Matches(Encoding.ASCII.GetString(response), regex);

			// Output results
			Console.WriteLine("===== Results =====");
			if(matches.Count > 0)
			{
				foreach(Match match in matches)
				{
					Console.WriteLine(HttpUtility.HtmlDecode(
						match.Groups["Name"].Value) + 
						" - " + match.Groups["URL"].Value);
				}
			}
			else
			{
				Console.WriteLine("0 results found");
			}
			Console.ReadLine();
		}
	}
}