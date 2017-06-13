using ReactiveSignalR.Messages;

namespace ReactiveSignalR.Service
{
	public interface IKeyWatcherHub
	{
		void NotificationSent(NotificationSentMessage message);
	}
}
