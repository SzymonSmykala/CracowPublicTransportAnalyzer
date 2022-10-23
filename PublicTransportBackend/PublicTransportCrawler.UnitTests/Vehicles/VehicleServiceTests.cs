using FluentAssertions;
using NSubstitute;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler.UnitTests.Vehicles;

[TestFixture]
public class VehicleServiceTests
{
    private IVehicleService _uut;
    private IHttpClientFactory _httpClientFactory;

    [SetUp]
    public void SetUp()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _uut = new VehicleService(_httpClientFactory);
    }

    [Test]
    public async Task ShouldGetAllVehicles()
    {
        // Arrange
        
        // Act
        var actual = await _uut.GetAllVehicles();

        // Assert
        actual.Should().NotBeNull();
    }
}