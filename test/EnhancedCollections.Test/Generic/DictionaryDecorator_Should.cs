
using System;
using System.Collections;
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
            return new Mock<IDictionary<T, V>>( MockBehavior.Loose);
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

            mock.SetupSet( m => m[5] = 5);

            var decorator = GetDecorator( mock.Object);

            decorator[5] = 5;

            mock.VerifySet( m => m[5] = 5, Times.Once);
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

        [Fact]
        public void CallDecoratedAddWithKvp()
        {
            CallDecoratedVoidMethod<int, int>( d => d.Add( new KeyValuePair<int, int>( 5, 5)));
        }

        [Fact]
        public void CallDecoratedAdd()
        {
            CallDecoratedVoidMethod<int, int>( d => d.Add( 5, 5));
        }

        [Fact]
        public void CallClear()
        {
            CallDecoratedVoidMethod<int, int>( d => d.Clear());
        }

        [Fact]
        public void CallDecoratedContains()
        {
            CallDecoratedMethod<int, int, bool>( d =>  d.Contains( new KeyValuePair<int, int>( 5, 5)));
        }

        [Fact]
        public void CallDecoratedContainsKey()
        {
            CallDecoratedMethod<int, int, bool>( d => d.ContainsKey( 5));
        }

        [Fact]
        public void CallDecoratedCopyTo()
        {
            CallDecoratedVoidMethod<int, int>( d => d.CopyTo( new KeyValuePair<int, int>[] {}, 0));
        }

        [Fact]
        public void CallDecoratedGetEnumerator()
        {
            CallDecoratedVoidMethod<int, int>( d => d.GetEnumerator());
        }

        [Fact]
        public void CallDecoratedRemoveKvp()
        {
            CallDecoratedMethod<int, int, bool>( d => d.Remove( new KeyValuePair<int, int>( 5, 5)));
        }

        [Fact]
        public void CallDecoratedRemove()
        {
            CallDecoratedMethod< int, int, bool>( d => d.Remove( 5));
        }

        [Fact]
        public void CallDecoratedTryGetValue()
        {
            int outVal;
            CallDecoratedMethod< int, int, bool>( d => d.TryGetValue( 5, out outVal));
        }

        [Fact]
        public void CallDecoratedNonGenericGetEnumerator()
        {
            CallDecoratedMethod< int, int, IEnumerator>( d => (d as IEnumerable).GetEnumerator());
        }
        

        
        private void CallDecoratedVoidMethod<TKey, TValue>( Expression<Action<IDictionary<TKey, TValue>>> expr)
        {
            var mock = GetMock<TKey, TValue>();
            
            mock.Setup( expr);

            var decorator = GetDecorator( mock.Object);

            var compiled = expr.Compile();

            compiled.Invoke( decorator);

            mock.Verify( expr);
        }

        private void CallDecoratedMethod<TKey, TValue, TRet>( Expression<Func<IDictionary<TKey, TValue>, TRet>> expr)
        {
            var mock = GetMock<TKey, TValue>();

            mock.Setup( expr);

            var decorator = GetDecorator( mock.Object);
            var compiled = expr.Compile();

            compiled.Invoke( decorator);

            mock.Verify( expr);
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