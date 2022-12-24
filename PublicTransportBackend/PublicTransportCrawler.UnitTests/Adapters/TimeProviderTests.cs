using FluentAssertions;
using PublicTransportCrawler.Adapters;

namespace PublicTransportCrawler.UnitTests.Adapters;

[TestFixture]
public class TimeProviderTests
{
    private ITimeProvider _uut;

    [SetUp]
    public void SetUp()
    {
        _uut = new TimeProvider();
    }

    [Test]
    public void ShouldReturnCentralEuropeanTime()
    {
        // Act
        var actual = _uut.GetCurrentTime();
        
        // Assert
        actual.Should().BeCloseTo(DateTime.UtcNow + TimeSpan.FromHours(1), TimeSpan.FromSeconds(1));
    }
}