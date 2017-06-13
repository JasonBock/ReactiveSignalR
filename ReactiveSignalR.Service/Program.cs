using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using ReactiveSignalR.Messages;
using System;
using System.Net.Http;
using System.Text;

namespace ReactiveSignalR.Service
{
	class Program
	{
		private const string HostUrl = "http://localhost:2112/";

		static void Main(string[] args)
		{
			Console.Out.WriteLine($"Starting {Program.HostUrl}...");

			using (WebApp.Start<Startup>(Program.HostUrl))
			{
				var message = JsonConvert.SerializeObject(
					new UserKeysMessage("a", new [] { 'a' }), Formatting.Indented);
				var content = new StringContent(message,
					Encoding.Unicode, "application/json");
				var response = new HttpClient().PostAsync(
					"http://localhost:2112/api/keys", content).Result;
				Console.WriteLine(response);
				Console.WriteLine(response.Content.ReadAsStringAsync().Result);

				Console.Out.WriteLine(
					$"Service running at {Program.HostUrl} - press return to terminate the service...");
				Console.In.ReadLine();
			}
		}
	}
}
