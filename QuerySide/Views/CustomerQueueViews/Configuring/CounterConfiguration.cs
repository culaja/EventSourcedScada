using System;
using System.Linq.Expressions;

namespace QuerySide.Views.CustomerQueueViews.Configuring
{
    internal sealed class CounterConfiguration
    {
        public Guid Id { get; }
        public string Name { get; set; }

        public CounterConfiguration(
            Guid id,
            string name)
        {
            Id = id;
            Name = name;
        } 
    }
}