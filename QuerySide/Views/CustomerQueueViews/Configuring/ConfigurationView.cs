using System.Collections.Generic;
using QuerySide.QueryCommon;
using Shared.CustomerQueue;
using Shared.TicketIssuer;

namespace QuerySide.Views.CustomerQueueViews.Configuring
{
    public sealed class ConfigurationView : SynchronizedView,
        IHandle<CounterAdded>,
        IHandle<CounterRemoved>,
        IHandle<CounterNameChanged>,
        IHandle<OpenTimeAdded>,
        IHandle<OpenTimeRemoved>
    {
        private readonly Dictionary<int, CounterConfiguration> _countersById = new Dictionary<int, CounterConfiguration>();
        private readonly List<OpenTimeConfiguration> _openTimeConfigurations = new List<OpenTimeConfiguration>();

        public IReadOnlyList<CounterConfiguration> Counters => new List<CounterConfiguration>(_countersById.Values);

        public IReadOnlyList<OpenTimeConfiguration> OpenTimes => _openTimeConfigurations;
        
        public void Handle(CounterAdded e)
        {
            _countersById.Add(e.CounterId, new CounterConfiguration(e.CounterId, e.CounterName));
        }

        public void Handle(CounterRemoved e)
        {
            _countersById.Remove(e.CounterId);
        }

        public void Handle(CounterNameChanged e)
        {
            _countersById[e.CounterId].Name = e.NewCounterName;
        }

        public void Handle(OpenTimeAdded e)
        {
            _openTimeConfigurations.Add(new OpenTimeConfiguration(e.DayOfWeek, e.BeginTimestamp, e.EndTimestamp));
        }

        public void Handle(OpenTimeRemoved e)
        {
            _openTimeConfigurations.Remove(new OpenTimeConfiguration(e.DayOfWeek, e.BeginTimestamp, e.EndTimestamp));
        }
    }
}