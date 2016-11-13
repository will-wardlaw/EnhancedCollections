
using System.Collections.Generic;
using EnhancedCollections.Generic;
using Moq;
using Xunit;

namespace EnhancedCollections.Test.Generic
{
    public class DictionaryDecorator_Should
    {
        [Fact]
        public void CallDecoratedIndexerGet()
        {
            var mock = new Mock<IDictionary<int, int>>();

            mock.SetupGet( m => m[It.IsAny<int>()]);

            var decorator = new DictionaryDecorator<int, int>( mock.Object);

            int val = decorator[5];

            mock.VerifyGet( m => m[It.IsAny<int>()], Times.Once);
        }

        [Fact]
        public void CallDecoratedIndexerSet()
        {
            var mock = new Mock<IDictionary<int, int>>();

            mock.SetupSet( m => m[It.IsAny<int>()] = It.IsAny<int>());

            var decorator = new DictionaryDecorator<int, int>( mock.Object);

            decorator[5] = 5;

            mock.VerifySet( m => m[It.IsAny<int>()] = It.IsAny<int>(), Times.Once);
        }

        [Fact]
        public void CallDecoratedCount()
        {
            var mock = new Mock<IDictionary< int, int>>();

            mock.SetupGet( m => m.Count);

            var decorator = new DictionaryDecorator< int, int>( mock.Object);

            var val = decorator.Count;

            mock.VerifyGet( m => m.Count);
        }
    }
}