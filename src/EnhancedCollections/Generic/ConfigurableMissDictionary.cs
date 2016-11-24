using System;
using System.Collections.Generic;

namespace EnhancedCollections.Generic
{
    public class ConfigurableMissDictionary<TKey, TValue> : DictionaryDecorator<TKey, TValue>
    {

        public ConfigurableMissDictionary( ) : base( new Dictionary<TKey, TValue>())
        {
            // Intentionally blank.
        }

        public ConfigurableMissDictionary( IDictionary<TKey, TValue> decorated) : base( decorated)
        {
            // Intentionally blank.
        }

        public delegate TValue DictionaryMissHandler( TKey missedKey);

        public event DictionaryMissHandler MissedOnIndexGet;

        public event DictionaryMissHandler MissedOnTryGetValue;

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
                return GetValue( MissedOnIndexGet, key);
            }

        }

        public override bool TryGetValue( TKey key, out TValue val)
        {
            val = GetValue( MissedOnIndexGet, key);

            return true;
        }
        
    }
}