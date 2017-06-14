using Microsoft.AspNet.SignalR;
using ReactiveSignalR.Contracts;

namespace ReactiveSignalR.Service
{
	public sealed class KeyWatcherHub
		: Hub<IKeyWatcherHub>
	{ }
}