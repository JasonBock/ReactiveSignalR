using ReactiveSignalR.Messages;

namespace ReactiveSignalR.Contracts
{
	public interface IKeyWatcherHub
	{
		void NotificationSent(NotificationSentMessage message);
	}
}
