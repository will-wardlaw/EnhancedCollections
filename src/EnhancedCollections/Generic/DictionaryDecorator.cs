using System;
using System.Collections;
using System.Collections.Generic;

namespace EnhancedCollections.Generic
{
    public class DictionaryDecorator<TKey, TValue> : IDictionary<TKey, TValue>
    {

        protected IDictionary<TKey, TValue> _decorated;

        public DictionaryDecorator( IDictionary<TKey, TValue> decorated)
        {
            _decorated = decorated; 
        }

        public TValue this[TKey key]
        {
            get
            {
                return _decorated[key];
            }

            set
            {
                _decorated[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return _decorated.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _decorated.IsReadOnly;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return _decorated.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return _decorated.Values;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _decorated.Add( item);
        }

        public void Add(TKey key, TValue value)
        {
            _decorated.Add( key, value);
        }

        public void Clear()
        {
            _decorated.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _decorated.Contains( item);
        }

        public bool ContainsKey(TKey key)
        {
            return _decorated.ContainsKey( key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _decorated.CopyTo( array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _decorated.GetEnumerator();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _decorated.Remove(item);
        }

        public bool Remove(TKey key)
        {
            return _decorated.Remove( key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _decorated.TryGetValue( key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _decorated.GetEnumerator();
        }
    }
}