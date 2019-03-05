using System;
using System.Collections.Generic;
using Common;

namespace CommandSide.Adapters.InMemory
{
    public sealed class UniqueIndexing<T> where T : AggregateRoot
    {
        private readonly Dictionary<Type, Dictionary<object, T>> _allIndexes = new Dictionary<Type, Dictionary<object, T>>();

        public bool ContainsIndex(object key)
        {
            if (!_allIndexes.TryGetValue(key.GetType(), out var indexDictionary))
            {
                indexDictionary = new Dictionary<object, T>();
                _allIndexes.Add(key.GetType(), indexDictionary);
            }
            
            return indexDictionary.ContainsKey(key);
        }

        public void AddIndex(object key, T value)
        {
            if (!_allIndexes.TryGetValue(key.GetType(), out var indexDictionary))
            {
                indexDictionary = new Dictionary<object, T>();
                _allIndexes.Add(key.GetType(), indexDictionary);
            }
            
            indexDictionary.Add(key, value);
        }

        public Maybe<T> GetValueFor(object key)
        {
            if (_allIndexes.TryGetValue(key.GetType(), out var indexDictionary) &&
                indexDictionary.TryGetValue(key, out var value))
            {
                return value;
            }
            
            return Maybe<T>.None;
        }
    }
}