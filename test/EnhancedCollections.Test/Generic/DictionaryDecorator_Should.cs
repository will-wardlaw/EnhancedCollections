
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

            mock.Verify( m => m[It.IsAny<int>()], Times.Once);

        }
    }
}