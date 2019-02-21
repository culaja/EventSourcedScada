using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using Common;
using Common.Messaging;

namespace AutofacMessageBus
{
    public sealed class AutofacLocalMessageBus : ILocalMessageBus, IDisposable
	{
		private readonly BlockingCollection<IMessage> _messagesBlockingCollection = new BlockingCollection<IMessage>();
		private readonly AutofacMessageResolver _messageResolver;
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly Thread _workerThread;

		public AutofacLocalMessageBus(
			IComponentContext componentContext)
		{
			_messageResolver = new AutofacMessageResolver(componentContext);
			_workerThread = new Thread(Worker);
			_workerThread.Start();
		}

		public IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages) => messages
			.Select(Dispatch)
			.ToList();

		public IMessage Dispatch(IMessage message)
		{
			_messagesBlockingCollection.Add(message);
			return message;
		}

		public void Dispose()
		{
			_cancellationTokenSource.Cancel();
			_workerThread.Join();
		}

		private void Worker()
		{
			bool isCancellationNotRequested;
			do
			{
				isCancellationNotRequested = TakeMessageFromBlockingCollectionOrNoneIfCanceled()
					.Map(DispatchMessageToAllRegisteredHandlers)
					.HasValue;

			} while (isCancellationNotRequested);
		}

		private Maybe<IMessage> TakeMessageFromBlockingCollectionOrNoneIfCanceled()
		{
			try
			{
				return Maybe<IMessage>.From(_messagesBlockingCollection.Take(_cancellationTokenSource.Token));
			}
			catch (OperationCanceledException)
			{
				return Maybe<IMessage>.None;
			}
		}

		private IMessage DispatchMessageToAllRegisteredHandlers(IMessage message)
		{
			_messageResolver
				.GetMessageHandlersFor(message)
				.Select(handler => DispatchTo(message, handler))
				.ToList();
			return message;
		}

		private IMessage DispatchTo(IMessage message, IMessageHandler messageHandler)
		{
			messageHandler.Handle(message)
				.OnFailure(e => Console.WriteLine(e));
			return message;
		}
	}
}