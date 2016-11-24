
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var decorated = keyList.ToDictionary( k => k, k => {
                var sb = new StringBuilder();
                for( int i = 0; i < k; i++)
                {
                    sb.Append( 'a');
                }
                return sb.ToString();
            });

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
    }
}
