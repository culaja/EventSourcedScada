﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Common.Messaging;

namespace CommonAdapters.AutofacMessageBus
{
    public sealed class AutofacMessageResolver
    {
        private readonly IComponentContext _componentContext;

        public AutofacMessageResolver(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public IReadOnlyList<IMessageHandler> GetMessageHandlersFor(IMessage message)
        {
            return GetMessageHandlersFor(new List<IMessageHandler>(), message.GetType());
        }

        private IReadOnlyList<IMessageHandler> GetMessageHandlersFor(List<IMessageHandler> appendList, Type type)
        {
            var baseType = type.BaseType;
            if (baseType.IsAssignableTo<IMessage>())
            {
                GetMessageHandlersFor(appendList, baseType);
            }

            appendList.AddRange(GetMessageHandlersForType(type));
            return appendList;
        }

        private IEnumerable<IMessageHandler> GetMessageHandlersForType(Type type)
        {
            var genericMessageHandlerType = typeof(IMessageHandler<>).MakeGenericType(type);
            var genericMessageHandlerEnumerableType = typeof(IEnumerable<>).MakeGenericType(genericMessageHandlerType);
            return ((IEnumerable) _componentContext.Resolve(genericMessageHandlerEnumerableType))
                .Cast<IMessageHandler>()
                .Distinct(new MessageHandlerComparer());
        }
    }
}