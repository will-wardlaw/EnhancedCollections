
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EnhancedCollections.Generic;
using Moq;
using Xunit;

namespace EnhancedCollections.Test.Generic
{
    public class DictionaryDecorator_Should
    {
        private Mock<IDictionary<T, V>> GetMock<T, V>()
        {
            return new Mock<IDictionary<T, V>>();
        }

        private DictionaryDecorator<T, V> GetDecorator<T, V>( IDictionary<T, V> decorated)
        {
            return new DictionaryDecorator<T, V>( decorated);
        }

        [Fact]
        public void CallDecoratedIndexerGet()
        {
            var mock = GetMock<int, int>();

            mock.SetupGet( m => m[It.IsAny<int>()]);

            var decorator = GetDecorator( mock.Object);

            int val = decorator[5];

            mock.Verify( m => m[It.IsAny<int>()], Times.Once);
        }

        [Fact]
        public void CallDecoratedIndexerSet()
        {
            var mock = GetMock<int, int>();

            mock.SetupSet( m => m[It.IsAny<int>()] = It.IsAny<int>());

            var decorator = GetDecorator( mock.Object);

            decorator[5] = 5;

            mock.VerifySet( m => m[It.IsAny<int>()] = It.IsAny<int>(), Times.Once);
        }

        [Fact]
        public void CallDecoratedCount()
        {
            CallDecoratedGetter<int, int, int>( m => m.Count);
        }

        [Fact]
        public void CallDecoratedIsReadOnly()
        {
            CallDecoratedGetter<int, int, bool>( m => m.IsReadOnly);
        }

        [Fact]
        public void CallDecoratedKeys()
        {
            CallDecoratedGetter<int, int, ICollection<int>>( m => m.Keys);
        }

        [Fact]
        public void CallDecoratedValues()
        {
            CallDecoratedGetter<int, int, ICollection<int>>( m => m.Keys);
        }

        
        private void CallDecoratedGetter<TKey, TValue, TProperty>( Expression<Func<IDictionary<TKey, TValue>, TProperty>> getterExpression)
        {
            var mock = GetMock<TKey, TValue>();

            mock.SetupGet( getterExpression);

            var decorator = GetDecorator( mock.Object);

            var compiled = getterExpression.Compile();
            var val = compiled.DynamicInvoke( decorator);

            mock.VerifyGet( getterExpression);
        }
    }
}