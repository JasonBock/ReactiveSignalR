namespace ReactiveSignalR.Messages
{
	public sealed class NotificationSentMessage
	{
		public NotificationSentMessage(string user, string badWords)
		{
			this.User = user;
			this.BadWords = badWords;
		}

		public string User { get; }
		public string BadWords { get; }
	}
}
