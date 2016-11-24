using System;
using System.Collections.Generic;

namespace EnhancedCollections.Generic
{
    public class ConfigurableMissDictionary<TKey, TValue> : DictionaryDecorator<TKey, TValue>
    {
        public delegate TValue DictionaryMissHandler( TKey missedKey);

        private DictionaryMissHandler _missedOnIndexGet;

        private event DictionaryMissHandler _missedOnTryGetValue;

        public ConfigurableMissDictionary( IDictionary<TKey, TValue> decorated, DictionaryMissHandler missedHandler) : this( decorated, missedHandler, missedHandler)
        {
            //Intentionally blank.
        }

        public ConfigurableMissDictionary( IDictionary<TKey, TValue> decorated, DictionaryMissHandler missedOnIndexGet, DictionaryMissHandler missedOnTryGetValue) : base( decorated)
        {
            _missedOnIndexGet = missedOnIndexGet;
            _missedOnTryGetValue = missedOnTryGetValue;
        }     

        private TValue GetValue( DictionaryMissHandler missHandler, TKey key)
        {
            TValue val;
            if( _decorated.TryGetValue( key, out val))
            {
                return val;
            }
            return HandleMiss( missHandler, key);
        }

        private TValue HandleMiss( DictionaryMissHandler missHandler, TKey missedKey)
        {
            if( missHandler == null) throw new NullReferenceException( "missHandler");

            return missHandler( missedKey);
        }

        public override TValue this[TKey key]
        {
            get {
                return GetValue( _missedOnIndexGet, key);
            }
        }

        public override bool TryGetValue( TKey key, out TValue val)
        {
            val = GetValue( _missedOnTryGetValue, key);

            return true;
        }
    }
}