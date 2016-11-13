
using System.Collections.Generic;
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

            mock.VerifyGet( m => m[It.IsAny<int>()], Times.Once);
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
            var mock = GetMock<int, int>();

            mock.SetupGet( m => m.Count);

            var decorator = GetDecorator( mock.Object);

            var val = decorator.Count;

            mock.VerifyGet( m => m.Count);
        }
    }
}