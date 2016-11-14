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

        public virtual TValue this[TKey key]
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

        public virtual int Count
        {
            get
            {
                return _decorated.Count;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return _decorated.IsReadOnly;
            }
        }

        public virtual ICollection<TKey> Keys
        {
            get
            {
                return _decorated.Keys;
            }
        }

        public virtual ICollection<TValue> Values
        {
            get
            {
                return _decorated.Values;
            }
        }

        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            _decorated.Add( item);
        }

        public virtual void Add(TKey key, TValue value)
        {
            _decorated.Add( key, value);
        }

        public virtual void Clear()
        {
            _decorated.Clear();
        }

        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _decorated.Contains( item);
        }

        public virtual bool ContainsKey(TKey key)
        {
            return _decorated.ContainsKey( key);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _decorated.CopyTo( array, arrayIndex);
        }

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _decorated.GetEnumerator();
        }

        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _decorated.Remove(item);
        }

        public virtual bool Remove(TKey key)
        {
            return _decorated.Remove( key);
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            return _decorated.TryGetValue( key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _decorated.GetEnumerator();
        }
    }
}