using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PhoneWebShop.Business.Test.Services.MemoryCachingTests
{
    public class GetOrAdd
    {
        private readonly Mock<IMemoryCache> _mockMemoryCache = new();
        private readonly MemoryCaching _classToBeTested;
        private readonly Mock<IOptions<CacheSettings>> _mockCacheOptions = new();

        public GetOrAdd()
        {
            _mockCacheOptions.SetupGet(options => options.Value).Returns(new CacheSettings());
            _classToBeTested = new(_mockMemoryCache.Object, _mockCacheOptions.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData((string)null)]
        public void Should_ThrowArgumentException_When_KeyIsNullOrEmpty(string key)
        {
            // Act
            Func<Task> action = async () => await _classToBeTested.GetOrAdd(key, () => Task.FromResult("Anything"));

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void Should_AddToCache_When_NotInCache()
        {
            // Arrange
            object expectedOutput = new();
            _mockMemoryCache.Setup(cache => cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny)).Returns(false);
            var mockCacheEntry = new Mock<ICacheEntry>();
            _mockMemoryCache.Setup(cache => cache.CreateEntry(It.IsAny<object>())).Returns(mockCacheEntry.Object);

            // Act
            await _classToBeTested.GetOrAdd("Key", () => Task.FromResult("Anything"));

            // Assert
            _mockMemoryCache.Verify(cache => cache.CreateEntry(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Should_OnlySetCacheOnce_When_SimultaneouslyCalledFromMultipleThreads()
        {
            // Arrange
            var doesEntryExist = false;
            _mockMemoryCache.Setup(cache => cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny))
                .Returns(() =>
                {
                    return doesEntryExist;
                });
            var mockCacheEntry = new Mock<ICacheEntry>();

            _mockMemoryCache.Setup(cache => cache.CreateEntry(It.IsAny<object>()))
                .Callback(() =>
                {
                    doesEntryExist = true;
                })
                .Returns(mockCacheEntry.Object);

            var tasks = new Task[1000];
            var callbackCount = 0;

            // Act
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(async () => await _classToBeTested.GetOrAdd("Key", () =>
                {
                    callbackCount++;
                    return Task.FromResult("Value");
                }));
            }

            await Task.WhenAll(tasks);

            // Assert
            callbackCount.Should().Be(1);
            _mockMemoryCache.Verify(cache => cache.CreateEntry(It.IsAny<object>()), Times.Exactly(1));
        }
    }
}
