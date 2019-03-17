using System.Collections.Generic;
using System.Linq;
using Autofac;
using Common;
using Common.Messaging;

namespace CommonAdapters.AutofacMessageBus
{
	public sealed class AutofacLocalMessageBus : IDomainEventBus, ICommandBus
	{
		private readonly AutofacMessageResolver _messageResolver;
		private readonly object _syncObject = new object();
		
		public AutofacLocalMessageBus(
			IComponentContext componentContext)
		{
			_messageResolver = new AutofacMessageResolver(componentContext);
		}

		public IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages) => messages
			.Select(Dispatch)
			.ToList();

		public IMessage Dispatch(IMessage message)
		{
			DispatchMessageToAllRegisteredHandlers(message);
			return message;
		}

		public Result Execute(ICommand c) => DispatchMessageToAllRegisteredHandlers(c);
		
		public Nothing ScheduleOneWayCommand(ICommand c)
		{
			DispatchMessageToAllRegisteredHandlers(c);
			return Nothing.NotAtAll;
		}

		private Result DispatchMessageToAllRegisteredHandlers(IMessage message)
		{
			lock (_syncObject)
			{
				return Result.Combine(
					_messageResolver
						.GetMessageHandlersFor(message)
						.Select(handler => DispatchTo(message, handler))
						.ToArray());	
			}
		}

		private Result DispatchTo(IMessage message, IMessageHandler messageHandler) => messageHandler.Handle(message);
	}
}