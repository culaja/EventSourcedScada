using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Common;
using Common.Messaging;

namespace CommonAdapters.AutofacMessageBus
{
	public sealed class AutofacLocalMessageBus : IDomainEventBus, ICommandBus, IDisposable
	{
		private readonly BlockingCollection<MessageContext> _messagesContextBlockingCollection = new BlockingCollection<MessageContext>();
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

		public IMessage Dispatch(IMessage message) => DispatchInternal(message).MessageToHandle;

		public Task<Result> ExecuteAsync(IMessage message) => DispatchInternal(message).WaitToBeHandledAsync();

		private MessageContext DispatchInternal(IMessage message)
		{
			var messageContext = new MessageContext(message);
			_messagesContextBlockingCollection.Add(messageContext);
			return messageContext;
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
				isCancellationNotRequested = TakeMessageContextFromBlockingCollectionOrNoneIfCanceled()
					.Map(messageContext => messageContext
						.FinalizeWith(DispatchMessageToAllRegisteredHandlers(messageContext.MessageToHandle)))
					.HasValue;

			} while (isCancellationNotRequested);
		}

		private Maybe<MessageContext> TakeMessageContextFromBlockingCollectionOrNoneIfCanceled()
		{
			try
			{
				return Maybe<MessageContext>.From(_messagesContextBlockingCollection.Take(_cancellationTokenSource.Token));
			}
			catch (OperationCanceledException)
			{
				return Maybe<MessageContext>.None;
			}
		}

		private Result DispatchMessageToAllRegisteredHandlers(IMessage message) => Result.Combine(
			_messageResolver
				.GetMessageHandlersFor(message)
				.Select(handler => DispatchTo(message, handler))
				.ToArray());

		private Result DispatchTo(IMessage message, IMessageHandler messageHandler) => messageHandler.Handle(message);
	}
}