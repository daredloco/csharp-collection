using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoWa
{
	namespace Networking
	{
		static class Helpers
		{
			public static readonly HttpClient httpClient = new HttpClient();
			public static readonly WebClient webClient = new WebClient();

			public static string GetQueryString(KeyValuePair<string, string>[] querydata)
			{
				string querystring = "";
				foreach (KeyValuePair<string, string> data in querydata)
				{
					if (string.IsNullOrEmpty(querystring))
						querystring = "?" + data.Key + "=" + data.Value;
					else
						querystring = "&" + data.Key + "=" + data.Value;
				}
				return querystring;
			}

			public static Dictionary<string,string> GetPostQuery(KeyValuePair<string,string>[] querydata)
			{
				Dictionary<string, string> dict = new Dictionary<string, string>();
				foreach(KeyValuePair<string,string> kvp in querydata)
				{
					dict.Add(kvp.Key, kvp.Value);
				}
				return dict;
			}
		}

		public class WebServer
		{
			public string BaseUrl { get; private set; }
			string subDir = "/";
			WebClient client = Helpers.webClient;

			/// <summary>
			/// WebServer connection to the server with the domain 'domain'
			/// </summary>
			/// <param name="domain">The domain of the webserver</param>
			/// <param name="isSSL">Is the connection made with SSL or not?</param>
			public WebServer(string domain, bool isSSL = true)
			{
				try
				{
					if (domain.StartsWith("http://"))
						domain.Replace("http://", "");
					if (domain.StartsWith("https://"))
						domain.Replace("https://","");

					IPHostEntry entry = Dns.GetHostEntry(domain);
					BaseUrl = entry.HostName;
					if (isSSL)
						BaseUrl = "https://" + BaseUrl;
					else
						BaseUrl = "http://" + BaseUrl;
				}catch(Exception ex)
				{
					throw ex;
				}
			}

			/// <summary>
			/// WebServer connection to the server with the IP 'ip'
			/// </summary>
			/// <param name="ip">The IP address of the webserver</param>
			/// <param name="isSSL">Is the connection made with SSL or not?</param>
			public WebServer(IPAddress ip, bool isSSL = true)
			{
				try
				{
					IPHostEntry entry = Dns.GetHostEntry(ip);
					BaseUrl = entry.HostName;
					if (isSSL)
						BaseUrl = "https://" + BaseUrl;
					else
						BaseUrl = "http://" + BaseUrl;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			/// <summary>
			/// Fetches the page content and returns it as a string
			/// </summary>
			/// <param name="page">The page on the webserver</param>
			/// <param name="querydata">The GET data</param>
			/// <returns>A string with the fetched data</returns>
			public string GetString(string page, params KeyValuePair<string, string>[] querydata)
			{
				string querystring = Helpers.GetQueryString(querydata);

				try
				{
					return client.DownloadString(BaseUrl + subDir + page + querystring);
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}

			/// <summary>
			/// Fetches the page content and returns it as a byte array
			/// </summary>
			/// <param name="page">The page on the webserver</param>
			/// <param name="querydata">The GET data</param>
			/// <returns>A byte array with the fetched data</returns>
			public byte[] GetData(string page, params KeyValuePair<string,string>[] querydata)
			{
				string querystring = Helpers.GetQueryString(querydata);

				try
				{
					return client.DownloadData(BaseUrl + subDir + page + querystring);
				}catch(Exception ex)
				{
					throw ex;
				}
			}

			/// <summary>
			/// Downloads a file from the server and saves it to a location
			/// </summary>
			/// <param name="page">The file to download</param>
			/// <param name="destination">The destination of the file</param>
			/// <param name="querydata">The GET data</param>
			public void DownloadFile(string page, string destination, params KeyValuePair<string,string>[] querydata)
			{
				string querystring = Helpers.GetQueryString(querydata);

				try
				{
					client.DownloadFile(BaseUrl + subDir + page + querystring, destination);
				}catch(Exception ex)
				{
					throw ex;
				}
			}

			/// <summary>
			/// Fetches the page content and returns it as a string
			/// </summary>
			/// <param name="page">The page on the webserver</param>
			/// <param name="querydata">The POST data</param>
			/// <returns>A string with the fetched data</returns>
			public string GetPostString(string page, params KeyValuePair<string,string>[] querydata)
			{
				try
				{
					var content = new FormUrlEncodedContent(Helpers.GetPostQuery(querydata));
					return Helpers.httpClient.PostAsync(BaseUrl + subDir + page, content).Result.Content.ReadAsStringAsync().Result;
				}

				catch(Exception ex)
				{
					throw ex;
				}
			}

			/// <summary>
			/// Sets the subdirectories. If empty it will set to root
			/// </summary>
			/// <param name="subs">The sub directories</param>
			public void SetSubs(params string[] subs)
			{
				subDir = "/";
				if (subs.Length < 1)
				{
					return;
				}
				foreach(string sub in subs)
				{
					subDir += sub + "/";
				}
			}

			
		}

		public class Server
		{
			/// <summary>
			/// A Network Server (Not Implemented in this Version!)
			/// </summary>
			public Server()
			{
				throw new NotImplementedException();
			}
		}

		public class Client
		{
			/// <summary>
			/// A Network Client (Not Implemented in this Version!)
			/// </summary>
			public Client()
			{
				throw new NotImplementedException();
			}
		}
	}
}
