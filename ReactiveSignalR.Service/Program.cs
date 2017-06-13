using Microsoft.Owin.Hosting;
using System;

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
				Console.Out.WriteLine(
					$"Service running at {Program.HostUrl} - press return to terminate the service...");
				Console.In.ReadLine();
			}
		}
	}
}
