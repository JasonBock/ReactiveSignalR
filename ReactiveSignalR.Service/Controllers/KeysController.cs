using ReactiveSignalR.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ReactiveSignalR.Service.Controllers
{
	public sealed class KeysController 
		: ApiController
	{
		private static readonly string[] BadWords = { "cotton", "headed", "ninny", "muggins" };

		private readonly KeyWatcherHub hub;

		public KeysController(KeyWatcherHub hub) =>
			this.hub = hub ?? throw new ArgumentNullException(nameof(hub));

		public void Post([FromBody] UserKeysMessage value)
		{
			var keys = new string(value.Keys.ToArray()).ToLower();

			var foundBadWords = new List<string>();

			foreach (var word in KeysController.BadWords)
			{
				if (keys.Contains(word))
				{
					foundBadWords.Add(word);
				}
			}

			if (foundBadWords.Count > 0)
			{
				var badWords = string.Join(", ", foundBadWords);
				Console.Out.WriteLine("HEY!");
				this.hub.Clients.All.NotificationSent(
					new NotificationSentMessage(value.Name, badWords));
			}
		}
	}
}
