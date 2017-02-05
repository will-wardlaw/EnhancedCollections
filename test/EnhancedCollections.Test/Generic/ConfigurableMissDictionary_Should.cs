
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnhancedCollections.Generic;
using Xunit;

namespace EnhancedCollections.Test.Generic
{
    public class ConfigurableMissDictionary_Should
    {
        //TODO: A lot more Unit Tests!

        [Theory]
        [InlineData( new int[] {})]
        [InlineData( new [] { 0})]
        [InlineData( new [] { 1, 2, 3, 4, 5})]
        [InlineData( new [] { 100, 101, 102})]
        public void NotCallMissedHandlerWhenContainsKeyOnIndexGet( IEnumerable<int> keyList)
        {
            keyList = keyList.ToList();
            var decorated = BuildTestingDecoratedDictionary( keyList);

            var callCount = 0;
            var missHandler = new ConfigurableMissDictionary<int, string>.DictionaryMissHandler( k => { callCount++; return "bad data";});

            var dict = new ConfigurableMissDictionary<int, string>( decorated, missHandler, null);

            foreach( var k in keyList)
            {
                var expected = decorated[k];
                var actual = dict[k];

                Assert.Equal( expected, actual);
            }

            var expectedCallCount = 0;
            var actualCallCount = callCount;

            Assert.Equal( expectedCallCount, actualCallCount);
        }

        [Theory]
        [InlineData( new int[] {}, new int[] {})]
        [InlineData( new int[] {1, 2, 3, 4}, new int[]{ 5, 6, 7, 8})]
        public void DoesCallMissHandlerWhenDoesNotContainKeyOnIndexGet( IEnumerable<int> keyList, IEnumerable<int> missingKeyList)
        {
            keyList = keyList.ToList();

            var decorated = BuildTestingDecoratedDictionary( keyList);

            var callCount = 0;
            var missHandler = new ConfigurableMissDictionary<int, string>.DictionaryMissHandler( k => { callCount++; return "bad data";});

            var dict = new ConfigurableMissDictionary<int, string>( decorated, missHandler, null);

            foreach( var missingKey in missingKeyList)
            {
                if( decorated.ContainsKey( missingKey))
                {
                    throw new Exception( "missingKey should not be in keyList");
                }
                
                var expected = "bad data";
                var actual = dict[missingKey];

                Assert.Equal( expected, actual);
            }

            var expectedCallCount = missingKeyList.Count();
            var actualCallCount = callCount;
            Assert.Equal( expectedCallCount, actualCallCount);
        }

        private IDictionary< int, string> BuildTestingDecoratedDictionary( IEnumerable<int> keyList)
        {
            keyList = keyList.ToList();
            var decorated = keyList.ToDictionary( k => k, k => {
                var sb = new StringBuilder();
                for( int i = 0; i < k; i++)
                {
                    sb.Append( 'a');
                }
                return sb.ToString();
            });

            return decorated;
        }
    }
}
