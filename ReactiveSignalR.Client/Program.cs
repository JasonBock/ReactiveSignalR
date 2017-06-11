﻿using KeyWatching;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Forms;
using K = KeyWatching;

namespace ReactiveSignalR.Client
{
	class Program
	{
		private const string Termination = "STOP IT";

		private static Queue<char> buffer = new Queue<char>();

#pragma warning disable IDE0022 // Use expression body for methods
		static void Main(string[] args)
		{
			//Program.HandleKeysViaEvents();
			//Program.HandleKeysViaManualObservable();
			Program.HandleKeysFromEventPattern();
		}
#pragma warning restore IDE0022 // Use expression body for methods

		private static void HandleKeysViaEvents()
		{
			using (var keyLogger = new EventedNativeKeyWatcher())
			{
				keyLogger.KeyLogged += (s, e) =>
				{
					Console.Write(e.Key);
					Program.CheckForTermination(e.Key);
				};
				Application.Run();
			}
		}

		private static void HandleKeysViaManualObservable()
		{
			using (var keyLogger = new ObservableNativeKeyWatcher())
			{
				keyLogger.Subscribe(new ObservingNativeKeyWatcher("Observer 1"));
				keyLogger.Subscribe(new ObservingNativeKeyWatcher("Observer 2"));
				Application.Run();
			}
		}

		private static void HandleKeysFromEventPattern()
		{
			using (var keyLogger = new EventedNativeKeyWatcher())
			{
				var observable = Observable.FromEventPattern<K.KeyEventArgs>(
					keyLogger, nameof(EventedNativeKeyWatcher.KeyLogged));
				using (var subscription = observable.Subscribe(
					pattern =>
					{
						var key = pattern.EventArgs.Key;
						Console.Out.Write(key);
						Program.CheckForTermination(key);
					}))
				{
					Application.Run();
				}
			}		}

		private static void CheckForTermination(char key)
		{
			Program.buffer.Enqueue(key);

			while (Program.buffer.Count > Program.Termination.Length)
			{
				Program.buffer.Dequeue();
			}

			if (Program.buffer.Count == Program.Termination.Length)
			{
				var termination = new string(Program.buffer.ToArray());

				if (termination == Program.Termination)
				{
					Application.Exit();
				}
			}
		}
	}
}