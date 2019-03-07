using System;
using System.Linq.Expressions;

namespace QuerySide.Views.CustomerQueueViews.Configuring
{
    internal sealed class CounterConfiguration
    {
        public int Id { get; }
        public string Name { get; set; }

        public CounterConfiguration(
            int id,
            string name)
        {
            Id = id;
            Name = name;
        } 
    }
}