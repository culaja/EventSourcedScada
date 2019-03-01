using System;
using System.Collections.Concurrent;
using System.Threading;
using Common;
using Common.Messaging;
using Ports;
using static Common.Maybe<System.Threading.Thread>;
using static Common.Nothing;

namespace EventStore
{
    public sealed class DomainEventAggregator : IDisposable
    {
        private readonly BlockingCollection<IDomainEvent> _aggregatedEvents = new BlockingCollection<IDomainEvent>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Maybe<Thread> _maybeWorkerThread = None;

        public Nothing Append(IDomainEvent e)
        {
            _aggregatedEvents.Add(e);
            return NotAtAll;
            
        }

        public Nothing StopAggregation(EventStoreSubscriptionHandler eventStoreSubscriptionHandler) =>_maybeWorkerThread
            .Unwrap(
                WhenThereIsWorkerThreadJustIgnore,
                () => WhenThereIsNoWorkerThreadStartIt(eventStoreSubscriptionHandler));

        private static Nothing WhenThereIsWorkerThreadJustIgnore(Thread _) => NotAtAll;

        private Nothing WhenThereIsNoWorkerThreadStartIt(EventStoreSubscriptionHandler eventStoreSubscriptionHandler)
        {
            var t = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        eventStoreSubscriptionHandler(_aggregatedEvents.Take(_cancellationTokenSource.Token));
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }    
                }
            });
            t.Start();
            _maybeWorkerThread = t;
            return NotAtAll;
        }

        public void Dispose()
        {
            _maybeWorkerThread.Map(t =>
            {
                _cancellationTokenSource.Cancel();
                t.Join();
                return NotAtAll;
            });
            _maybeWorkerThread = None;
        }
    }
}