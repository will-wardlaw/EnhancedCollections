using System.Collections.Generic;

namespace EnhancedCollections.Generic
{
    public class DefaultValueDictionary<Tkey, Tvalue> : ConfigurableMissDictionary<Tkey, Tvalue>
    {
        public DefaultValueDictionary( IDictionary<Tkey, Tvalue> decorated, Tvalue defaultValue) : base( decorated, k => defaultValue)
        {
            // Nothing to do here.
        }

        public DefaultValueDictionary( IDictionary<Tkey, Tvalue> decorated) : this( decorated,default( Tvalue))
        {
            // Nothing to do here.
        }
    }
}
